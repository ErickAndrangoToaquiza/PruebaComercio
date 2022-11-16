namespace Curso.ComercioElectronico.Application;



public interface ICarroAppService
{
    Task<CarroDto> GetByIdAsync(Guid id);

    ListaPaginada<CarroDto> GetAll(int limit=10,int offset=0);

    ListaPaginada<CarroDto> GetByClientIdAll(int clientId, int limit=10,int offset=0);


    Task<CarroDto> CreateAsync(CarroCrearDto carro);

    Task UpdateAsync (Guid id, CarroActualizarDto carro);

    Task<bool> AnularAsync(Guid carroId);
}