using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class ModificarUsuario:IModificarUsuario
    {
        private IRepositorioUsuario _repositorioUsuario;
        public ModificarUsuario(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        /// <summary>
        /// Caso de uso de modificar, se hardcodea el rol y email pues son inmutables desde la creacion del usuario original.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuModificado"></param>
        public void Ejecutar(int id, UsuarioModificacionDto usuModificado)
        {
            Usuario usuarioSinModificar = _repositorioUsuario.GetById(id);
            usuModificado.Rol = usuarioSinModificar.Rol;
            usuModificado.Email = usuarioSinModificar.Email.ValorEmail;
            Usuario usuario = UsuarioMappers.FromDTO(usuModificado);
            _repositorioUsuario.Update(id, usuario);
        }
    }
}
