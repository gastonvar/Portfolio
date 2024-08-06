using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuario:IRepositorio<Usuario>
    {
        public Usuario GetByEmail(string email);
    }
}
