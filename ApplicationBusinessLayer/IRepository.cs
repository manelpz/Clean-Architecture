




using EnterpriseBusinessLayer;

namespace ApplicationBusinessLayer;
//use cases
public interface IRepository<TModel>
{
    //Aqui tomaba a beer dorectamente y se puso un generic
    Task<TModel> GetByIdAsync(int id);
    Task<IEnumerable<TModel>> GetAllAsync();
    Task AddAsync(Beer beer);
}