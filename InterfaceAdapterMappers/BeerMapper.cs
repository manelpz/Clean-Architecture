using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdapterMappers.DTO.Request;

namespace InterfaceAdapterMappers;

public class BeerMapper: IMapper<BeerRequestDTO, Beer>
{
    public Beer ToEntity(BeerRequestDTO dto)
        => new Beer()
        {
            Id = dto.Id,
            Name = dto.Name,
            Style = dto.Style,
            Alcohol = dto.Alcohol,
        };

}