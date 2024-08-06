namespace ObligatorioP3Consumidor.Models.Movimientos
{
    public class MovimientoStockAltaDTO
    {
        public int IdArticulo { get; set; }
        public int IdTipo { get; set; }
        public int Cantidad { get; set; }
        public string EmailUsuario { get; set; }
    }
}
