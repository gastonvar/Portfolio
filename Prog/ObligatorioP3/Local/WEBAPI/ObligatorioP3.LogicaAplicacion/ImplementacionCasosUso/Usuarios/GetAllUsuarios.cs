using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class GetAllUsuarios:IGetAllUsuarios
    {
        private IRepositorioUsuario _repositorioUsuario;
        public GetAllUsuarios(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        public IEnumerable<UsuarioListarDto> Ejecutar()
        {
            var usuariosOrigen = _repositorioUsuario.GetAll();
            if(usuariosOrigen == null || usuariosOrigen.Count() == 0)
            {
                throw new UsuarioNoValidoException("No hay usuarios registrados");
            }
            return UsuarioMappers.FromLista(usuariosOrigen);
        }
    }
}
