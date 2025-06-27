namespace EnterpriseBusinessLayer;

public class Concept
{
    public int IdBeer { get;  }
    public int Quantity { get;  }
    public decimal UnitPrice { get;  }
    public decimal Price { get;  }

    public Concept(int idBeer, int quantity, decimal unitPrice)
    {
        Quantity = quantity;
        IdBeer = idBeer;
        UnitPrice = unitPrice;
        Price = GetTotalPrice();
    }

    private decimal GetTotalPrice()
    => Quantity * UnitPrice;        
    
}