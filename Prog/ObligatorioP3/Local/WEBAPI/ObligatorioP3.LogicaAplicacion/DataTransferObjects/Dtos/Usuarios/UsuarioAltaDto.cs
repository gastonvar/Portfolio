using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios
{
    public class UsuarioAltaDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [Required]
        public string? ContrasenaEncriptada { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
