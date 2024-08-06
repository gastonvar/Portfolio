using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Clientes;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Clientes
{
    public class FiltrarClientes : IFiltrarClientes
    {
        private IRepositorioCliente _repositorioCliente;

        public FiltrarClientes(IRepositorioCliente repo)
        {
            _repositorioCliente = repo;
        }
        /// <summary>
        /// Llama al metodo filtrarXTexto del repositorio
        /// </summary>
        /// <param name="txt">Texto a filtrar por la razon social</param>
        /// <returns>IEnumerable de ClienteListarDto para mostrar en la view</returns>
        public IEnumerable<ClienteListarDto> FiltrarXTexto(string txt)
        {
            var clientesFiltrados = _repositorioCliente.FiltrarXTexto(txt);

            return ClienteMappers.FromLista(clientesFiltrados);
        }

        /// <summary>
        /// Llama al metodo filtrarXMonto del repositorio
        /// </summary>
        /// <param name="txt">Texto a filtrar por la razon social</param>
        /// <returns>IEnumerable de ClienteListarDto para mostrar en la view</returns>
        public IEnumerable<ClienteListarDto> FiltrarXMonto (decimal money)
        {
            var clientesFiltrados = _repositorioCliente.FiltrarXMonto(money);
            return ClienteMappers.FromLista(clientesFiltrados);
        }
    }
}
