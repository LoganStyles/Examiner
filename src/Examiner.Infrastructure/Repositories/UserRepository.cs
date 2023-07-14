using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.Contexts;

namespace Examiner.Infrastructure.Repositories;

public class UserRepository<T> : BaseRepository<UserIdentity>, IUserRepository
{

    /// <summary>
    /// UserRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public UserRepository(ExaminerContext context) : base(context) { }
}