
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class CarroAppService : ICarroAppService
{

    private readonly ICarroRepository carroRepository;
    private readonly IProductoAppService productoAppService;
    private readonly ILogger<CarroAppService>logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CarroAppService (
        ICarroRepository carroRepository,
        IProductoAppService productoAppService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CarroAppService>logger)
        
        {
            this.carroRepository =carroRepository;
            this.productoAppService =productoAppService;
            this.logger =logger;
            this.unitOfWork =unitOfWork;
            this.mapper = mapper;


        }


    public async Task<CarroDto> CreateAsync(CarroCrearDto carroCrearDto)
    {
       logger.LogInformation ("crear Carrito");
       
       var carro =mapper.Map<Carro>(carroCrearDto);
       carro =await carroRepository.AddAsync(carro);
       await carroRepository.UnitOfWork.SaveChangesAsync();
       var carroCreado = mapper.Map<CarroCrearDto>(carroCrearDto);
       
        return await CreateAsync (carroCreado);
    }

    public ListaPaginada<CarroDto> GetAll(int limit = 10, int offset = 0)
    {
        var consulta = carroRepository.GetAllIncluding(x=>x.Items,
                                    x =>x.Items);

        var total =consulta.Count();
        var consultaListaCarroDto =consulta.Skip(offset)
                                    .Take(limit).Select(x=> new CarroDto(){

                                        Id =x.Id,
                                        Total = x.Total,
                                        ClienteId =x.ClienteId,
                                        Observaciones =x.Observaciones,
                                        Items = x.Items.Select(item =>new CarroItemDto(){
                                            Cantidad = item.Cantidad,
                                            CarroId =item.CarroId,
                                            Precio=item.Precio,
                                            Product =item.Product.Nombre,
                                            ProductId =item.ProductId
                                        }).ToList()

                                    });
        var resultado =new ListaPaginada<CarroDto>();
        resultado.Total =total;
        resultado.Lista =consultaListaCarroDto.ToList();

        
        return resultado;
    }

    public ListaPaginada<CarroDto> GetByClientIdAll(int clientId, int limit = 10, int offset = 0)
    {
        var carroList = carroRepository.GetAll();
        var carroListDto =from m in carroList
                        select new CarroDto(){
                            Id =m.Id,
                            ClienteId = m.ClienteId
                        };
         var resultado =new ListaPaginada<CarroDto>();
        resultado.Lista =carroListDto.ToList();

        
        return resultado;
       
        }


    public Task<CarroDto> GetByIdAsync(Guid id)
    {
        var consulta =carroRepository.GetAllIncluding (x=>x.Cliente,x=>x.Items);
        consulta =consulta.Where(x=>x.Id ==id);

        var consultaCarroDto =consulta 
                                .Select(x=>new CarroDto(){
                                    Id=x.Id,
                                    Cliente =x.Cliente.Nombres,
                                    ClienteId = x.ClienteId,
                                    Fecha =x.Fecha,
                                    Total =x.Total,
                                    Items =x.Items.Select(item =>new CarroItemDto(){
                                        Cantidad=item.Cantidad,
                                        Id= item.Id,
                                        CarroId =item.CarroId,
                                        Precio =item.Precio,
                                        ProductId = item.ProductId,
                                        Product = item.Product.Nombre


                                    }).ToList()


                                });
        
        return Task.FromResult(consultaCarroDto.SingleOrDefault());
    }

    public async Task UpdateAsync(Guid id, CarroActualizarDto carroDto)
    {
        var carro = await carroRepository.GetByIdAsync(id);
        if (carro == null ){
            throw new ArgumentException($"El ID del carrito {id},no existe");
        }
        var existeNombre = await carroRepository.ExisteNombre(carroDto.Observaciones);
        if (existeNombre){

        throw new ArgumentException($"Ya existe ese carrito {carroDto.Estado}");}
    }
     public async Task<bool> AnularAsync(Guid carroId)
    {
        var carro = await carroRepository.GetByIdAsync(carroId);
        if (carro ==null ){
            throw new ArgumentException ($"El id del carro {carroId},se anulara");
        }
        carroRepository.Delete (carro);
        await carroRepository.UnitOfWork.SaveChangesAsync();
        return true;
        
        
    }
}