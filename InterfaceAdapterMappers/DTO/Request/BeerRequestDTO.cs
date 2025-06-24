namespace InterfaceAdapterMappers.DTO.Request;

public class BeerRequestDTO
{
    public int Id { set;  get; }
    public string? Name { set; get; } 
    public string? Style { set; get; }
    public decimal Alcohol { set; get; }
}