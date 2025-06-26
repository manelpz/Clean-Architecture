namespace InterfaceAdapterAdapters;

public interface IExternalService<T>
{
     Task<IEnumerable<T>> GetContentAsync();
}