using Examiner.Application.Notifications.Interfaces;
using Examiner.Domain.Dtos;
using Examiner.Domain.Dtos.Authentication;
using Examiner.Domain.Entities.Authentication;
using Examiner.Domain.Entities.Users;

namespace Examiner.Application.Authentication.Interfaces;

public interface ICodeService : IVerificationService
{
    Task<CodeGenerationResponse> CreateCode();
    Task<CodeVerification?> GetCode(string code);
    Task<GenericResponse> VerifyCode(User user, string suppliedCode);
}