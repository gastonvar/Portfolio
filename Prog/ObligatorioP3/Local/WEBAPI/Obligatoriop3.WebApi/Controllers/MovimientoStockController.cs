using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obligatoriop3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoStockController : ControllerBase
    {
        private IAltaMovimientoStock _altaMovimiento;
        private IListarResumenMovimientos _listarResumenMovimientos;
        private IFiltrarMovimientos _filtrarMovimientos;

        public MovimientoStockController(IAltaMovimientoStock altaMovimientoStock, IListarResumenMovimientos listarResumenMovimientos, IFiltrarMovimientos filtrarMovimientos)
        {
            _altaMovimiento = altaMovimientoStock;
            _listarResumenMovimientos = listarResumenMovimientos;
            _filtrarMovimientos = filtrarMovimientos;
        }

        // POST api/<MovimientoStockController>
        /// <summary>
        /// Registra un nuevo movimiento.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="201">Fue creado correctamente</response>
        /// <response code="400">Si el movimiento recibido es null o se produce un error de validación</response>    
        /// <response code="401">Si no se pudo autenticar el usuario</response> 
        /// <response code="500">Si se produce una excepción interna, por ejemplo si la base no existiera.</response>           


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "encargado")]
        [HttpPost("")]
        public IActionResult Post([FromBody] MovimientoStockAltaDTO dto)
        {
            if (dto == null) return BadRequest("No se recibió un movimiento");
            try
            {
                var user = HttpContext.User;
                if(!user.Identity.IsAuthenticated)
                    return Unauthorized("Debes loguearte");

                if (user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)==null)
                    return Unauthorized("No tienes el rol de encargado");
                _altaMovimiento.Ejecutar(dto);
                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MovimientoStockController>
        /// <summary>
        /// Agrupa los movimientos por año
        /// </summary>
        /// <returns>Movimientos agrupados por año</returns>
        /// <response code="200">Existen movimientos agrupados</response>
        /// <response code="401">Si no se pudo autenticar el usuario</response> 
        /// <response code="500">Si se produce una excepción interna, por ejemplo si la base no existiera.</response>     
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "encargado")]
        [HttpGet]
        public ActionResult<IEnumerable<MovimientoListarAgrupadoDTO>> ListarAgrupados()
        {
            try
            {
                var user = HttpContext.User;
                if (!user.Identity.IsAuthenticated)
                    return Unauthorized("Debes loguearte");

                if (user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role) == null)
                    return Unauthorized("No tienes el rol de encargado");
                IEnumerable<MovimientoListarAgrupadoDTO>  resumenMovimientos = _listarResumenMovimientos.Ejecutar();
                return Ok(resumenMovimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<MovimientoStockController>
        /// <summary>
        /// Obtiene los movimientos filtrados por tipo y articulo
        /// </summary>
        /// <param name="idArticulo">Id del articulo</param>
        /// <param name="idTipo">Id del tipo</param>
        /// <returns></returns>
        /// <response code="200">Existen movimientos</response>
        /// <response code="400">Si algun parametro recibido es null</response>    
        /// <response code="404">Si no se encuentran movimientos</response>    
        /// <response code="401">Si no se pudo autenticar el usuario</response> 
        /// <response code="500">Si se produce una excepción interna, por ejemplo si la base no existiera.</response>     
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "encargado")]
        [HttpGet("{idArticulo}/{idTipo}/{pagina}")]
        public ActionResult<MovimientoListarDTO> GetFiltrados(int idArticulo, int idTipo, int pagina)
        {
            if (idArticulo == null || idTipo == null) return BadRequest();
            try
            {
                var user = HttpContext.User;
                if (!user.Identity.IsAuthenticated)
                    return Unauthorized("Debes loguearte");

                if (user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role) == null)
                    return Unauthorized("No tienes el rol de encargado");
                var movimientosFiltrados = _filtrarMovimientos.Ejecutar(idArticulo, idTipo, pagina);
                if (movimientosFiltrados == null || movimientosFiltrados.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(movimientosFiltrados);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
