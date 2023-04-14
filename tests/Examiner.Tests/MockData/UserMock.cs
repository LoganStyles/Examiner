using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Users;
using BC = BCrypt.Net.BCrypt;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Dtos;
using Examiner.Common;

namespace Examiner.Tests.MockData;

public static class UserMock
{

    #region Registration
    public static RegisterUserRequest RegisterTutorWithInvalidPassword()
    {
        return new RegisterUserRequest("e@gmail.com", "string", "string");
    }

    public static RegisterUserRequest RegisterTutorWithValidPassword()
    {
        return new RegisterUserRequest("e@gmail.com", "strin(1)G", "strin(1)G");
    }
    public static RegisterUserRequest RegisterTutorWithNonMatchingPassword()
    {
        return new RegisterUserRequest("e@gmail.com", "strin()G", "strin(1)G");
    }

    public static User GetValidTutor()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = "adam",
            LastName = "felix",
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true
        };
    }
    public static User GetValidNewRegistrationTutor()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = "adam",
            LastName = "felix",
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true
        };

    }

    public static UserResponse? GetNewlyRegisteredUser()
    {
        return new UserResponse
        {
            Success = true,
            ResultMessage = "Registering user was successful, and verification code sent successfully",
            Id = Guid.NewGuid(),
            Email = "e@gmail.com"
        };
    }

    public static Task<IEnumerable<User>> GetEmptyListOfExistingUsers()
    {
        return Task.FromResult((new List<User>()).AsEnumerable());
    }

    public static Task<IEnumerable<User>> GetAListOfValidTutors()
    {
        return Task.FromResult(
            (
                new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "adam",
                        LastName = "felix",
                        Email = "e@gmail.com",
                        PasswordHash = BC.HashPassword("strin(1)G"),
                        IsActive = true
                    }
                }
            ).AsEnumerable()
        );
    }

    #endregion

    #region Login

    public static AuthenticationRequest AuthenticateTutorWithValidPassword()
    {
        return new AuthenticationRequest("emma@gmail.com", "strin(1)G");
    }
    
    public static AuthenticationRequest AuthenticateTutorWithNonExistingPassword()
    {
        return new AuthenticationRequest("emma@gmail.com", "strin(2)G");
    }

    public static AuthenticationRequest GetInvalidAuthenticationRequest()
    {
        return new AuthenticationRequest("emma@gmail.com", "");
    }

    public static Task<GenericResponse> GetSuccessfulAuthenticationResponse()
    {
        return Task.FromResult((GenericResponse)new AuthenticationResponse
        {
            Success = true,
            ResultMessage = $"{AppMessages.AUTHENTICATION} {AppMessages.SUCCESSFUL}",
            Email = "emma@gmail.com",
            JwtToken = "token",
            ExpiresIn = 20

        });
    }

    public static Task<GenericResponse> GetFailedUserResponse()
    {
        return Task.FromResult(new GenericResponse(false, $"{AppMessages.USER} {AppMessages.NOT_EXIST}"));
    }
    
    public static Task<GenericResponse> GetInvalidUserCredentialsUserResponse()
    {
        return Task.FromResult(new GenericResponse(false, $"{AppMessages.INVALID_EMAIL_PASSWORD}"));
    }

    public static Task<IEnumerable<User>> GetAListOfNewlyRegisteredValidTutors()
    {
        return Task.FromResult(
            (
                new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "emma@gmail.com",
                        PasswordHash = BC.HashPassword("strin(1)G"),
                        IsActive=false
                    }
                }
            ).AsEnumerable()
        );
    }
    
    public static Task<IEnumerable<User>> GetAListOfNewlyRegisteredValidTutorsWithoutRole()
    {
        return Task.FromResult(
            (
                new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Email = "emma@gmail.com",
                        PasswordHash = BC.HashPassword("strin(1)G"),
                        IsActive=true
                    }
                }
            ).AsEnumerable()
        );
    }

    public static AuthenticationResponse? GetValidAuthenticationResponse()
    {
        return new AuthenticationResponse
        {
            Success = true,
            ResultMessage = $"{AppMessages.AUTHENTICATION} {AppMessages.SUCCESSFUL}",
            Email = "emma@gmail.com",
            JwtToken = "token",
            ExpiresIn = 20
        };
    }

    
    

    #endregion
}