using Microsoft.EntityFrameworkCore;
using Examiner.Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Examiner.Infrastructure.Contexts;

public class ExaminerContext : DbContext
{
    // private readonly string? _connectionString;
    public ExaminerContext()
    {
        // _connectionString = connectionString;
    }

    public ExaminerContext(DbContextOptions<ExaminerContext> options) : base(options) { }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         // var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    //         // var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "examiner";
    //         // var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
    //         // var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
    //         // var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

    //         // var connection = $"server={dbHost};port={dbPort}; database={dbName}; user={dbUser}; password={dbPassword}";
    //         // var databaseUrl = Environment.GetEnvironmentVariable("MYSQL_URL");
    //         // var connection = string.IsNullOrEmpty(databaseUrl) ? _connectionString : BuildConnectionString(databaseUrl);

    //         optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    //     }
    // }

    // public string GetConnectionString(IConfiguration configuration)
    // {
    //     var connectionString = configuration.GetConnectionString("ExaminerConn");
    //     var databaseUrl = Environment.GetEnvironmentVariable("MYSQL_URL");
    //     return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
    // }

    //build the connection string from the environment. i.e. Railway
    // mysql://root:XlRXKXYCZ51QKu4rJxV0@containers-us-west-155.railway.app:6751/railway
    // postgresql://postgres:wkYanoSiFmyP7uek6cOo@containers-us-west-157.railway.app:5688/railway
    // private string BuildConnectionString(string databaseUrl)
    // {
    //     var databaseUri = new Uri(databaseUrl);
    //     var userInfo = databaseUri.UserInfo.Split(':');
    //     var connection = new StringBuilder();
    //     connection.Append($"server={databaseUri.Host};");
    //     connection.Append($"port={databaseUri.Port};");

    //     var database = databaseUri.LocalPath.TrimStart('/');
    //     connection.Append($"database={database};");
    //     var user = userInfo[0];
    //     connection.Append($"user={user};");
    //     var password = userInfo[1];
    //     connection.Append($"password={password}");

    //     return connection.ToString();

    //     // var builder = new NpgsqlConnectionStringBuilder
    //     // {
    //     //     Host = databaseUri.Host,
    //     //     Port = databaseUri.Port,
    //     //     Username = userInfo[0],
    //     //     Password = userInfo[1],
    //     //     Database = databaseUri.LocalPath.TrimStart('/'),
    //     //     SslMode = SslMode.Require,
    //     //     TrustServerCertificate = true
    //     // };
    //     // return builder.ToString();
    // }

    public DbSet<User>? Users { get; set; }
}