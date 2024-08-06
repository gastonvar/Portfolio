namespace ObligatorioP3Consumidor.Models.Movimientos
{
    public class MovimientosYselectores
    {
        public Selectores Selectores { get; set; }
        public IEnumerable<MovimientoListarDTO> Movimientos { get; set; }
    }
}
