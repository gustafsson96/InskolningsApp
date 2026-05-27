using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnboardingApp.Models;

namespace OnboardingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleSection> ModuleSections { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<UserModuleProgress> UserModuleProgresses { get; set; }
        public DbSet<UserChecklistItemStatus> UserChecklistItemStatus { get; set; }
        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<AdminNotification> AdminNotifications => Set<AdminNotification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<QuizAnswer>()
                .HasOne(q => q.SelectedOption)
                .WithMany()
                .HasForeignKey(q => q.SelectedOptionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<QuizAnswer>()
                .HasOne(q => q.QuizQuestion)
                .WithMany()
                .HasForeignKey(q => q.QuizQuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
