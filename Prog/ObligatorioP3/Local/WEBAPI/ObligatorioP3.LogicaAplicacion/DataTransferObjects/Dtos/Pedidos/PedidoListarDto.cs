using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos
{
    public class PedidoListarDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; }
        [Required]
        public DateTime FechaEmision { get; set; }
        [Required]
        public ClienteListarDto ClienteDto { get; set; }
        [Required]
        public decimal PrecioFinal { get; set; }
        [Required]
        public bool Anulado {  get; set; }
    }
}
