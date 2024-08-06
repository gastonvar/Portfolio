using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos
{
    public interface IGetPedido
    {
        public PedidoListarDto GetById(int? id);
    }
}
