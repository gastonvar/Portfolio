using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Clientes
{
    public interface IFiltrarClientes
    {
        public IEnumerable<ClienteListarDto> FiltrarXTexto(string txt);
        public IEnumerable<ClienteListarDto> FiltrarXMonto(decimal money);
    }
}
