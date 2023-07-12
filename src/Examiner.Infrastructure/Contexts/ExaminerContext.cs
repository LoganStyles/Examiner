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

        modelBuilder.Entity<Country>().HasData(new Country { Id = 1, Code = "NG", Title = "Nigeria" });

        modelBuilder.Entity<State>().HasData(new State { Id = 1, Title = "Abia", CountryId = 1, });
        modelBuilder.Entity<State>().HasData(new State { Id = 2, Title = "Adamawa", CountryId = 1, });
        modelBuilder.Entity<State>().HasData(new State { Id = 3, Title = "AkwaIbom", CountryId = 1, });

        modelBuilder.Entity<ExperienceLevel>().HasData(new ExperienceLevel { Id = 1, Title = "Low" });
        modelBuilder.Entity<ExperienceLevel>().HasData(new ExperienceLevel { Id = 2, Title = "Moderate" });
        modelBuilder.Entity<ExperienceLevel>().HasData(new ExperienceLevel { Id = 3, Title = "High" });

        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 1, Title = "Ordinary National Diploma" });
        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 2, Title = "Higher National Diploma" });
        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 3, Title = "Bachelor's Degree" });
        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 4, Title = "Postgraduate Diploma" });
        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 5, Title = "Masters" });
        modelBuilder.Entity<EducationDegree>().HasData(new EducationDegree { Id = 6, Title = "PHD" });
    }
    public DbSet<UserIdentity>? UserIdentities { get; set; }
    public DbSet<UserProfile>? UserProfiles { get; set; }
    public DbSet<CodeVerification>? CodeVerifications { get; set; }
    public DbSet<KickboxVerification>? KickboxVerifications { get; set; }
    public DbSet<SubjectCategory>? SubjectCategories { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<State>? States { get; set; }
    public DbSet<ExperienceLevel>? ExperienceLevels { get; set; }
    public DbSet<EducationDegree>? EducationDegrees { get; set; }
}