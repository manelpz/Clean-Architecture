using EnterpriseBusinessLayer;

namespace ApplicationBusinessLayer;

public class GetSaleUseCase
{
    private readonly IRepository<Sale> _repository;

    public GetSaleUseCase(IRepository<Sale> repository)
    => _repository = repository;
    

    public async Task<IEnumerable<Sale>> ExecuteAsync()
    => await _repository.GetAllAsync();
}