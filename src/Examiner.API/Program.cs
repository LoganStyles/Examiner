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

var builder = WebApplication.CreateBuilder(args);


static string BuildConnectionString(string databaseUrl)
{
    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');
    var connection = new StringBuilder();
    connection.Append($"server={databaseUri.Host};");
    connection.Append($"port={databaseUri.Port};");

    var database = databaseUri.LocalPath.TrimStart('/');
    connection.Append($"database={database};");
    var user = userInfo[0];
    connection.Append($"user={user};");
    var password = userInfo[1];
    connection.Append($"password={password}");

    return connection.ToString();
}

// Add services to the container.
builder.Services.AddDbContext<ExaminerContext>(
    options =>
    {
        var databaseUrl = Environment.GetEnvironmentVariable("MYSQL_URL");
        var connectionString = builder.Configuration.GetConnectionString("ExaminerConn");

        var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        options.UseMySql(connection, ServerVersion.AutoDetect(connection));
    });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICodeService, CodeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVerificationService, KickboxVerificationService>();

builder.Services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
builder.Services.AddCustomJwtAuthentication();

builder.Services.AddControllers(options =>
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
