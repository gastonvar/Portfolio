using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static ObligatorioP3.AccesoDatos.EF.RepositorioMovimientoEF;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    public class MovimientoStockMapper
    {
        public static MovimientoStock FromDTO(MovimientoStockAltaDTO dto, Articulo articulo, TipoDeMovimiento tipo, Usuario usuario)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            MovimientoStock movimientoStock = new MovimientoStock(DateTime.Now, dto.IdArticulo, articulo, dto.IdTipo, tipo, usuario.Id, usuario, dto.Cantidad);
            return movimientoStock;
        }

        public static MovimientoListarDTO ToDto(MovimientoStock movimientoStock)
        {
            if (movimientoStock == null) throw new ArgumentNullException("Error, movimiento de stock nulo");
            return new MovimientoListarDTO()
            {
                Id = movimientoStock.Id,
                Fecha = movimientoStock.Fecha,
                IdArticulo = movimientoStock.Articulo.Id,
                IdUsuario = movimientoStock.Usuario.Id,
                Cantidad = movimientoStock.Cantidad,
                IdTipoMovmiento = movimientoStock.Tipo.Id,
                NombreTipoMovimiento = movimientoStock.Tipo.Nombre,
                Aumento = movimientoStock.Tipo.Aumento,
                Coeficiente = movimientoStock.Tipo.Coeficiente
            };
        }

        public static MovimientoListarAgrupadoDTO ToDtoAgrupado(Object movimientoOrigen)
        {
            var anoProperty = movimientoOrigen.GetType().GetProperty("Ano");
            var movimientoCantidadProperty = movimientoOrigen.GetType().GetProperty("MovimientoCantidad");
            var totalProperty = movimientoOrigen.GetType().GetProperty("Total");

            if (anoProperty == null || movimientoCantidadProperty == null || totalProperty == null)
            {
                throw new InvalidOperationException("El objeto no tiene las propiedades esperadas.");
            }

            var ano = (string)anoProperty.GetValue(movimientoOrigen);
            var movimientoCantidad = (IEnumerable<dynamic>)movimientoCantidadProperty.GetValue(movimientoOrigen);
            var total = (int)totalProperty.GetValue(movimientoOrigen);

            var movimientoConvertido = new MovimientoListarAgrupadoDTO
            {
                Ano = ano,
                MovimientoCantidad = movimientoCantidad.Select(mc => new MovimientoCantidadDto
                {
                    Nombre = (string)mc.GetType().GetProperty("Nombre").GetValue(mc),
                    Cantidad = (int)mc.GetType().GetProperty("Cantidad").GetValue(mc)
                }).ToList(),
                Total = total
            };

            return movimientoConvertido;
        }

        public static IEnumerable<MovimientoListarDTO> FromLista(IEnumerable<MovimientoStock> movimientosOrigen)
        {
            if (movimientosOrigen == null || movimientosOrigen.Count() == 0) throw new ArgumentNullException("No existen movimientos de stock registrados");
            return movimientosOrigen.Select(MovimientoStock => ToDto(MovimientoStock));
        }

        public static IEnumerable<MovimientoListarAgrupadoDTO> FromListaAgrupado(IEnumerable<Object> movimientosOrigen)
        {
            if (movimientosOrigen == null || movimientosOrigen.Count() == 0) throw new ArgumentNullException("No existen movimientos de stock registrados");
            var movimientosConvertidos = movimientosOrigen.Select(MovimientoStock => ToDtoAgrupado(MovimientoStock));
            return movimientosConvertidos;
        }
    }
}
