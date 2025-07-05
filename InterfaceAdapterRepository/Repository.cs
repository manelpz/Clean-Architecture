
using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
//using InterfaceAdapter;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapterRepository;


public class Repository: IRepository<Beer>
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Beer> GetByIdAsync(int id)
    {
        var beerModel = await _dbContext.Beers.FindAsync(id);
        return new Beer()
        {
            Id = beerModel.Id,  
            Alcohol = beerModel.Alcohol,
            Name = beerModel.Name,
            Style = beerModel.Style,
        };
    }

    public async Task<IEnumerable<Beer>> GetAllAsync()
    {
        return await _dbContext.Beers
            .Select(b => new Beer
            {
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol,
                Style = b.Style,
            })
            .ToListAsync(); 
    }

    public async Task AddAsync(Beer beer)
    {
        var beerModel = new BeerModel()
        {
            Id = beer.Id,
            Style = beer.Style,
            Name = beer.Name,
            Alcohol = beer.Alcohol
        };
        await _dbContext.Beers.AddAsync(beerModel);
        await _dbContext.SaveChangesAsync();
    }
}

