namespace SupportBotAI.Models;

public sealed class AccessibilitySettings
{
    public string TextSize { get; set; } = "standard";
    public bool HighContrast { get; set; }
    public bool SpeechInput { get; set; } = true;
    public bool SpeechOutput { get; set; }
    public bool AnnounceMessages { get; set; } = true;
    public double SpeechRate { get; set; } = 1.0;
}
