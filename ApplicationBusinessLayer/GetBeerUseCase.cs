// See https://aka.ms/new-console-template for more information


using EnterpriseBusinessLayer;

namespace ApplicationBusinessLayer;
//use cases
public class GetBeerUseCase<TEntity, TOutPut>
{
    private readonly IRepository<TEntity> _beerRepository;
    private readonly IPresenter<TEntity, TOutPut> _presenter;

    public GetBeerUseCase(IRepository<TEntity> beerRepository, IPresenter<TEntity, TOutPut> presenter)
    {
        _beerRepository = beerRepository;
        _presenter = presenter;
    }

    public async Task<IEnumerable<TOutPut>> ExecuteAsync()
    {
        var beer = await _beerRepository.GetAllAsync();
        
        return _presenter.Present(beer);
    }

}