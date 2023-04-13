using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Examiner.Application.Authentication.Jwt;

/// <summary>
/// Adds Jwt authentication to the service pipeline via dependency injection
/// </summary>
public static class CustomJwtAuthExtension
{

    public static void AddCustomJwtAuthentication(this IServiceCollection service)
    {
        
        var jwt_security_key = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("JWT_SECURITY_KEY"))
        ? ConfigurationHelper.config["JWT_SECURITY_KEY"] 
        : Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");

        if (jwt_security_key is null)
            return;

        service.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {

            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt_security_key))
            };
        });
    }
}