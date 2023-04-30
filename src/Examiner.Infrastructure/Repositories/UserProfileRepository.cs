using Microsoft.EntityFrameworkCore;
using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Domain.Entities.Users;

namespace Examiner.Infrastructure.Repositories;

public class UserProfileRepository<T> : BaseRepository<UserProfile>, IUserProfileRepository
{

    /// <summary>
    /// UserProfileRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public UserProfileRepository(DbContext context) : base(context) { }
}