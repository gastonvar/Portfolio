using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.MovimientosStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock
{
    public interface IListarResumenMovimientos
    {
        public IEnumerable<MovimientoListarAgrupadoDTO>  Ejecutar();
    }
}
