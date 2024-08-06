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
    public class GetArticulo : IGetArticulo
    {
        private IRepositorioArticulo _repositorioArticulo;
        public GetArticulo(IRepositorioArticulo repo)
        {
            _repositorioArticulo = repo;
        }

        /// <summary>
        /// Recibe el id de Artículo por parámetro y lo manda a traer desde el repositorio, lo mapea a DTO y lo devuelve
        /// </summary>
        /// <param name="id"></param>
        public ArticuloListarDto Ejecutar(int? id)
        {
            var articuloOrigen = _repositorioArticulo.GetById(id);
            if (articuloOrigen == null)
            {
                throw new Exception("No existe el artículo");
            }
            return ArticuloMappers.ToDto(articuloOrigen);
        }
    }
}
