

using AutoMapper;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;

public class ConfiguracionesMapeoProfile : Profile
{
    //TipoProductoCrearActualizarDto => TipoProducto
    //TipoProducto => TipoProductoDto
    public ConfiguracionesMapeoProfile()
    {
        CreateMap<TipoProductoCrearActualizarDto, TipoProducto>();
        CreateMap<TipoProducto, TipoProductoDto>();
        CreateMap<CarroItemCrearActualizarDto,Carro>();
        CreateMap<Carro,CarroDto>();
        CreateMap<ProductoCrearActualizarDto,Producto>();
        CreateMap<Producto, ProductoDto>();
        CreateMap<OrdenItemCrearActualizarDto,Orden>();
        CreateMap<Orden, OrdenDto>();
        CreateMap<MarcaCrearActualizarDto,Marca>();
        CreateMap<Marca, MarcaDto>();
        

    }
}

