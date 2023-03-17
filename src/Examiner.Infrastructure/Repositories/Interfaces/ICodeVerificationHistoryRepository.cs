using Examiner.Common.Interfaces;
using Examiner.Domain.Entities.Notifications.Emails;

namespace Examiner.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Contract for CodeVerificationHistoryRepository
/// </summary>
public interface ICodeVerificationHistoryRepository : IBaseRepository<CodeVerificationHistory> { }