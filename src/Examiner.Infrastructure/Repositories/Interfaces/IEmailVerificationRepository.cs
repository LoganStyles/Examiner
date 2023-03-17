using Examiner.Common.Interfaces;
using Examiner.Domain.Entities.Notifications.Emails;

namespace Examiner.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Contract for EmailVerificationRepository
/// </summary>
public interface IEmailVerificationRepository : IBaseRepository<EmailVerification> { }
