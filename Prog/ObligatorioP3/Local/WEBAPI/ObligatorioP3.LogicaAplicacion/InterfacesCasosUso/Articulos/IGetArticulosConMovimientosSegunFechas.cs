using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos
{
    public interface IGetArticulosConMovimientosSegunFechas
    {
        IEnumerable<ArticuloListarDto> Ejecutar(DateTime fecha1, DateTime fecha2, int pagina);
    }
}
