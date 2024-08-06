using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock
{
    public interface IAltaMovimientoStock
    {
        public void Ejecutar(MovimientoStockAltaDTO dto);
    }
}
