using Microsoft.EntityFrameworkCore;
using Examiner.Authentication.Domain.Entities;

namespace Examiner.Infrastructure.Contexts;

public class ExaminerContext : DbContext
{
    public ExaminerContext()
    {

    }

    public ExaminerContext(DbContextOptions<ExaminerContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dbHost = "localhost";
            var dbName = "examiner";
            var dbUser = "root";
            var dbPort = "3306";
            var dbPassword = "";
            var connection = $"server={dbHost};port={dbPort}; database={dbName}; user={dbUser}; password={dbPassword}";

            optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
        }
    }

    public DbSet<User>? Users { get; set; }
}