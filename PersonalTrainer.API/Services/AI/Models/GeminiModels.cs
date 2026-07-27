using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PersonalTrainer.API.Services.AI.Models;

/// <summary>
/// Represents a request to be sent to Gemini API
/// </summary>
public record GeminiRequest
{
    /// <summary>
    /// List of content parts for the request
    /// </summary>
    [JsonPropertyName("contents")]
    public List<GeminiContent> Contents { get; init; } = [];

    /// <summary>
    /// System instructions or context for Gemini
    /// </summary>
    [JsonPropertyName("systemInstruction")]
    public GeminiSystemInstruction? SystemInstructions { get; init; }

    [JsonPropertyName("tools")]
    public List<GeminiTool>? Tools { get; init; }


    /// <summary>
    /// Generation configuration settings
    /// </summary>
    [JsonPropertyName("generationConfig")]
    public GeminiGenerationConfig? GenerationConfig { get; init; }

}

/// <summary>
/// Represents content within a Gemini request
/// </summary>
public record GeminiContent
{
    /// <summary>
    /// The role of the content provider (user, model, etc.)
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; init; } = "user";

    /// <summary>
    /// List of parts containing the actual content
    /// </summary>
    [JsonPropertyName("parts")]
    public List<GeminiPart> Parts { get; init; } = [];
}

/// <summary>
/// One part inside a content turn — can be text, a function call, or a function response.
/// Only one will be populated at a time.
/// </summary>
public record GeminiPart
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }

    [JsonPropertyName("functionCall")]
    public GeminiFunctionCall? FunctionCall { get; init; }

    [JsonPropertyName("functionResponse")]
    public GeminiFunctionResponse? FunctionResponse { get; init; }
}

/// <summary>
/// Represents system instructions for Gemini
/// </summary>
public record GeminiSystemInstruction
{
    /// <summary>
    /// List of parts containing instruction content
    /// </summary>
    [JsonPropertyName("parts")]
    public List<GeminiPart> Parts { get; init; } = [];
}

/// <summary>
/// Generation config — controls temperature, token limits, and response format.
/// </summary>
public record GeminiGenerationConfig
{
    [JsonPropertyName("temperature")]
    public float? Temperature { get; init; }

    [JsonPropertyName("maxOutputTokens")]
    public int? MaxOutputTokens { get; init; }

    [JsonPropertyName("topP")]
    public float? TopP { get; init; }

    [JsonPropertyName("topK")]
    public int? TopK { get; init; }

    [JsonPropertyName("responseMimeType")]
    public string? ResponseMimeType { get; init; }
}

/// <summary>
/// Gemini's request to call one of our registered tools.
/// </summary>
public record GeminiFunctionCall
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("args")]
    public Dictionary<string, object>? Args { get; init; }
}
// <summary>
/// Our response after executing a tool — sent back to Gemini to continue reasoning.
/// </summary>
public record GeminiFunctionResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("response")]
    public object? Response { get; init; }
}
/// <summary>
/// Tool container — holds all function declarations available to the agent.
/// </summary>
public record GeminiTool
{
    [JsonPropertyName("functionDeclarations")]
    public List<GeminiFunctionDeclaration> FunctionDeclarations { get; init; } = [];
}

/// <summary>
/// Declares a single tool Gemini can call — name, description, and parameter schema.
/// </summary>
public record GeminiFunctionDeclaration
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    [JsonPropertyName("parameters")]
    public GeminiFunctionParameters? Parameters { get; init; }
}
/// <summary>
/// JSON Schema describing the parameters a tool accepts.
/// </summary>
public record GeminiFunctionParameters
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = "object";

    [JsonPropertyName("properties")]
    public Dictionary<string, GeminiParameterProperty> Properties { get; init; } = new();

    [JsonPropertyName("required")]
    public List<string> Required { get; init; } = [];
}

/// <summary>
/// Describes a single parameter property — type, description, and allowed values.
/// </summary>
public record GeminiParameterProperty
{
    [JsonPropertyName("type")]
    public string Type { get; init; } = "string";

    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    [JsonPropertyName("items")]
    public GeminiParameterProperty? Items { get; init; }

    [JsonPropertyName("enum")]
    public List<string>? Enum { get; init; }
}
// ── Response models ────────────────────────────────────────────

/// <summary>
/// Full response from Gemini API.
/// </summary>
public record GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate> Candidates { get; init; } = [];

    [JsonPropertyName("usageMetadata")]
    public GeminiUsageMetadata? UsageMetadata { get; init; }
}

public record GeminiUsageMetadata
{
    [JsonPropertyName("promptTokenCount")]     public int PromptTokenCount     { get; init; }
    [JsonPropertyName("candidatesTokenCount")] public int CandidatesTokenCount { get; init; }
    [JsonPropertyName("totalTokenCount")]      public int TotalTokenCount      { get; init; }
}

/// <summary>
/// A single candidate response from Gemini.
/// </summary>
public record GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiContent Content { get; init; } = new();

    [JsonPropertyName("finishReason")]
    public string? FinishReason { get; init; }
}
