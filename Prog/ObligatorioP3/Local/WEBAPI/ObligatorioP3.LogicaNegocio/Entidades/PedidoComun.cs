using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using ObligatorioP3.LogicaNegocio.Excepciones.Pedido;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public class PedidoComun : Pedido
    {
        private PedidoComun() : base() { }
        public PedidoComun(DateTime fechaEntrega, Cliente cliente, List<ArticulosPedido> lineas, decimal iva, decimal recargo) : base(fechaEntrega, cliente, lineas, iva, recargo) 
        {
            
            PrecioFinal = CalcularYFijarPrecioFinal();
            EsValido();
        }

        public override void EsValido()
        {
            base.EsValido();
            if (7 > (FechaEntrega.Day - Fecha.Day)) throw new PedidoNoValidoException("Error, el plazo de entrega del pedido Común no puede ser menor a 1 semana");
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
