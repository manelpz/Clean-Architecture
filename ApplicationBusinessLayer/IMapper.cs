namespace ApplicationBusinessLayer;

public interface IMapper<TDTO, TOutPut>
{
    public TOutPut ToEntity(TDTO dto);
}