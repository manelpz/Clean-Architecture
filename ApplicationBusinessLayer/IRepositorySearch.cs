using System.Linq.Expressions;

namespace ApplicationBusinessLayer;

public interface IRepositorySearch< TModel, TEntity>
{
    public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TModel, bool>> predicate);
}