using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    internal class ArticulosPedidoMappers
    {
        public static ArticulosPedido FromDTO(ArticulosPedidoDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Articulo articulo = new Articulo(dto.ArticuloListarDto.Id, dto.ArticuloListarDto.Nombre, dto.ArticuloListarDto.Descripcion, dto.ArticuloListarDto.Codigo, dto.ArticuloListarDto.Precio, dto.ArticuloListarDto.Stock);
            ArticulosPedido articulosPedido = new ArticulosPedido(dto.PedidoId, articulo, dto.Unidades, dto.PrecioUnitario);
            return articulosPedido;
        }

        public static ArticulosPedidoDto ToDto(ArticulosPedido artPed)
        {
            if (artPed == null) throw new ArgumentNullException("Error, artículo nulo");
            return new ArticulosPedidoDto()
            {
                PedidoId = artPed.PedidoId,
                ArticuloListarDto = ArticuloMappers.ToDto(artPed.Articulo),
                Unidades = artPed.Unidades,
                PrecioUnitario = artPed.PrecioUnitario
            };
        }
    }
}
