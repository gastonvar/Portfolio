using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos
{
    public class GetPedido : IGetPedido
    {
        private IRepositorioPedido _repositorioPedido;
        public GetPedido(IRepositorioPedido repo)
        {
            _repositorioPedido = repo;
        }

        /// <summary>
        /// Manda a traer del repositorio 1 pedido que coincida con la id que recibe
        /// </summary>
        /// <param name="id">id del pedido que se quiere traer</param>
        /// <returns>El pedido en formato DTO</returns>
        public PedidoListarDto GetById(int? id)
        {
            var pedido = _repositorioPedido.GetById(id);
            if (pedido == null) throw new Exception("Error, no existe ningun pedido con esa id");
            var pedidoDto = PedidoMappers.ToDto(pedido);
            return pedidoDto;
        }
    }
}
