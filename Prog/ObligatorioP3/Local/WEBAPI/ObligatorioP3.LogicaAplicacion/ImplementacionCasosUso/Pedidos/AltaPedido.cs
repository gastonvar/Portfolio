using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos
{
    public class AltaPedido : IAltaPedido
    {
        private IRepositorioPedido _repositorioPedido;
        private IRepositorioParametro _repositorioParametro;
        public AltaPedido(IRepositorioPedido repo, IRepositorioParametro repoParam)
        {
            _repositorioPedido = repo;
            _repositorioParametro = repoParam;
        }

        /// <summary>
        /// Mapea el pedido de DTO a Entidad, consulta los parámetros configurables y lo envía al repositorio para agregarlo a la base de datos
        /// </summary>
        /// <param name="dto">Es el pedido en formato DTO</param>
        /// <param name="esExpress">Un valor booleano que se utiliza para diferenciar si es express o común y ejecutar una lógica distinta en cada uno</param>
        public void Ejecutar(PedidoAltaDto dto, bool esExpress)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Pedido nulo");
            }

            Parametro iva = _repositorioParametro.GetParametro("iva");
            decimal valorIva = decimal.Parse(iva.Valor);
            Parametro plazo = _repositorioParametro.GetParametro("plazoExpress");
            int plazoExpress = int.Parse(plazo.Valor);
            

            if (!esExpress) 
            {
                Parametro recargoComun = null;
                decimal valorRecargoComun = 1;
                if(dto.ClienteDto.Distancia > 100)
                {
                    recargoComun = _repositorioParametro.GetParametro("recargoComun");
                }
                if (recargoComun != null)
                {
                    valorRecargoComun = decimal.Parse(recargoComun.Valor);
                }
                Pedido pedido = PedidoMappers.FromDTOcomun(dto, valorIva, valorRecargoComun);
                _repositorioPedido.Add(pedido);
            }
            else if (esExpress)
            {
                Parametro recargoExpress = null;
                if(dto.FechaEntrega.Date == DateTime.Today.Date)
                {
                    recargoExpress = _repositorioParametro.GetParametro("recargoExpressB");
                }
                else
                {
                    recargoExpress = _repositorioParametro.GetParametro("recargoExpressA");
                }
                
                decimal valorRecargoExpress = decimal.Parse(recargoExpress.Valor);
                Pedido pedido = PedidoMappers.FromDTOExpress(dto, valorIva, plazoExpress, valorRecargoExpress);
                pedido.EsValido();
                _repositorioPedido.Add(pedido);
            }
        }
    }
}
