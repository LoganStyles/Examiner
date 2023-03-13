using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Examiner.Common.Entities;

namespace Examiner.Common.Interfaces;

public interface IBaseRepository<TEntity> where TEntity:BaseEntity{

    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(object id);
    Task DeleteAsync(TEntity entityToDelete);
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", int? take = null, int? skip = null);
    Task<TEntity?> GetByIdAsync([AllowNull] object id);
    Task Update(TEntity entityToUpdate);
}