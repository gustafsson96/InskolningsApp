using Microsoft.AspNetCore.Identity;

// Represents an application user. Extends ASP.NET Identity user with additional fields.
namespace OnboardingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? Startdate { get; set; }

        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }
}
