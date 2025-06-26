namespace ApplicationBusinessLayer;

public interface IExternalServiceAdapter<T>
{
    Task<IEnumerable<T>> GetDataAsync();
    
}