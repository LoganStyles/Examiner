using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Examiner.Infrastructure.Repositories.Interfaces;
using Examiner.Domain.Entities;

namespace Examiner.Infrastructure.Repositories;

///<summary>
/// Implements contract for basic database operations on all entities
///</summary>
///<typeparam name="TEntity">The Type of Entity to operate on</typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{

    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    ///<summary>
    ///Repository constructor
    /// </summary>
    /// <param name="dbContext"> The Database Context </param>
    public BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    /// <summary>
    /// Get a collection of entities based on some specified criteria
    /// </summary>
    /// <param name="filter"> The condition the entities must fulfil to be returned</param>
    /// <param name="orderBy"> The function used to order the entities</param>
    /// <param name="take"> The number of records to limit the results to</param>
    /// <param name="skip"> The number of records to skip</param>
    /// <param name="includeProperties"> Any other navigation properties to include when returning the collection</param>
    /// <returns> A collection of entities </returns>

    public virtual async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null
    )
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(item);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Get an entity by id
    /// </summary>
    /// <param name="id">The id of the entity to retrieve</param>
    /// <returns> The entity object if found, otherwise null </returns>
    public virtual async Task<TEntity?> GetByIdAsync([AllowNull] object id)
    {
        if (id is not null)
            return await _dbSet.FindAsync(id);
        else
            return null;
    }

    /// <summary>
    /// Adds an entity
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The entity that was added</returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    /// <summary>
    /// Determines which object to delete
    /// </summary>
    /// <param name="entityToDelete">The entity to delete</param>
    /// <returns><see cref="Task"/></returns>
    public virtual async Task DeleteAsync(object id)
    {
        TEntity? entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete is not null)
            await DeleteAsync(entityToDelete);
    }

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entityToDelete">The entity to delete</param>
    /// <returns><see cref="Task"/></returns>
    public virtual Task DeleteAsync(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes entities based on a condition
    /// </summary>
    /// <param name="filter">The condition the entities must fulfil to be deleted</param>
    /// <returns><see cref="Task"/></returns>
    public virtual Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = _dbSet.Where(filter);
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="entityToUpdate">The entity to update</param>
    /// <returns><see cref="Task"/></returns>
    public virtual Task Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        return Task.CompletedTask;
    }


}