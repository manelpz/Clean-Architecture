namespace EnterpriseBusinessLayer;

public class Sale
{
    public int Id { get; }
    public DateTime Date { get; }
    public decimal Total { get; }
    public List<Concept> Concepts { get; }

    public Sale(DateTime date, List<Concept> concepts)
    {
        Date = date;
        Concepts = concepts;
        Total = GetTotal();
    }
    
    public Sale(int id, DateTime date, List<Concept> concepts)
    {
        Id = id;
        Date = date;
        Concepts = concepts;
        Total = GetTotal();
    }
    
    public decimal GetTotal()
        => Concepts.Sum(p => p.Price);
}