using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Clientes;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Clientes
{
    public class GetAllClientes:IGetAllClientes
    {
        private IRepositorioCliente _repositorioCliente;
        public GetAllClientes(IRepositorioCliente repo)
        {
            _repositorioCliente= repo;
        }
        /// <summary>
        /// Ejecuta el metodo GetAll del repositorio y mapea a los clientes a una lista de ClientesDTO
        /// </summary>
        /// <returns>Una lista de ClienteListarDto hecha a partir de todos los clientes del sistema</returns>
        public IEnumerable<ClienteListarDto> Ejecutar()
        {
            var clientesOrigen = _repositorioCliente.GetAll();
            if (clientesOrigen == null || clientesOrigen.Count() == 0)
            {
                throw new Exception("No hay clientes registrados");
            }
            return ClienteMappers.FromLista(clientesOrigen);
        }
    }
}
