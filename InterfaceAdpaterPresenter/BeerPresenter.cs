using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;

namespace InterfaceAdpaterPresenter;

public class BeerPresenter:IPresenter<Beer, BeerViewModel>
{
    public IEnumerable<BeerViewModel> Present(IEnumerable<Beer> beers)
    {
        return beers.Select(b => new BeerViewModel
        {
            Id = b.Id,
            Alcohol = b.Alcohol + "%",
            Name = "Cerveza: "+b.Name,
        });
    }
}

