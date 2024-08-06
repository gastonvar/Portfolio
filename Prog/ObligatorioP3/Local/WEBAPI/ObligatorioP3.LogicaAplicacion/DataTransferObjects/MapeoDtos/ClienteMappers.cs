using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    public class ClienteMappers
    {

        public static ClienteListarDto ToDto(Cliente cli)
        {
            if (cli == null) throw new ArgumentNullException("Error, cli nulo");
            return new ClienteListarDto()
            {
                Id = cli.Id,
                RazonSocial = cli.RazonSocial,
                RUT = cli.RUT,
                Calle = cli.Direccion.Calle,
                Ciudad = cli.Direccion.Ciudad,
                Numero = cli.Direccion.Numero,
                Distancia = cli.Direccion.Distancia
            };
        }

        public static IEnumerable<ClienteListarDto> FromLista(IEnumerable<Cliente> clientesOrigen)
        {
            if (clientesOrigen == null || clientesOrigen.Count() == 0) return null;
            return clientesOrigen.Select(cliente => ToDto(cliente));
        }
    }
}
