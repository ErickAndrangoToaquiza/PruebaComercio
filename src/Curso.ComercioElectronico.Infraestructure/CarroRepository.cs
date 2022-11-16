using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Infraestructure;

public class CarroRepository : EfRepository<Carro,Guid>, ICarroRepository
{
    public CarroRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    Task<bool> ICarroRepository.ExisteNombre(string nombre)
    {
        throw new NotImplementedException();
    }

    Task<bool> ICarroRepository.ExisteNombre(string nombre, int idExcluir)
    {
        throw new NotImplementedException();
    }
}