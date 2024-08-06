using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obligatoriop3.WebApi.DTOS.UsuariosDTO;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using Obligatoriop3.WebApi.UtilidadesJWT;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Obligatoriop3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ILoginAPI _login;

        public UsuarioController(ILoginAPI login) {
            _login = login;
        }

        /// <summary>
        /// Login de usuarios JWT
        /// </summary>
        /// <param name="usr">DTO que contiene email y contraseña</param>
        /// <returns>Devuelve el token generado, el rol y el email</returns>
        /// <response code="200">Retorna token, rol y email</response>
        /// <response code="401">Falla en credenciales</response>    
        /// <response code="500">Si se produce una excepción no contemplada, por ejemplo si la base no existiera.</response>   

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UsuarioLoginDTO usr) {
            try
            {
                Usuario? usu = _login.Ejecutar(usr.Email, usr.Contrasena);
                if (usu == null) return Unauthorized("Credenciales incorrectas");
                if (!usu.Rol.Equals("encargado")) return Unauthorized("NO sos encargado, no te corresponde entrar aca.");
                string token = ManejadowJWT.GenerarToken(usu.Email.ValorEmail, usu.Rol);
                return Ok(new { Token = token, Rol = usu.Rol, Email = usu.Email.ValorEmail });
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
