using Microsoft.EntityFrameworkCore;
using Examiner.Domain.Entities.Users;
using Examiner.Domain.Entities.Notifications.Emails;

namespace Examiner.Infrastructure.Contexts;

public class ExaminerContext : DbContext
{
    public ExaminerContext()
    {
    }

    public ExaminerContext(DbContextOptions<ExaminerContext> options) : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<CodeVerification>? CodeVerifications { get; set; }
        public DbSet<CodeVerificationHistory>? CodeVerificationHistories { get; set; }
        public DbSet<EmailVerification>? EmailVerifications { get; set; }
}