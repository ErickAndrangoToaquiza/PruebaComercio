using AutoMapper;
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IMapper mapper;
    private readonly ILogger<TipoProductoAppService> logger;

    public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository,
        IMapper mapper,
        ILogger<TipoProductoAppService> logger )
    {
        this.tipoProductoRepository = tipoProductoRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {
       
        logger.LogInformation("Crear Tipo Producto");

        //Mapeo Dto => Entidad. (Manual)
        //var tipoProducto = new TipoProducto();
        //tipoProducto.Nombre = tipoProductoDto.Nombre;
 
        //Automatico
        var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);

        //Persistencia objeto
        tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        //var tipoProductoCreada = new TipoProductoDto();
        //tipoProductoCreada.Nombre = tipoProducto.Nombre;
        //tipoProductoCreada.Id = tipoProducto.Id;

         var tipoProductoCreada = mapper.Map<TipoProductoDto>(tipoProducto);

 
        return tipoProductoCreada;
    }

    public Task<bool> DeleteAsync(int marcaId)
    {
        throw new NotImplementedException();
    }

    public ListaPaginada<TipoProductoDto> GetAll(int limit = 10, int offset = 0)
    {
        throw new NotImplementedException();
    }

    public Task<TipoProductoDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, TipoProductoCrearActualizarDto marca)
    {
        throw new NotImplementedException();
    }
}

