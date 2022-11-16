namespace Curso.ComercioElectronico.Domain;

public interface ICarroRepository : IRepository<Carro,Guid>{
     Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, int idExcluir);

}