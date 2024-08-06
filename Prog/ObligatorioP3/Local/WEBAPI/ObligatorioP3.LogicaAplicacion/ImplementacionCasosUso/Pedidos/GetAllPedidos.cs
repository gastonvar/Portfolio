using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos
{
    public class GetAllPedidos : IGetAllPedidos
    {
        private IRepositorioPedido _repositorioPedido;
        public GetAllPedidos(IRepositorioPedido repo)
        {
            _repositorioPedido = repo;
        }

        /// <summary>
        /// Manda a traer del repositorio todos los pedidos
        /// </summary>
        /// <returns>Devuelve un IEnumerable de pedidos en formato DTO</returns>
        public IEnumerable<PedidoListarDto> Ejecutar()
        {
            var pedidosOrigen = _repositorioPedido.GetAll();
            if (pedidosOrigen == null || pedidosOrigen.Count() == 0)
            {
                throw new Exception("No hay pedidos registrados");
            }
            return PedidoMappers.FromLista(pedidosOrigen);
        }
    }
}
