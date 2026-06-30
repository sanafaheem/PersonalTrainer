using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using PersonalTrainer.API.Models.DTO;

namespace PersonalTrainer.API.Services;

public class GeminiService(HttpClient httpClient, IConfiguration config, ILogger<GeminiService> logger) : IWorkoutAIService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public async Task<WorkoutPlanResponse> GenerateWorkoutPlanAsync(UserWorkoutProfileRequest request)
    {
        var apiKey = config["Gemini:ApiKey"]
            ?? throw new InvalidOperationException("Gemini API key is not configured.");

        var prompt = BuildPrompt(request);

        var geminiRequest = new GeminiRequest
        {
            Contents = [new Content { Parts = [new Part { Text = prompt }] }],
            GenerationConfig = new GenerationConfig { ResponseMimeType = "application/json" }
        };

        var json = JsonSerializer.Serialize(geminiRequest, JsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
        var response = await httpClient.PostAsync(url, content);

        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            throw new InvalidOperationException("Gemini rate limit reached. Please wait a moment and try again.");

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

        var planJson = geminiResponse.Candidates[0].Content.Parts[0].Text;
        var plan = JsonSerializer.Deserialize<WorkoutPlanResponse>(planJson, JsonOptions)
            ?? throw new InvalidOperationException("Failed to deserialize workout plan.");

        logger.LogInformation(
            "Gemini response SUCCESS — Plan: \"{Title}\" with {Count} exercises",
            plan.Title, plan.Exercises.Count);

        return plan;
    }

    private static string BuildPrompt(UserWorkoutProfileRequest r) => $$"""
        You are an expert personal trainer. Generate a personalised workout plan as valid JSON.

        User profile:
        - Name: {{r.FirstName}}
        - Age: {{r.Age}}
        - Fitness level: {{r.FitnessLevel}}
        - Goal: {{r.Goal}}
        - Focus area: {{r.FocusArea}}
        - Session duration: {{r.DurationMinutes}} minutes
        - Available equipment: {{string.Join(", ", r.Equipment)}}
        - Health limitations: {{r.HealthLimitations ?? "none"}}

        Return ONLY a JSON object matching this exact schema — no markdown, no explanation:
        {
          "title": "string",
          "motivationalIntro": "string personalised with the user's name",
          "warmupCue": "string",
          "cooldownCue": "string",
          "completionMessage": "string personalised with the user's name",
          "exercises": [
            {
              "name": "string",
              "instructions": "string",
              "durationSeconds": number (required, always > 0),
              "restSeconds": number,
              "sets": number or null,
              "reps": number or null,
              "musclesTargeted": "string",
              "difficulty": "string",
              "encouragementMessage": "string"
            }
          ]
        }

        Rules:
        - Every exercise must have durationSeconds > 0
        - Total exercise time should fit within {{r.DurationMinutes}} minutes
        - Tailor exercises to the fitness level, goal, focus area and equipment
        - Respect any health limitations
        """;

    // Gemini API request/response shapes
    private record GeminiRequest
    {
        [JsonPropertyName("contents")] public List<Content> Contents { get; init; } = [];
        [JsonPropertyName("generationConfig")] public GenerationConfig? GenerationConfig { get; init; }
    }

    private record Content
    {
        [JsonPropertyName("parts")] public List<Part> Parts { get; init; } = [];
    }

    private record Part
    {
        [JsonPropertyName("text")] public string Text { get; init; } = "";
    }

    private record GenerationConfig
    {
        [JsonPropertyName("responseMimeType")] public string ResponseMimeType { get; init; } = "application/json";
    }

    private record GeminiResponse
    {
        [JsonPropertyName("candidates")] public List<Candidate> Candidates { get; init; } = [];
        [JsonPropertyName("usageMetadata")] public UsageMetadata? UsageMetadata { get; init; }
    }

    private record Candidate
    {
        [JsonPropertyName("content")] public Content Content { get; init; } = new();
    }

    private record UsageMetadata
    {
        [JsonPropertyName("promptTokenCount")]     public int PromptTokenCount     { get; init; }
        [JsonPropertyName("candidatesTokenCount")] public int CandidatesTokenCount { get; init; }
        [JsonPropertyName("totalTokenCount")]      public int TotalTokenCount      { get; init; }
    }
}
