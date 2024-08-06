using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioP3.Web.Models.UsuariosModels
{
    public class UsuarioLoginModel
    {
        [Required]
        
        public string Email { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
