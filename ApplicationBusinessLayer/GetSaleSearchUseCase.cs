using System.Linq.Expressions;
using EnterpriseBusinessLayer;

namespace ApplicationBusinessLayer;

public class GetSaleSearchUseCase<TModel>
{
    private readonly IRepositorySearch< TModel, Sale> _saleSearchRepository;

    public GetSaleSearchUseCase(IRepositorySearch< TModel, Sale> saleSeachRepository)
    => _saleSearchRepository = saleSeachRepository;
    

    public async Task<IEnumerable<Sale>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
    => await _saleSearchRepository.GetAsync(predicate);
    
   
}