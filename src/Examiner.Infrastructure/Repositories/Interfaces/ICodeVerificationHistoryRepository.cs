using Examiner.Authentication.Domain.Entities;
using Examiner.Common.Interfaces;

namespace Examiner.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Contract for CodeVerificationHistoryRepository
/// </summary>
public interface ICodeVerificationHistoryRepository : IBaseRepository<CodeVerificationHistory> { }