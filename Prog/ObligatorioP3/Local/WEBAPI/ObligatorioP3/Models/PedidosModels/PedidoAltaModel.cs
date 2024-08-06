using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using System.Collections;
using System.Collections.Generic;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;

namespace ObligatorioP3.Web.Models.PedidosModels
{
    public class PedidoAltaModel
    {
        public IEnumerable<ClienteListarDto> clientes { get; set; }
        public IEnumerable<ArticuloListarDto> articulos { get; set; }
        public List<ArticulosPedidoDto> lineas { get; set; }
    }
}
