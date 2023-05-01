
using AutoMapper;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Dtos.Content;
using Examiner.Domain.Dtos.Users;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Content;
using Examiner.Domain.Entities.Notifications.Emails;
using Examiner.Domain.Entities.Users;
using Kickbox.Models;
using BC = BCrypt.Net.BCrypt;

namespace Examiner.Authentication.Domain.Mappings;

/// <summary>
/// Maps different types together
/// </summary>
public class CustomDtoMapper : Profile
{
    public CustomDtoMapper()
    {
        CreateMap<RegisterUserRequest, UserIdentity>()
        .ForMember(dest => dest.PasswordHash, map => map.MapFrom(src => BC.HashPassword(src.Password)));

        CreateMap<UserIdentity, UserResponse>();
        CreateMap<UserIdentity, UserDto>();
        CreateMap<CodeVerification, CodeVerificationResponse>();
        CreateMap<ExtendedKickBoxResponse, KickboxVerification>();
        CreateMap<SubjectCategory, SubjectCategoryDto>();
        CreateMap<Subject, SubjectDto>();
    }
}