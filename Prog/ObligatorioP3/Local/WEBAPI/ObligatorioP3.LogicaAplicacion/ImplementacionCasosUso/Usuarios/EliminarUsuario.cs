using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class EliminarUsuario:IEliminarUsuario
    {
        private IRepositorioUsuario _repositorioUsuario;
        public EliminarUsuario(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        public void Ejecutar(int id)
        {
            _repositorioUsuario.Remove(id);
        }
    }
}
