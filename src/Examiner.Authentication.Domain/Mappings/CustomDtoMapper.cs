
using AutoMapper;
using Examiner.Authentication.Domain.Dtos;
using Examiner.Authentication.Domain.Entities;
using BC = BCrypt.Net.BCrypt;

namespace Examiner.Authentication.Domain.Mappings;

/// <summary>
/// Maps different types together
/// </summary>
public class CustomDtoMapper : Profile
{
    public CustomDtoMapper()
    {
        CreateMap<RegisterUserRequest, User>()
        .ForMember(dest => dest.PasswordHash, map => map.MapFrom(src => BC.HashPassword(src.Password)));

        CreateMap<User, UserResponse>();
    }
}