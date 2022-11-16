using FluentValidation;

namespace Curso.ComercioElectronico.Application;

 
public class MarcaCrearActualizarDtoValidator : AbstractValidator<MarcaCrearActualizarDto>
{
    public MarcaCrearActualizarDtoValidator()
    {
        RuleFor(x => x).Must(x => false).WithMessage("Verificacion de validaciones con el proyecto");

        RuleFor(x => x.Nombre).Length(1,2);
    }
}
public class CarroItemCrearActualizarDtoValidator : AbstractValidator<CarroItemCrearActualizarDto>
{
    public CarroItemCrearActualizarDtoValidator()
    {
        RuleFor(x => x).Must(x => false).WithMessage("Verificacion de validaciones con el proyecto");

    }
}
public class OrdenItemCrearActualizarDtoValidator : AbstractValidator<OrdenItemCrearActualizarDto>
{
    public OrdenItemCrearActualizarDtoValidator()
    {
        RuleFor(x => x).Must(x => false).WithMessage("Verificacion de validaciones con el proyecto");

    }
}