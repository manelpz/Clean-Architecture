using EnterpriseBusinessLayer;

namespace InterfaceAdapterMappers.DTO.Request;

public class SaleRequestDTO
{
    public List<Concept> Concepts { get; set; }
}

public class ConceptRequestDTO
{
    public int IdBeer { get;  }
    public int Quantity { get;  }
    public decimal UnitPrice { get;  }
    
}