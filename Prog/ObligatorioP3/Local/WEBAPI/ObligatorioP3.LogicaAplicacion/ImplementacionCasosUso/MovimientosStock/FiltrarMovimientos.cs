using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.MovimientosStock
{
    public class FiltrarMovimientos : IFiltrarMovimientos
    {
        private IRepositorioMovimiento _repositorioMovimientoStock;
        private IRepositorioParametro _repositorioParametro;
        public FiltrarMovimientos(IRepositorioMovimiento repo, IRepositorioParametro repositorioParametro)
        {
            _repositorioMovimientoStock = repo;
            _repositorioParametro = repositorioParametro;
        }

        public IEnumerable<MovimientoListarDTO> Ejecutar(int idArticulo, int idTipo, int pagina)
        {
            try
            {
                var param = _repositorioParametro.GetParametro("cantidadRegistros");
                int cantidadRegistros = int.Parse(param.Valor);
                var MovimientosFiltrados = _repositorioMovimientoStock.Filtrar(idArticulo, idTipo, pagina, cantidadRegistros);
                if(MovimientosFiltrados.Count() == 0) { throw new Exception("No existen movimientos asociados a esos IDs"); }
                return MovimientoStockMapper.FromLista(MovimientosFiltrados);
            }catch (Exception ex)
            {
                throw;
            }
        }


    }
}
