using Examiner.Authentication.Application.Interfaces;
using Examiner.Authentication.Application.Jwt;
using Examiner.Authentication.Application.Services;
using Examiner.Infrastructure.Contexts;
using Examiner.Infrastructure.UnitOfWork;
using Examiner.Infrastructure.UnitOfWork.Interfaces;
using Examiner.Users.Application.Interfaces;
using Examiner.Users.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ExaminerContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();

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
