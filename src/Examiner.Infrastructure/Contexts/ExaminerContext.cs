using Microsoft.EntityFrameworkCore;
using Examiner.Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;

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
}