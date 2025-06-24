namespace EnterpriseBusinessLayer;

public class Beer
{
    //entities
    public int Id { set;  get; }
    public string? Name { set; get; } 
    public string? Style { set; get; }
    public decimal Alcohol { set; get; }
    public bool IsStrongBeer() => Alcohol > 7.5m;
    
}