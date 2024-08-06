using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class GetUsuario:IGetUsuario
    {
        private IRepositorioUsuario _repositorioUsuario;

        public GetUsuario(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        public UsuarioListarDto GetById(int? id)
        {
            var usuario = _repositorioUsuario.GetById(id);
            if (usuario == null) throw new UsuarioNoValidoException("Error, no existe ningun usuario con esa id");
            var usuarioDto = UsuarioMappers.ToDto(usuario);
            return usuarioDto;
        }
    }
}
