using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Examiner.Application.Authentication.Jwt;

/// <summary>
/// Generates an authentication token with the user's role & email
/// </summary>
public class JwtTokenHandler : IJwtTokenHandler
{

    private readonly IConfiguration _configuration;

    private const int JWT_TOKEN_VALIDITY_DURATION = 30;

    public JwtTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
    {

        var response = new AuthenticationResponse(false, AppMessages.UNABLE_TO_GENERATE_TOKEN);

        var jwt_security_key = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("JWT_SECURITY_KEY"))
        ? _configuration["JWT_SECURITY_KEY"] : Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
        if (jwt_security_key is null)
            return response;

        var tokenExpiryTimeStamp = DateTime.Now.AddDays(JWT_TOKEN_VALIDITY_DURATION);
        var tokenKey = Encoding.ASCII.GetBytes(jwt_security_key);
        var claimsIdentity = new ClaimsIdentity(new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Name, request.Email)
        });

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        response.Success = true;
        response.ResultMessage = AppMessages.AUTHENTICATION + AppMessages.SUCCESSFUL;
        response.Email = request.Email;
        response.JwtToken = token;
        response.ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds;
        return response;

    }
}