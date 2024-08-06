using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using ObligatorioP3.LogicaNegocio.Excepciones.Pedido;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public class PedidoExpress : Pedido
    {
        public int PlazoExpress { get; set; }

        private PedidoExpress() : base() { }
        public PedidoExpress(DateTime fechaEntrega, Cliente cliente, List<ArticulosPedido> lineas, decimal iva, int plazoExpress, decimal recargo) : base(fechaEntrega, cliente, lineas, iva, recargo)
        {
            PlazoExpress = plazoExpress;
            PrecioFinal = CalcularYFijarPrecioFinal();
        }

        public override void EsValido()
        {
            base.EsValido();
            if ((PlazoExpress < (FechaEntrega.Day - Fecha.Day))) throw new PedidoNoValidoException("Error, el plazo de entrega del pedido Express no puede superar los 5 días");
        }

        public override decimal CalcularYFijarPrecioFinal()
        {
            decimal precioBase = base.CalcularYFijarPrecioFinal();
                decimal total = precioBase * Recargo;
                return total * IVA;
        }

        public override void AnularPedido()
        {
            base.AnularPedido();
        }
    }
}
