namespace SupportBotAI.Options;

public sealed class GeminiOptions
{
    public const string SectionName = "Gemini";

    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = "gemini-3.6-flash";
    public int TimeoutSeconds { get; set; } = 25;
    public int MaxOutputTokens { get; set; } = 700;
}
