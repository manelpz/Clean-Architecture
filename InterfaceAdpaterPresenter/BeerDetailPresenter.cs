using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;

namespace InterfaceAdpaterPresenter;

public class BeerDetailPresenter:IPresenter<Beer, BeerDetailViewModel>
{
    public IEnumerable<BeerDetailViewModel> Present(IEnumerable<Beer> data)
        => data.Select(b => new BeerDetailViewModel
        {
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol +"%",
                Style = b.Style,
                Color = b.IsStrongBeer()?"red":"green",
                Message = b.IsStrongBeer()?"Cervza fuerte":"Cerveza suave",
                
        });

}