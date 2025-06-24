namespace ApplicationBusinessLayer;

public interface IPresenter<TEntity, TOutPut>
{
    public IEnumerable<TOutPut> Present(IEnumerable<TEntity> data); 
}