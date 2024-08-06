using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido
{
    public class ArticulosPedidoDto
    {
        [Required]
        public int PedidoId { get; set; }
        [Required]
        public ArticuloListarDto ArticuloListarDto { get; set; }
        [Required]
        public int Unidades { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
    }
}
