using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses
{
    public class ArticulosPedido : IValidable<ArticulosPedido>
    {
        /*Prop de navegacion*/
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        public Articulo Articulo { get; set; }
        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public ArticulosPedido(int pedidoId, Articulo articulo, int unidades, decimal precioUnitario) 
        {
            PedidoId = pedidoId;
            Articulo = articulo;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            EsValido();
        }

        public ArticulosPedido()
        {
            
        }
        public void EsValido()
        {
            if(Articulo.Stock < Unidades) { throw new ArgumentException($"No hay suficiente stock del artículo: {Articulo.Nombre}. Stock disponible: {Articulo.Stock}"); }
        }
    }
}
