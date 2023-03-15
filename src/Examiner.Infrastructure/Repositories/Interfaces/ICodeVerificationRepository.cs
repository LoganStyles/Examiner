using Examiner.Authentication.Domain.Entities;
using Examiner.Common.Interfaces;

namespace Examiner.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Contract for CodeVerificationRepository
/// </summary>
public interface ICodeVerificationRepository : IBaseRepository<CodeVerification> { }