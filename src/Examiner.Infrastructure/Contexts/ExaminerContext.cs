using Microsoft.EntityFrameworkCore;
using Examiner.Domain.Entities.Users;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Content;

namespace Examiner.Infrastructure.Contexts;

/// <summary>
/// Extends the DbContext
/// </summary>
public class ExaminerContext : DbContext
{
    public ExaminerContext()
    {
    }

    public ExaminerContext(DbContextOptions<ExaminerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubjectCategory>().HasData(new SubjectCategory { Id = 1, Title = "Science" });
        modelBuilder.Entity<SubjectCategory>().HasData(new SubjectCategory { Id = 2, Title = "Art" });
        modelBuilder.Entity<SubjectCategory>().HasData(new SubjectCategory { Id = 3, Title = "Social Science" });

        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 1, Title = "Chemistry", SubjectCategoryId = 1 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 2, Title = "Physics", SubjectCategoryId = 1 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 3, Title = "Computer Science", SubjectCategoryId = 1 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 4, Title = "History", SubjectCategoryId = 2 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 5, Title = "Government", SubjectCategoryId = 2 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 6, Title = "Economics", SubjectCategoryId = 2 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 7, Title = "Sociology", SubjectCategoryId = 3 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 8, Title = "Geography", SubjectCategoryId = 3 });
        modelBuilder.Entity<Subject>().HasData(new Subject { Id = 9, Title = "Mass communication", SubjectCategoryId = 3 });
    }
    public DbSet<UserIdentity>? UserIdentities { get; set; }
    public DbSet<UserProfile>? UserProfiles { get; set; }
    public DbSet<CodeVerification>? CodeVerifications { get; set; }
    public DbSet<KickboxVerification>? KickboxVerifications { get; set; }
    public DbSet<SubjectCategory>? SubjectCategories { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
}