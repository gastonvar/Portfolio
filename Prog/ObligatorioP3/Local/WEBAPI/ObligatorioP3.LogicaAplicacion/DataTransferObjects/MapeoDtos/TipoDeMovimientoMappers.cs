using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos
{
    public class TipoDeMovimientoMappers
    {
        public static TipoDeMovimiento FromDTO(TipoDeMovimientoAltaDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            TipoDeMovimiento nuevo = new TipoDeMovimiento(dto.Nombre,dto.Aumento);
            return nuevo;
        }
        public static TipoDeMovimiento FromDTO(TipoDeMovimientoListarDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            TipoDeMovimiento nuevo = new TipoDeMovimiento(dto.Nombre, dto.Aumento);
            return nuevo;
        }

        public static TipoDeMovimiento FromDTO(TipoDeMovimientoModificacionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var tipo = new TipoDeMovimiento(dto.Id, dto.Nombre, dto.Aumento);
            return tipo;
        }

        public static TipoDeMovimientoListarDTO ToDto(TipoDeMovimiento tipo)
        {
            if (tipo == null) throw new ArgumentNullException("Error, tipo nulo");
            return new TipoDeMovimientoListarDTO()
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Aumento = tipo.Aumento,
                Coeficiente = tipo.Coeficiente
            };
        }

        public static IEnumerable<TipoDeMovimientoListarDTO> FromLista(IEnumerable<TipoDeMovimiento> tiposOrigen)
        {
            if (tiposOrigen == null || tiposOrigen.Count() == 0) throw new ArgumentNullException("No existen tipos registrados");
            return tiposOrigen.Select(tipo => ToDto(tipo));
        }
    }
}
