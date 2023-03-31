using Examiner.Domain.Dtos;

namespace Examiner.Application.Notifications.Interfaces;

/// <summary>
/// Describes contract for verifying notification channels 
//  such as mobile phone, email address & codes
/// </summary>
public interface IVerificationService
{
    Task<GenericResponse> IsVerified(string channel);
    Task<GenericResponse> IsValid(string channel);
}