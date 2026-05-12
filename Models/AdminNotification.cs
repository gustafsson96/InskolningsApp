namespace OnboardingApp.Models
{
    public class AdminNotification
    {
        public int AdminNotificationId { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }

        public bool IsRead { get; set; }

        public string Type { get; set; } = string.Empty;
    }
}
