using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos
{
    public class GetArticulosConMovimientosSegunFechas : IGetArticulosConMovimientosSegunFechas
    {
        private IRepositorioArticulo _repositorioArticulo;
        private IRepositorioParametro _repositorioParametro;
        public GetArticulosConMovimientosSegunFechas(IRepositorioArticulo repo, IRepositorioParametro repositorioParametro)
        {
            _repositorioArticulo = repo;
            _repositorioParametro = repositorioParametro;
        }
        public IEnumerable<ArticuloListarDto> Ejecutar(DateTime fecha1, DateTime fecha2, int pagina)
        {
            try
            {
                var param = _repositorioParametro.GetParametro("cantidadRegistros");
                int cantidadRegistros = int.Parse(param.Valor);
                var articulosFiltrados = _repositorioArticulo.GetArticulosConMovimientosSegunFechas(fecha1, fecha2, pagina, cantidadRegistros);
                var articulosListarDTO = ArticuloMappers.FromLista(articulosFiltrados);
                
                return articulosListarDTO;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
