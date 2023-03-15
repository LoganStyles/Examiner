using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.UnitOfWork.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    #region Properties

    IUserRepository UserRepository { get; }
    ICodeVerificationRepository CodeVerificationRepository { get; }
    ICodeVerificationHistoryRepository CodeVerificationHistoryRepository { get; }

    #endregion

    #region Methods

    Task CompleteAsync();

    #endregion
}