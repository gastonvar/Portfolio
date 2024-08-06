using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos
{
    public class GetAllArticulos : IGetAllArticulos
    {
        private IRepositorioArticulo _repositorioArticulo;
        public GetAllArticulos(IRepositorioArticulo repo)
        {
            _repositorioArticulo = repo;
        }

        /// <summary>
        /// Llama al repositorio y trae todos los artículos, los mapea en un IEnumerable de DTO y los devuelve
        /// </summary>
        public IEnumerable<ArticuloListarDto> Ejecutar()
        {
            var articulosOrigen = _repositorioArticulo.GetAll();
            if (articulosOrigen == null || articulosOrigen.Count() == 0)
            {
                throw new Exception("No hay articulos registrados");
            }
            return ArticuloMappers.FromLista(articulosOrigen);
        }
    }
}
