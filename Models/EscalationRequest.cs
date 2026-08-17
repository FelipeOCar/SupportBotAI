using System.ComponentModel.DataAnnotations;

namespace SupportBotAI.Models;

public sealed class EscalationRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte gib deinen Namen ein.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Der Name muss zwischen 2 und 80 Zeichen lang sein.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte gib eine Bestellnummer ein.")]
    [RegularExpression(@"^TS-\d{5}$", ErrorMessage = "Verwende das Format TS-12345.")]
    public string OrderNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Bitte beschreibe dein Anliegen.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Das Anliegen muss zwischen 10 und 1000 Zeichen lang sein.")]
    public string Concern { get; set; } = string.Empty;

    [Required]
    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Normal;

    public bool IncludeConversation { get; set; } = true;
    public List<ChatMessage> ConversationSnapshot { get; set; } = [];
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
}

public enum UrgencyLevel
{
    Normal,
    High,
    Critical
}
