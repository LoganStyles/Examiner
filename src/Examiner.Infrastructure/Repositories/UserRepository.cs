using Microsoft.EntityFrameworkCore;
using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Domain.Entities.Users;

namespace Examiner.Infrastructure.Repositories;

public class UserRepository<T> : BaseRepository<User>, IUserRepository
{

    /// <summary>
    /// UserRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public UserRepository(DbContext context) : base(context) { }
}