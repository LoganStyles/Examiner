using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.UnitOfWork.Interfaces;

/// <summary>
/// Describes contract for unit of work
/// </summary>

public interface IUnitOfWork : IAsyncDisposable
{
    #region Properties

    IUserRepository UserRepository { get; }
    ICodeVerificationRepository CodeVerificationRepository { get; }
    IUserProfileRepository UserProfileRepository { get; }
    IKickboxVerificationRepository KickboxVerificationRepository { get; }
    ISubjectRepository SubjectRepository { get; }

    #endregion

    #region Methods

    Task CompleteAsync();

    #endregion
}