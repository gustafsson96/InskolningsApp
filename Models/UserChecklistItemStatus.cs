// Represents the completion status of a checklist item for a specific user.
namespace OnboardingApp.Models
{
    public class UserChecklistItemStatus
    {
        public int UserChecklistItemStatusId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        public int ChecklistItemId { get; set; }
        public ChecklistItem? ChecklistItem { get; set; }
        public ChecklistStatus Status { get; set; } = ChecklistStatus.NotStarted;
        public DateTime? CompletedAt { get; set; }
    }
}
