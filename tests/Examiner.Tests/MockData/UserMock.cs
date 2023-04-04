using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Users;
using BC = BCrypt.Net.BCrypt;
using Examiner.Domain.Dtos.Users;

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
}