

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{

    private readonly IClienteAppService clienteAppService;

    public ClienteController(IClienteAppService clienteAppService)
    {
        this.clienteAppService = clienteAppService;
    }

     

    [HttpPost]
    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteCrearActualizarDto)
    {

        return await clienteAppService.CreateAsync(clienteCrearActualizarDto);

    }
 
    //TODO: Agregar las otras capacidades del api de clientes..

}