using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    internal class PedidoMappers
    {
        public static Pedido FromDTOcomun(PedidoAltaDto dto, decimal iva, decimal recargo)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Cliente cliente = new Cliente(dto.ClienteDto.Id, dto.ClienteDto.RazonSocial, dto.ClienteDto.RUT, dto.ClienteDto.Calle, dto.ClienteDto.Ciudad, dto.ClienteDto.Numero, dto.ClienteDto.Distancia);
            List<ArticulosPedido> lineas = new List<ArticulosPedido>();
            foreach (var item in dto.LineasDto)
            {
                ArticulosPedido lineaConvertida = ArticulosPedidoMappers.FromDTO(item);
                lineas.Add(lineaConvertida);
            }
            Pedido pedido = new PedidoComun(dto.FechaEntrega, cliente, lineas, iva, recargo);
            return pedido;
        }

        public static Pedido FromDTOExpress(PedidoAltaDto dto, decimal iva, int plazoExpress, decimal recargo)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Cliente cliente = new Cliente(dto.ClienteDto.Id, dto.ClienteDto.RazonSocial, dto.ClienteDto.RUT, dto.ClienteDto.Calle, dto.ClienteDto.Ciudad, dto.ClienteDto.Numero, dto.ClienteDto.Distancia);
            List<ArticulosPedido> lineas = new List<ArticulosPedido>();
            foreach (var item in dto.LineasDto)
            {
                ArticulosPedido lineaConvertida = ArticulosPedidoMappers.FromDTO(item);
                lineas.Add(lineaConvertida);
            }
            Pedido pedido = new PedidoExpress(dto.FechaEntrega, cliente, lineas, iva, plazoExpress, recargo);
            return pedido;
        }

        public static PedidoListarDto ToDto(Pedido ped)
        {
            if (ped == null) throw new ArgumentNullException("Error, pedido nulo");
            return new PedidoListarDto()
            {
                Id = ped.Id,
                FechaEntrega = ped.FechaEntrega,
                FechaEmision = ped.Fecha,
                ClienteDto = ClienteMappers.ToDto(ped.Cliente),
                PrecioFinal = ped.PrecioFinal,
                Anulado = ped.Anulado,
            };
        }

        public static IEnumerable<PedidoListarDto> FromLista(IEnumerable<Pedido> PedidosOrigen)
        {
            if (PedidosOrigen == null || PedidosOrigen.Count() == 0) throw new ArgumentNullException("No existen pedidos de esa fecha");
            return PedidosOrigen.Select(Pedido => ToDto(Pedido));
        }
    }
}
