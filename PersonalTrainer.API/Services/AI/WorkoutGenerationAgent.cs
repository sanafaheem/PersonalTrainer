using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using PersonalTrainer.API.Enums;
using PersonalTrainer.API.Models.DTO;
using PersonalTrainer.API.Services.AI.Models;

namespace PersonalTrainer.API.Services.AI;

public class WorkoutGenerationAgent(
    HttpClient httpClient,
    IConfiguration config,
    ILogger<WorkoutGenerationAgent> logger,
    IExerciseQueryService exerciseQueryService) : IWorkoutGenerationAgent
{
    private string GeminiUrl =>
        $"""https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={config["Gemini:ApiKey"]}""";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    public async Task<WorkoutPlanResponse> GenerateAsync(UserWorkoutProfileRequest request)
    {
        logger.LogInformation("Starting workout generation for {Name}, {FocusArea}, {Duration} mins",
        request.FirstName, request.FocusArea, request.DurationMinutes);

        // Conversation history — grows with each exchange between us and Gemini
        var conversation = new List<GeminiContent>
        {
            new()
            {
                Role = "user",
                Parts = [new GeminiPart { Text = BuildPrompt(request) }]
            }
        };
        // Agent loop — max 5 iterations to prevent infinite loops
        for (int i = 0; i < 5; i++)
        {
                logger.LogInformation("Agent iteration {Iteration} — calling Gemini", i + 1);

            // Step 1 — Send conversation to Gemini
            var response = await CallGeminiAsync(conversation);
            var candidate = response.Candidates.FirstOrDefault()
                ?? throw new InvalidOperationException("No response from Gemini.");

            // Step 2 — Add Gemini's response to conversation history
            conversation.Add(candidate.Content);

            // Step 3 — Check if Gemini wants to call a tool
            var functionCallPart = candidate.Content.Parts
                .FirstOrDefault(p => p.FunctionCall != null);

            if (functionCallPart?.FunctionCall != null)
            {
                 logger.LogInformation("Gemini requested tool: {ToolName}", 
                functionCallPart.FunctionCall.Name);

                // Step 4 — Execute the tool
                var toolResult = await ExecuteToolAsync(
                    functionCallPart.FunctionCall, request);

                // Step 5 — Add tool result back to conversation
                conversation.Add(new GeminiContent
                {
                    Role = "user",
                    Parts =
                    [
                        new GeminiPart
                    {
                        FunctionResponse = new GeminiFunctionResponse
                        {
                            Name = functionCallPart.FunctionCall.Name,
                            Response = toolResult
                        }
                    }
                    ]
                });

                continue; // Go back to top — let Gemini process the result
            }

            logger.LogInformation("Gemini returned final plan — deserializing");
            // Step 6 — No function call means Gemini is done
            var textPart = candidate.Content.Parts
                .FirstOrDefault(p => p.Text != null)
                ?? throw new InvalidOperationException("No text response from Gemini.");

            var planJson = StripMarkdown(textPart.Text!);
            logger.LogInformation("Plan JSON: {PlanJson}", planJson);

            return JsonSerializer.Deserialize<WorkoutPlanResponse>(planJson, JsonOptions)
                ?? throw new InvalidOperationException("Failed to deserialize workout plan.");
        }
        logger.LogError("Agent loop exceeded maximum iterations");
        throw new InvalidOperationException("Agent loop exceeded maximum iterations.");
    }

    public static readonly GeminiTool WorkOutTools = new()
    {
        FunctionDeclarations =
        [
            new GeminiFunctionDeclaration
            {
                Name = "get_exercises",
                Description = "Retrieves exercises from the database filtered by focus area and available equipment. Always call this before building the workout plan.",
                Parameters = new GeminiFunctionParameters
                {
                    Type = "object",
                    Properties = new Dictionary<string, GeminiParameterProperty>
                    {
                        { "focusArea", new GeminiParameterProperty { Type = "string", Description = "The primary focus area of the workout", Enum = Enum.GetNames<FocusArea>().ToList() } },
                        { "equipment", new GeminiParameterProperty { Type = "array", Description = "List of available equipment the user has.", Items = new GeminiParameterProperty { Type = "string", Enum = Enum.GetNames<Equipment>().ToList() } } },
                    },
                    Required = ["focusArea", "equipment"]
                }
            }
        ]
    };
    private async Task<GeminiResponse> CallGeminiAsync(List<GeminiContent> conversation)
    {
        var request = new GeminiRequest
        {
            Contents = conversation,
            Tools = [WorkOutTools],
            SystemInstructions = new GeminiSystemInstruction
            {
                Parts =
                [
                    new GeminiPart
                {
                    Text = "You are an expert personal trainer. Always call get_exercises before building a workout plan. Never invent exercises — only use exercises provided by the tool."
                }
                ]
            },
            GenerationConfig = new GeminiGenerationConfig
            {
                Temperature = 0.7f,
                MaxOutputTokens = 8192
            }
        };

        var json = JsonSerializer.Serialize(request, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(GeminiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            logger.LogError("Gemini error {StatusCode}: {Body}", (int)response.StatusCode, errorBody);
            response.EnsureSuccessStatusCode();
        }

        var responseJson = await response.Content.ReadAsStringAsync();
        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseJson, JsonOptions)
            ?? throw new InvalidOperationException("Failed to deserialize Gemini response.");

        var usage = geminiResponse.UsageMetadata;
        logger.LogInformation(
            "Gemini token usage — Prompt: {Prompt}, Response: {Response}, Total: {Total}",
            usage?.PromptTokenCount, usage?.CandidatesTokenCount, usage?.TotalTokenCount);

        return geminiResponse;
    }
    private async Task<object> ExecuteToolAsync(
    GeminiFunctionCall functionCall,
    UserWorkoutProfileRequest request)
    {
        switch (functionCall.Name)
        {
            case "get_exercises":
                return await ExecuteGetExercisesAsync(request);

            default:
                throw new InvalidOperationException(
                    $"Unknown tool requested by Gemini: {functionCall.Name}");
        }
    }
    private async Task<object> ExecuteGetExercisesAsync(
        UserWorkoutProfileRequest request)
    {
        // Use values directly from request — we already have them
    var focusArea = Enum.Parse<FocusArea>(request.FocusArea);
    var equipment = request.Equipment
        .Select(e => Enum.Parse<Equipment>(e))
        .ToList();
        // Query the database
        var exercises = await exerciseQueryService
            .GetExercisesForProfileAsync(focusArea, equipment);
            
         logger.LogInformation("Found {Count} exercises for {FocusArea}", 
        exercises.Count, focusArea);
        // Return exercises plus extra context Gemini needs to build the plan
        return new
        {
            exercises,
            userFirstName = request.FirstName,
            age = request.Age,
            fitnessLevel = request.FitnessLevel,
            goal = request.Goal,
            durationMinutes = request.DurationMinutes,
            healthLimitations = request.HealthLimitations ?? "none"
        };
    }
    private static string StripMarkdown(string text)
    {
        var trimmed = text.Trim();
        if (!trimmed.StartsWith("```")) return trimmed;

        var firstNewline = trimmed.IndexOf('\n');
        if (firstNewline < 0) return trimmed;

        var start = firstNewline + 1;
        var end = trimmed.LastIndexOf("```");

        if (end <= start) return trimmed[start..].Trim();

        return trimmed[start..end].Trim();
    }

    private static string BuildPrompt(UserWorkoutProfileRequest r) => $$"""
    You are an expert personal trainer. Your job is to create a personalised workout plan.

    User profile:
    - Name: {{r.FirstName}}
    - Age: {{r.Age}}
    - Fitness level: {{r.FitnessLevel}}
    - Goal: {{r.Goal}}
    - Focus area: {{r.FocusArea}}
    - Session duration: {{r.DurationMinutes}} minutes
    - Available equipment: {{string.Join(", ", r.Equipment)}}
    - Health limitations: {{r.HealthLimitations ?? "none"}}

    Instructions:
    1. FIRST call get_exercises to retrieve exercises from the database
    2. Select appropriate exercises based on the user's goal, fitness level and health limitations
    3. Avoid exercises where contraindications match the user's health limitations
    4. Scale duration, sets and reps within the min/max ranges based on fitness level and age
    5. Beginner → closer to min ranges, Advanced → closer to max ranges
    6. Make sure total workout fits within {{r.DurationMinutes}} minutes
    7. Return ONLY a JSON object — no markdown, no explanation:

    {
      "title": "string",
      "motivationalIntro": "string — personalised with user name",
      "warmupCue": "string",
      "cooldownCue": "string",
      "completionMessage": "string — personalised with user name",
      "exercises": [
        {
          "name": "string",
          "instructions": "string",
          "durationSeconds": number,
          "restSeconds": number,
          "sets": number or null,
          "reps": number or null,
          "musclesTargeted": "string",
          "difficulty": "string",
          "encouragementMessage": "string — personalised with user name"
        }
      ]
    }
    """;
}
