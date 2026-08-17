using System.ComponentModel.DataAnnotations;

namespace SupportBotAI.Models;

public sealed class FeedbackEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public Guid MessageId { get; set; }

    [Required(ErrorMessage = "Bitte wähle hilfreich oder nicht hilfreich aus.")]
    public HelpfulnessRating? Rating { get; set; }

    [StringLength(500, ErrorMessage = "Der Kommentar darf höchstens 500 Zeichen enthalten.")]
    public string? Comment { get; set; }

    public bool RequestHumanSupport { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}

public enum HelpfulnessRating
{
    Helpful,
    NotHelpful
}
