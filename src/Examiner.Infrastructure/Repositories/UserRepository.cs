using Microsoft.EntityFrameworkCore;
using Examiner.Authentication.Domain.Entities;
using Examiner.Infrastructure.Repositories.Base;
using Examiner.Infrastructure.Repositories.Interfaces;

namespace Examiner.Infrastructure.Repositories;

public class UserRepository<T> : BaseRepository<User>, IUserRepository
{

    /// <summary>
    /// UserRepository Constructor
    /// </summary>
    /// <param name="dbContext"> The database context</param>
    public UserRepository(DbContext context) : base(context) { }
}