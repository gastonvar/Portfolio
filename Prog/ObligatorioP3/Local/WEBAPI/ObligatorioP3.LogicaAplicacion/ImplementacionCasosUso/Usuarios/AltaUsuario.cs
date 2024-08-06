using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class AltaUsuario : IAltaUsuario
    {
        private IRepositorioUsuario _repositorioUsuario;
        
        public AltaUsuario(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        /// <summary>
        /// Mappea el UsuarioAltaDto en un Usuario, encripta su contraseña,llama al metodo Add del repositorioEF de Usuario.
        /// </summary>
        /// <param name="dto"></param>
        public void Ejecutar(UsuarioAltaDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Usuario nulo");
            }

            if (_repositorioUsuario.GetByEmail(dto.Email) == null)
            {
                Usuario usuario = UsuarioMappers.FromDTO(dto);
                usuario.Contrasena.ContrasenaEncriptada = Usuario.EncriptarContraseña(usuario.Contrasena.ContrasenaNoEncriptada);
                _repositorioUsuario.Add(usuario);
            }
            else
            {
                throw new Exception("Ya existe un usuario con ese email en el sistema.");
            }
                

        }
        

    }
}
