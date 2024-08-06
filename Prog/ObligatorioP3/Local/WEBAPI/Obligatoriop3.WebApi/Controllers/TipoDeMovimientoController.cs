using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obligatoriop3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeMovimientoController : ControllerBase
    {
        private IAltaMovimiento _altaTipo;
        private IGetTipoDeMovimiento _buscarTipo;
        private IEditarTipoDeMovimiento _editarTipo;
        private IBorrarTipoDeMovimiento _borrarTipo;

        public TipoDeMovimientoController(IAltaMovimiento altaTipo, IGetTipoDeMovimiento buscarTipo, IEditarTipoDeMovimiento editarTipo, IBorrarTipoDeMovimiento borrarTipo)
        {
            _altaTipo = altaTipo;
            _buscarTipo = buscarTipo;
            _editarTipo = editarTipo;
            _borrarTipo = borrarTipo;
        }

        // GET: api/<TipoDeMovimientoController>
        /// <summary>
        /// Lista todos los tipos de movimiento.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// <response code="200">Retorna la lista</response>
        /// <response code="404">Si no hay tipos</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>    

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]

        public ActionResult<IEnumerable<TipoDeMovimientoListarDTO>> Get()
        {
            try
            {
                var tipos = _buscarTipo.GetAll();
                if (tipos == null || !tipos.Any()) {
                    return NotFound();
                }
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<TipoDeMovimientoController>/5
        /// <summary>
        /// Obtiene el tipo de movimiento con la id ingresada.
        /// </summary>
        /// <param name="id">Id a buscar</param>
        /// <returns></returns>
        /// <response code="200">Retorna el item</response>
        /// <response code="404">Si no se encuentra</response>    
        /// <response code="400">Si hay un error relacionado a la entidad</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>    
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "GetTipoById")]
        public ActionResult<TipoDeMovimientoListarDTO> Get(int id)
        {
            try
            {
                var tipo = _buscarTipo.GetById(id);
                if (tipo == null)
                {
                    return NotFound();
                }
                return Ok(tipo);
            }
            catch (TipoDeMovimientoNoValidoException ex) { return BadRequest(ex.Message); }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // POST api/<TipoDeMovimientoController>
        /// <summary>
        /// Registra un nuevo tipodemovimiento.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <response code="201">Retorna el item creado y establece el Header Location a la ubicación del GetById</response>
        /// <response code="400">Si el docente recibido es null o se produce un error de validación</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>    

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [HttpPost("")]
        public IActionResult Post([FromBody] TipoDeMovimientoAltaDTO dto)
        {
            if (dto == null) return BadRequest("Debe indicar un tipo de movimiento dto");
            try
            {
                _altaTipo.Ejecutar(dto);
                return CreatedAtRoute("GetTipoById", new { id = dto.Id }, dto);

            }
            catch (TipoDeMovimientoNoValidoException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex) { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<TipoDeMovimientoController>/5
        /// <summary>
        /// Edita un tipo de movimiento.
        /// </summary>
        /// <param name="dto">Datos nuevos</param>
        /// <param name="id">Id del tipo a editar</param>
        /// <returns></returns>
        /// <response code="200">Se edita</response>
        /// <response code="400">Si el tipo recibido es null o se produce un error de validación</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>    

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TipoDeMovimientoModificacionDto dto)
        {
            if (dto==null)
            {
                return BadRequest("Debe indicar un tipo de movimiento dto");
            }
            try
            {
                _editarTipo.Ejecutar(id,dto);
                return Ok(dto);
            }
            catch (TipoDeMovimientoNoValidoException ex)
            {

                return BadRequest(ex.Message);
            }
            catch(Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError,ex.Message); }


        }

        // DELETE api/<TipoDeMovimientoController>/5
        /// <summary>
        /// Elimina un tipodemovimiento.
        /// </summary>
        /// <param name="id">Id del tipo a borrar</param>
        /// <returns></returns>
        /// <response code="204">Si se elimina exitosamente</response>
        /// <response code="400">Si se produce un error de validación</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>    

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _borrarTipo.Ejecutar(id);
                return NoContent();
            }
            catch (TipoDeMovimientoNoValidoException ex) { return BadRequest(); }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
