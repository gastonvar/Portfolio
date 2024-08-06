using ObligatorioP3Consumidor.Models.Articulo;
using ObligatorioP3Consumidor.Models.TiposMovimientos;

namespace ObligatorioP3Consumidor.Models.Movimientos
{
    public class Selectores
    {
        public IEnumerable<ArticuloListarDto> Articulos { get; set; }
        public IEnumerable<TipoDeMovimientoListarDTO> Tipos { get; set; }
    }
}
