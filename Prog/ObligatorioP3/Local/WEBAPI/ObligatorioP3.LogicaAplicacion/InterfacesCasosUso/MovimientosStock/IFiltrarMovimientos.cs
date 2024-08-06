using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock
{
    public interface IFiltrarMovimientos
    {
        public IEnumerable<MovimientoListarDTO> Ejecutar(int idArticulo, int idTipo, int pagina);
    }
}
