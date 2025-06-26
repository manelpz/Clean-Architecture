using FluentValidation;
using InterfaceAdapterMappers.DTO.Request;

namespace FrameworkDriversAPI.Validators;

public class BeerValidator:AbstractValidator<BeerRequestDTO> 
{
    public BeerValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("la cervza debe tener nombre");
        RuleFor(dto => dto.Style).NotEmpty().WithMessage("la cervza debe tener un estilo");
        RuleFor(dto => dto.Alcohol).GreaterThan(0).WithMessage("la cervza debe tener alcohol");
    }
}