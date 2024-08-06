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
    public class GetCliente: IGetCliente
    {
        private IRepositorioCliente _repositorioCliente;
        public GetCliente(IRepositorioCliente repo)
        {
            _repositorioCliente= repo;
        }
        public ClienteListarDto Ejecutar(int? id)
        {
            var clienteOrigen = _repositorioCliente.GetById(id);
            if (clienteOrigen == null)
            {
                throw new Exception("Existe el Cliente");
            }
            return ClienteMappers.ToDto(clienteOrigen);
        }
    }
}
