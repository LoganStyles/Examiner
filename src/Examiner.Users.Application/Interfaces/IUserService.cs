using Examiner.Common.Dtos;

namespace Examiner.Users.Application.Interfaces;

public interface IUserService
{
    Task<GenericResponse> GetByIdAsync(Guid Id);
    Task<GenericResponse> GetUserByEmail(string email);
    // Task<GenericResponse> Get(
    //     string requiredStatus = "",
    //     string? requiredOrder = null,
    //     string includeProperties = "",
    //     int? take = null,
    //     int? skip = null);
}