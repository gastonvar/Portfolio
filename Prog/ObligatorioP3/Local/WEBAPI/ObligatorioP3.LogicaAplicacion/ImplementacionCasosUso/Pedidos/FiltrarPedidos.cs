using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos
{
    public class FiltrarPedidos : IFiltrarPedidos
    {
        private IRepositorioPedido _repositorioPedido;
        public FiltrarPedidos(IRepositorioPedido repo)
        {
            _repositorioPedido = repo;
        }

        /// <summary>
        /// Llama al método del repositorio que filtra los pedidos no entregados según la fecha de emisión
        /// </summary>
        /// <param name="date">Fecha de emisión</param>
        /// <returns>IEnumerable de pedidos en formato DTO</returns>
        public IEnumerable<PedidoListarDto> Filtrar(DateTime date)
        {
            var PedidosFiltrados = _repositorioPedido.Filtrar(date);

            return PedidoMappers.FromLista(PedidosFiltrados);
        }
    }
}
