using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Examiner.Application.Authentication.Interfaces;
using Examiner.Domain.Dtos.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Examiner.Application.Authentication.Jwt;

/// <summary>
/// Generates an authentication token with the user's role & email
/// </summary>
public class JwtTokenHandler : IJwtTokenHandler
{

    public const string JWT_SECURITY_KEY = "yoe9393832616ad6b-ab51-40d7-9492-8f6116f90f92jNxUizBwUW7a673f57-2ce9-4783-b578-37b70e86bf6fVOqPfff()lYliu#Q9fCqy49c8zjTR0a9o8u/Wa16i94QPbe1ffeb7-7b52--fe3964c26210+/8eHYqm8nCeAw/pk3jBLu4wAipjnmJkw==_=@1HHW";
    private const int JWT_TOKEN_VALIDITY_MINS = 20;
    private const string SUCCESS_RESPONSE="Authenticating user was successful";

    public JwtTokenHandler()
    {

    }
    public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
    {
        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        var claimsIdentity = new ClaimsIdentity(new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Name, request.Email),
            new Claim(ClaimTypes.Role, request.Role)
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

        return new AuthenticationResponse
        {
            Success = true,
            ResultMessage = SUCCESS_RESPONSE,
            Email = request.Email,
            JwtToken = token,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
        };
    }
}