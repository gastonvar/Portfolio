using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos
{
    public interface IGetAllPedidos
    {
        public IEnumerable<PedidoListarDto> Ejecutar();
    }
}
