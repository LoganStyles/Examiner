using Examiner.Domain.Entities.Users;

namespace Examiner.Infrastructure.Repositories.Interfaces;

/// <summary>
/// User Repository Interface
/// </summary>

public interface IUserRepository : IBaseRepository<UserIdentity> { }