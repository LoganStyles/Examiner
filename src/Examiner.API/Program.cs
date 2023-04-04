using System.Text;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Application.Authentication.Jwt;
using Examiner.Application.Authentication.Services;
using Examiner.Application.Notifications.Interfaces;
using Examiner.Application.Notifications.Services;
using Examiner.Application.Users.Interfaces;
using Examiner.Application.Users.Services;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.Helpers;
using Examiner.Infrastructure.UnitOfWork;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;


static string BuildConnectionString(string databaseUrl)
{
    var databaseUri = new Uri(databaseUrl);
    var database = databaseUri.LocalPath.TrimStart('/');
    var userInfo = databaseUri.UserInfo.Split(':');
    var user = userInfo[0];
    var password = userInfo[1];

    var connection = new StringBuilder(5);
    connection.Append($"server={databaseUri.Host};");
    connection.Append($"port={databaseUri.Port};");
    connection.Append($"database={database};");
    connection.Append($"user={user};");
    connection.Append($"password={password}");

    return connection.ToString();
}

// Add services to the container.
services.AddDbContext<ExaminerContext>(
    options =>
    {
        // production environment
        var databaseUrl = Environment.GetEnvironmentVariable("MYSQL_URL");

        // for development environment
        var connectionString = string.Empty;
        var appConnectionString = builder.Configuration.GetConnectionString("ExaminerConn");
        if (appConnectionString is not null)
        {
            var connStrBuilder = new MySqlConnectionStringBuilder(appConnectionString);
            connStrBuilder.UserID = builder.Configuration["MYSQL_USER"];
            connStrBuilder.Password = builder.Configuration["MYSQL_PASSWORD"];
            connectionString = connStrBuilder.ConnectionString;
        }

        var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        options.UseMySql(connection, ServerVersion.AutoDetect(connection));
    });
services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddScoped<IAuthenticationService, AuthenticationService>();
services.AddScoped<ICodeService, CodeService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IVerificationService, KickboxVerificationService>();

services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
services.AddCustomJwtAuthentication();

services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).ConfigureApiBehaviorOptions(options =>
{
    // To preserve the default behaviour, capture the original delegate to call later.
    var builtInFactory = options.InvalidModelStateResponseFactory;

    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

        // Perform logging here
        // ...

        // Invoke the default behavior, which produces a ValidationProblemDetails response.
        // To produce a custom response, return a different implementation of 
        // IActionResult instead.
        return builtInFactory(context);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// update the database with latest migrations
var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
