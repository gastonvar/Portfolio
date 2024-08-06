using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos
{
    public class AnularPedido : IAnularPedido
    {
        private IRepositorioPedido _repositorioPedido;
        public AnularPedido(IRepositorioPedido repo)
        {
            _repositorioPedido = repo;
        }

        /// <summary>
        /// Manda a llamar el método Anular en el repositorio
        /// </summary>
        /// <param name="id">Id del pedido que se quiere anular</param>
        public void Ejecutar(int id)
        {
            _repositorioPedido.Anular(id);
        }
    }
}
