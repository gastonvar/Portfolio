using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCliente:IRepositorio<Cliente>
    {
        public IEnumerable<Cliente> FiltrarXTexto(string txt);
        public IEnumerable<Cliente> FiltrarXMonto(decimal money);
    }
}
