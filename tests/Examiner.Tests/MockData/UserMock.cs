using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Users;
using BC = BCrypt.Net.BCrypt;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Dtos;
using Examiner.Common;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Models;

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

    public static UserIdentity GetValidTutor()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true
        };
    }

    public static UserDto GetValidUserDto()
    {
        return new UserDto
        {
            Email = "e@gmail.com"
        };
    }
    public static UserIdentity GetValidNewRegistrationTutor()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true,
            Role=Role.Tutor
        };

    }
    public static UserIdentity GetUnverifiedTutor()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = false
        };

    }
    
    public static UserIdentity GetNoRoleUser()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true
        };

    }
    public static UserIdentity GetValidRegisteredTutorWithExpiredCodeRequestingCodeVerification()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "e@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true,
            CodeVerification = new CodeVerification()
            {
                Code = "000000",
                IsSent = true,
                Attempts = 0,
                Expired = true
            }
        };

    }
    public static IEnumerable<UserIdentity> GetListOfRegisteredTutorWithExpiredCodeRequestingCodeVerification()
    {
        return
            new List<UserIdentity>{

                new UserIdentity {
                    Id = Guid.NewGuid(),
                    Email = "e@gmail.com",
                    PasswordHash = BC.HashPassword("strin(1)G"),
                    IsActive = true,
                    CodeVerification = new CodeVerification()
                    {
                        Code = "000000",
                        IsSent = true,
                        Attempts = 0,
                        Expired = true
                    }
                }
            }
        ;

    }

    public static IEnumerable<UserIdentity> GetListOfRegisteredTutorWithValidCodeRequestingCodeVerification()
    {
        var codeExpiryTimeStamp = DateTime.Now.AddHours(1);
        return
            new List<UserIdentity>{

                new UserIdentity {
                    Id = Guid.NewGuid(),
                    Email = "e@gmail.com",
                    PasswordHash = BC.HashPassword("strin(1)G"),
                    IsActive = true,
                    CodeVerification = new CodeVerification()
                    {
                        Code = "000000",
                        IsSent = true,
                        Attempts = 0,
                        CreatedDate=DateTime.Now,
                        Expired = false,
                        ExpiresIn=(int)codeExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
                    }
                }
            }
        ;

    }
    public static UserIdentity GetValidRegisteredTutorRequestingCodeThatMatchesAndExists()
    {
        return new UserIdentity
        {
            Id = Guid.NewGuid(),
            Email = "emaa@gmail.com",
            PasswordHash = BC.HashPassword("strin(1)G"),
            IsActive = true,
            CodeVerification = new CodeVerification()
            {
                Code = "123456",
                IsSent = true,
                Attempts = 0,
                Expired = false
            }
        };
    }

    public static UserResponse? GetNewlyRegisteredUserResponse()
    {
        return new UserResponse
        {
            Success = true,
            ResultMessage = "Registering user was successful, and verification code sent successfully",
            Email = "e@gmail.com"
        };
    }

    public static Task<IEnumerable<UserIdentity>> GetEmptyListOfExistingUsers()
    {
        return Task.FromResult((new List<UserIdentity>()).AsEnumerable());
    }

    public static Task<IEnumerable<UserIdentity>> GetAListOfValidTutors()
    {
        return Task.FromResult(
            (
                new List<UserIdentity>
                {
                    new UserIdentity
                    {
                        Id = Guid.NewGuid(),
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
    public static Task<GenericResponse> GetInvalidRequestGenericResponse()
    {
        return Task.FromResult(new GenericResponse(false, $"{AppMessages.INVALID_REQUEST}"));
    }

    public static Task<IEnumerable<UserIdentity>> GetAListOfNewlyRegisteredValidTutors()
    {
        return Task.FromResult(
            (
                new List<UserIdentity>
                {
                    new UserIdentity
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

    public static Task<IEnumerable<UserIdentity>> GetAListOfNewlyRegisteredValidTutorsWithoutRole()
    {
        return Task.FromResult(
            (
                new List<UserIdentity>
                {
                    new UserIdentity
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
    public static Task<GenericResponse> GetValidUserResponse()
    {
        return Task.FromResult(new GenericResponse(true, $"{AppMessages.CHANGE_PASSWORD} {AppMessages.SUCCESSFUL}"));
    }
    public static Task<GenericResponse> GetValidRoleSelectGenericResponse()
    {
        return Task.FromResult(new GenericResponse(true, $"{AppMessages.ROLE} {AppMessages.SUCCESSFUL}"));
    }

    public static ChangePasswordRequest GetNonMatchingPasswordsRequest()
    {
        return new ChangePasswordRequest("emma@gmail.com", "test", "password", "newpassword");
    }

    public static ChangePasswordRequest GetValidChangePasswordRequest()
    {
        return new ChangePasswordRequest("emma@gmail.com", "strin(1)G", "strin(2)G", "strin(2)G");
    }
    public static ChangePasswordRequest GetNonExistingPasswordChangePasswordRequest()
    {
        return new ChangePasswordRequest("emma@gmail.com", "strin(2)G", "strin(1)G", "strin(1)G");
    }
    public static ChangePasswordRequest GetNonValidPasswordParamsChangePasswordRequest()
    {
        return new ChangePasswordRequest("emma@gmail.com", "strin(1)G", "newpassword", "newpassword");
    }

    #endregion

    #region role

    public static SelectRoleRequest GetNonExistingEmailSelectRoleRequest()
    {
        return new SelectRoleRequest("ema@gmail.com", AppMessages.ROLE_TUTOR);
    }
    public static SelectRoleRequest GetNonExistingRoleSelectRoleRequest()
    {
        return new SelectRoleRequest("ema@gmail.com", "Tutors");
    }
    public static SelectRoleRequest GetExistingEmailAndRoleSelectRoleRequest()
    {
        return new SelectRoleRequest("e@gmail.com", AppMessages.ROLE_TUTOR);
    }

    #endregion

    #region code verification

    public static CodeVerificationRequest GetNonExistingUserCodeVerificationRequest()
    {
        return new CodeVerificationRequest("ema@gmail.com", "000000");
    }

    public static CodeVerificationRequest GetExistingUserCodeVerificationRequest()
    {
        return new CodeVerificationRequest("emaa@gmail.com", "123456");
    }
    #endregion

    #region code resend

    public static ResendVerificationCodeRequest GetNonExistingUserResendVerificationRequest()
    {
        return new ResendVerificationCodeRequest("ema@gmail.com");
    }
    public static ResendVerificationCodeRequest GetExistingUserResendVerificationRequest()
    {
        return new ResendVerificationCodeRequest("emaa@gmail.com");
    }
    #endregion

    #region profile update

    // public static PhoneUpdateRequest GetExistingUserPhoneUpdateRequest()
    // {
    //     return new PhoneUpdateRequest("emaa@gmail.com", "234", "8034599890");
    // }

    public static GenericResponse GetSuccessfulPhoneUpdateResponse()
    {
        return GenericResponse.Result(true, $"{AppMessages.MOBILE_PHONE} {AppMessages.UPDATE} {AppMessages.SUCCESSFUL}");
    }
    #endregion


}