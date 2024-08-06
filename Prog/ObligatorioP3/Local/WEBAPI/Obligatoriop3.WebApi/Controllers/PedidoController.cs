using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obligatoriop3.WebApi.Controllers
{
    /// <summary>
    /// Controller para manejar las operaciones con pedidos
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]

    public class PedidoController : ControllerBase
    {
        private IRepositorioPedido _repoPedidos = new RepositorioPedidoEF(new ObligatorioP3Context());
        private IGetAllPedidosAnulados _getAllPedidosAnulados;
        public PedidoController(IRepositorioPedido repo, IGetAllPedidosAnulados getallanulados)
        {
            _repoPedidos = repo;
            _getAllPedidosAnulados = getallanulados;
        }

        /// <summary>
        /// Get de los pedidos anulados
        /// </summary>
        /// <returns>Si no encuentra ninguno devuelve 404, de otro modo devuelve 200, si ocurre una excepcion devuelve 500</returns>
        /// <response code="200">Hay pedidos</response>
        /// <response code="404">No hay pedidos</response>
        /// <response code="500">Excepcion interna</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PedidoListarDto>> Get()
        {
            try
            {
                var articulos = _getAllPedidosAnulados.Ejecutar();
                if (!articulos.Any())
                {
                    return NotFound();
                }
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
