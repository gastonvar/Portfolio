using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioParametro : IRepositorio<Parametro>
    {
        public Parametro GetParametro(string nombre);
    }
}
