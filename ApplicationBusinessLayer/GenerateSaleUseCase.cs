using System.ComponentModel.DataAnnotations;
using EnterpriseBusinessLayer;
using ApplicationBusinessLayer.Exceptions;
using ValidationException = ApplicationBusinessLayer.Exceptions.ValidationException;

namespace ApplicationBusinessLayer;

public class GenerateSaleUseCase<TDTO>
{
   private IRepository<Sale> _repository;
   private readonly IMapper<TDTO, Sale> _mapper;

   public GenerateSaleUseCase(IRepository<Sale> repository, IMapper<TDTO, Sale> mapper)
   {
      _repository = repository;
      _mapper = mapper;
   }

   public async Task ExecuteAsync(TDTO saleDTO)
   {
      var sale = _mapper.ToEntity( saleDTO );

      if (sale.Concepts.Count == 0)
      {
         throw new ValidationException("No concepts found");
      }

      if (sale.Total <= 0)
      {
         throw new ValidationException("No total found");
      }
      
      await _repository.AddAsync(sale);
   }
}