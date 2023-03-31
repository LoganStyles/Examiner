using Examiner.Application.Notifications.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;

namespace Examiner.Application.Authentication.Interfaces;

public interface ICodeService : IVerificationService
{
    Task<CodeGenerationResponse> GetCode();
}