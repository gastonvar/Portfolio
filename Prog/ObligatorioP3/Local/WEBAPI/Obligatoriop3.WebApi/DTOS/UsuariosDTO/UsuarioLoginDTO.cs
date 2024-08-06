
namespace Obligatoriop3.WebApi.DTOS.UsuariosDTO
{
    public record UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Contrasena { get; set; }
    }
}
