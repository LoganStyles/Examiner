using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Domain.Entities.Users;
using Examiner.Infrastructure.Contexts;

namespace Examiner.Infrastructure.Repositories;

public class UserProfileRepository<T> : BaseRepository<UserProfile>, IUserProfileRepository
{

    /// <summary>
    /// UserProfileRepository Constructor
    /// </summary>
    /// <param name="context"> The database context</param>
    public UserProfileRepository(ExaminerContext context) : base(context) { }
}