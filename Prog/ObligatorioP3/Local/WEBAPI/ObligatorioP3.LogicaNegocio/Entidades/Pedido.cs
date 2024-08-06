using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using ObligatorioP3.LogicaNegocio.Excepciones.Pedido;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public abstract class Pedido : IEntity, IValidable<Pedido>, IPedido
    {
        #region Propiedades y constructores
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }
        public Cliente Cliente { get; set; }
        public decimal PrecioFinal { get; set; }

        public List<ArticulosPedido> Lineas;

        public decimal IVA { get; set; }
        public bool Anulado { get; set; }
        
        public decimal Recargo { get; set; }

        public Pedido() { }
        public Pedido(DateTime fechaEntrega, Cliente cliente, List<ArticulosPedido> lineas, decimal iva, decimal recargo) 
        {
            Fecha = DateTime.Now;
            FechaEntrega = fechaEntrega;
            Cliente = cliente;
            Lineas = lineas;
            IVA = iva;
            Anulado = false;
            Recargo = recargo;
        }
        #endregion
        public virtual void EsValido()
        {
            if (Fecha == null) throw new PedidoNoValidoException("Error, fecha nula");
            if (FechaEntrega == null) throw new PedidoNoValidoException("Error, fecha nula");
            if (Cliente == null) throw new PedidoNoValidoException("Error, cliente nulo");
            if (FechaEntrega.Date < Fecha.Date) throw new PedidoNoValidoException("Error, fecha de entrega anterior a fecha actual");
        }

        public virtual decimal CalcularYFijarPrecioFinal()
        {
            decimal total = 0;
            foreach (var item in Lineas)
            {
                total += item.PrecioUnitario * item.Unidades;
            }
            return total;
        }

        public virtual void AnularPedido()
        {
            Anulado = true;
        }
    }
}
