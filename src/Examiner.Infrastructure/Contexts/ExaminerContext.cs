using Microsoft.EntityFrameworkCore;
using Examiner.Domain.Entities.Users;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Domain.Entities.Authentication;

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

    

    public DbSet<User>? Users { get; set; }
    public DbSet<CodeVerification>? CodeVerifications { get; set; }
    public DbSet<CodeVerificationHistory>? CodeVerificationHistories { get; set; }
    public DbSet<KickboxVerification>? KickboxVerifications { get; set; }
}