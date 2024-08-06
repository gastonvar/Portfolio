using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.MovimientosStock
{
    public class ListarResumenMovimientos : IListarResumenMovimientos
    {
        private IRepositorioMovimiento _repositorioMovimientoStock;
        public ListarResumenMovimientos(IRepositorioMovimiento repo)
        {
            _repositorioMovimientoStock = repo;
        }

        public IEnumerable<MovimientoListarAgrupadoDTO> Ejecutar()
        {
            var listaOrigen = _repositorioMovimientoStock.GetGrouped();
            if (listaOrigen == null)
            {
                throw new Exception("No hay movimientos registrados");
            } 
            return MovimientoStockMapper.FromListaAgrupado(listaOrigen);
        }
    }
}
