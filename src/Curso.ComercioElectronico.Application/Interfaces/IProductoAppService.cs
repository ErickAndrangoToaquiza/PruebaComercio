namespace Curso.ComercioElectronico.Application;



public interface IProductoAppService
{
    Task<ProductoDto> GetByIdAsync(int id);

   
    ListaPaginada<ProductoDto> GetAll(int limit=10,int offset=0);


    Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input);


    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto marca);

    Task UpdateAsync (int id, ProductoCrearActualizarDto marca);

    Task<bool> DeleteAsync(int marcaId);
}
