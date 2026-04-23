using System.ComponentModel.DataAnnotations;

namespace OnboardingApp.Models
{
    public class QuizAnswer
    {
        public int QuizAnswerId { get; set; }

        [Required]
        public int QuizAttemptId { get; set; }

        [Required]
        public int QuizQuestionId { get; set; }

        [Required]
        public int SelectedOptionId { get; set; }

        // Navigation properties
        public QuizAttempt QuizAttempt { get; set; } = null!;
        public QuizQuestion QuizQuestion { get; set; } = null!;
        public QuizOption SelectedOption { get; set; } = null!;
    }
}
