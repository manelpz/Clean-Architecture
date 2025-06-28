using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdapterMappers.DTO.Request;

namespace InterfaceAdapterMappers;

public class SaleMapper:IMapper<SaleRequestDTO, Sale>
{
    public Sale ToEntity(SaleRequestDTO dto)
    {
        var concepts = new List<Concept>(); 
        //var  total = dto.Concepts.Sum(x => x.Quantity * x.UnitPrice);
        foreach (var concept in dto.Concepts)
        {
         concepts.Add(new Concept(concept.IdBeer, concept.Quantity, concept.UnitPrice));   
        }

        var sale = new Sale(DateTime.Now, concepts);

        return sale;
    }
    
}