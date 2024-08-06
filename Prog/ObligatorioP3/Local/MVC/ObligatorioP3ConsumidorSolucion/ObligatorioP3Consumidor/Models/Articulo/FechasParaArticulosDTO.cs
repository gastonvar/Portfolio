using Humanizer;

namespace ObligatorioP3Consumidor.Models.Articulo
{
    public class FechasYArticulosDTO
    {
        public DateOnly fecha1 { get; set; }
        public DateOnly fecha2 { get; set; }
        
        public IEnumerable<ArticuloListarDto> listaArticulos { get; set; } = null;
    }
}
