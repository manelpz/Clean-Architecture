using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapterRepository;

public class SaleRepository: IRepository<Sale>
{
    private readonly AppDbContext _dbContext;

    public SaleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Sale> GetByIdAsync(int id)
    {
        var saleModel = await _dbContext.Sales.FindAsync(id);
        return new Sale(
        
            saleModel.Id,
            saleModel.CreationDate,
                    _dbContext.Models 
                        .Where(c => c.IdSale == saleModel.Id)
                        .Select(c => new Concept(c.IdBeer, c.Quantity, c.UnitPrice))
                        .ToList()
        ); 
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
        => await _dbContext.Sales
            .Select(s => new Sale(
                s.Id,
                s.CreationDate,
                _dbContext.Models
                    .Where(c => c.IdSale == s.Id)
                    .Select(c => new Concept(c.IdBeer, c.Quantity, c.UnitPrice))
                    .ToList()
            )).ToListAsync(); 
        

    public async Task AddAsync(Sale sale)
    {
        var saleModel = new SaleModel();

        saleModel.CreationDate = sale.Date;
        saleModel.Total = sale.Total;
        saleModel.Concepts = sale.Concepts.Select(c => new ConceptModel
        {
            IdBeer = c.IdBeer,
            UnitPrice = c.UnitPrice,
            Quantity = c.Quantity
            

        }).ToList();
        
        await _dbContext.Sales.AddAsync(saleModel);
        await _dbContext.SaveChangesAsync();
    }
}