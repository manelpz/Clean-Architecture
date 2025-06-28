using System.Linq.Expressions;
using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdaptarModels;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapterRepository;

public class SaleSearchRepository:IRepositorySearch< SaleModel, Sale>
{
    private readonly AppDbContext _dbContext;

    public SaleSearchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Sale>> GetAsync(Expression<Func<SaleModel, bool>> predicate)
    {
        var sales = new List<Sale>();
        var salesModel =await _dbContext.Sales.Include("Concepts").Where(predicate).ToListAsync();

        foreach (var saleModel in salesModel)
        {
            var concepts = new List<Concept>();

            foreach (var conceptModel in saleModel.Concepts)
            {
             var concept = new Concept(conceptModel.Id, conceptModel.Quantity, conceptModel.UnitPrice);
                concepts.Add(concept);
            }

            var sale = new Sale(saleModel.CreationDate, concepts);
            sales.Add(sale);

        }
        
        return sales;
    }
}