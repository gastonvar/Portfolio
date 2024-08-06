using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos
{
    public class AltaArticulo : IAltaArticulo
    {
        private IRepositorioArticulo _repositorioArticulo;
        public AltaArticulo(IRepositorioArticulo repo)
        {
            _repositorioArticulo = repo;
        }

        /// <summary>
        /// Mappea el dto a Entidad y lo manda al método Add del repositorio de artículos
        /// </summary>
        /// <param name="dto"></param>
        public void Ejecutar(ArticuloAltaDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Articulo nulo");
            }

            Articulo articulo = ArticuloMappers.FromDTO(dto);
            _repositorioArticulo.Add(articulo);
        }
    }
}
