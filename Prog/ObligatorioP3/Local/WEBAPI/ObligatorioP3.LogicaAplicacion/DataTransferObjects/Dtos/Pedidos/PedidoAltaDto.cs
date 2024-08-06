using ObligatorioP3.LogicaNegocio.Entidades.AssosiationClasses;
using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos
{
    public class PedidoAltaDto
    {
        [Required]
        public DateTime FechaEntrega { get; set; }
        [Required]
        public ClienteListarDto ClienteDto { get; set; }
        [Required]
        public List<ArticulosPedidoDto> LineasDto;
    }
}
