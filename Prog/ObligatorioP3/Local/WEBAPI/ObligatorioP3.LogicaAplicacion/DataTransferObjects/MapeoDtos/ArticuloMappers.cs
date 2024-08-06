using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    public class ArticuloMappers
    {
        public static Articulo FromDTO(ArticuloAltaDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Articulo articulo = new Articulo(dto.Nombre, dto.Descripcion, dto.Codigo, dto.Precio, dto.Stock);
            return articulo;
        }

        public static Articulo FromDTO(ArticuloListarDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Articulo articulo = new Articulo(dto.Nombre, dto.Descripcion, dto.Codigo, dto.Precio, dto.Stock);
            return articulo;
        }

        public static ArticuloListarDto ToDto(Articulo art)
        {
            if (art == null) throw new ArgumentNullException("Error, artículo nulo");
            return new ArticuloListarDto()
            {
                Id = art.Id,
                Nombre = art.Nombre,
                Descripcion = art.Descripcion,
                Codigo = art.Codigo,
                Precio = art.Precio,
                Stock = art.Stock,
            };
        }

        public static IEnumerable<ArticuloListarDto> FromLista(IEnumerable<Articulo> articulosOrigen)
        {
            if (articulosOrigen == null || articulosOrigen.Count() == 0) throw new ArgumentNullException("No existen articulos registrados");
            return articulosOrigen.Select(Articulo => ToDto(Articulo));
        }
    }
}
