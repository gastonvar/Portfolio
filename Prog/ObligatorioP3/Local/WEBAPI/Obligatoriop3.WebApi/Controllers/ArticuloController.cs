using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos;
using Microsoft.AspNetCore.Authorization;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace Obligatoriop3.WebApi.Controllers
{
    ///<summary>Controller para manejar las operaciones con articulos</summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]

    public class ArticuloController : ControllerBase
    {
        private IGetAllArticulos _getAllArticulos;
        private IGetArticulosConMovimientosSegunFechas _getArticulosConMovimientosSegunFechas;
        public ArticuloController(IGetAllArticulos getall, IGetArticulosConMovimientosSegunFechas getArticulosConMovimientosSegunFechas)
        {
            _getAllArticulos = getall;
            _getArticulosConMovimientosSegunFechas = getArticulosConMovimientosSegunFechas;
        }

        /// <summary>
        /// Get de todos los articulos
        /// </summary>
        /// <returns>Si no encuentra ninguno devuelve 404, de otro modo devuelve 200, si ocurre una excepcion devuelve 500</returns>
        /// <response code="200">Hay articulos</response>
        /// <response code="404">No hay articulos</response>
        /// <response code="500">Excepcion interna</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticuloListarDto>> Get()
        {
            try
            {
                //El metodo getall en el repositorio los devuelve ordenados
                var articulos = _getAllArticulos.Ejecutar();
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

        /// <summary>
        /// Recibe dos fechas y en base a ellas filtra los articulos que tuvieron movimientos entre ellas
        /// </summary>
        /// <param name="fecha1">Fecha inicio busqueda</param>
        /// <param name="fecha2">Fecha final busqueda</param>
        /// <returns>Devuelve los articulos con movimientos entre esas fechas</returns>
        /// <response code="200">Hay articulos</response>
        /// <response code="404">No hay articulos</response>
        /// <response code="500">Excepcion interna</response>
        [HttpGet("{fecha1}/{fecha2}/{pagina}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "encargado")]
        public ActionResult<IEnumerable<ArticuloListarDto>> GetArticulosConMovimientosEnFechas(string fecha1, string fecha2, int pagina)
        {
            try
            { 
                string fecha1Decodificada = HttpUtility.UrlDecode(fecha1);
                string fecha2Decodificada = HttpUtility.UrlDecode(fecha2);
                string fecha1StringReparada = fecha1Decodificada.Replace('-','/');
                string fecha2StringReparada = fecha2Decodificada.Replace('-','/');
                DateTime fecha1FromString = DateTime.Parse(fecha1StringReparada);
                DateTime fecha2FromString = DateTime.Parse(fecha2StringReparada);
                var articulos = _getArticulosConMovimientosSegunFechas.Ejecutar(fecha1FromString, fecha2FromString, pagina);
                if (!articulos.Any()) return NotFound();
                return Ok(articulos);
            }
            catch (Exception ex)
            {

                return StatusCode(500,ex.Message);
            }
        }
    }
}
