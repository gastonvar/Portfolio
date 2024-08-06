using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    public class UsuarioMappers
    {
        //FromDTO es una sobrecarga de metodo para capturar cualquier tipo de DTO de Usuario y devolver un objeto Usuario
        public static Usuario FromDTO(UsuarioAltaDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Usuario usuario = new Usuario(dto.Email, dto.Nombre, dto.Apellido, dto.Contrasena, dto.Rol);
            usuario.Contrasena.ContrasenaEncriptada = Usuario.EncriptarContraseña(dto.Contrasena);
            return usuario;
        }

        public static Usuario FromDTO(UsuarioModificacionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var usuario = new Usuario(dto.Id, dto.Email, dto.Nombre, dto.Apellido, dto.Contrasena,dto.Rol);
            usuario.Contrasena.ContrasenaEncriptada = Usuario.EncriptarContraseña(dto.Contrasena);
            return usuario;
        }

        public static UsuarioListarDto ToDto(Usuario usu)
        {
            if (usu == null) throw new ArgumentNullException("Error, usuario nulo");
            return new UsuarioListarDto()
            {
                Id = usu.Id,
                Email = usu.Email.ValorEmail,
                Nombre = usu.NombreCompleto.Nombre,
                Apellido = usu.NombreCompleto.Apellido,
                Contrasena = usu.Contrasena.ContrasenaNoEncriptada,
                ContrasenaEncriptada = usu.Contrasena.ContrasenaEncriptada,
                Rol = usu.Rol
            };
        }


        public static IEnumerable<UsuarioListarDto> FromLista(IEnumerable<Usuario> usuarios)
        {
            if (usuarios == null || usuarios.Count() == 0) throw new ArgumentNullException("No existen usuarios registrados");
            return usuarios.Select(usuario => ToDto(usuario));
        }
    }
}
