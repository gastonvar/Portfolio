using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Excepciones.Articulo;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioArticuloEF : IRepositorioArticulo
    {
        #region Propiedades y constructor
        private static int cantArticulos = 0;
        private ObligatorioP3Context _db { get; set; }

        public RepositorioArticuloEF(ObligatorioP3Context db)
        {
            _db = db;
        }
        #endregion

        /// <summary>
        /// Agrega el artículo a la base de datos
        /// </summary>
        /// <param name="art">Entidad Articulo</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArticuloNoValidoException"></exception>
        public void Add(Articulo art)
        {
            art.EsValido();
            if (art == null)
            {
                throw new ArgumentNullException("Error, artículo nulo para cargar a la BD");
            }
            try
            {
                _db.Articulos.Add(art);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new ArticuloNoValidoException($"Error interno. Envie a su programador: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {

                throw new ArticuloNoValidoException($"El artículo no se pudo agregar, más info: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera todos los artículos de la base de datos
        /// </summary>
        /// <returns>IEnumerable de entidades articulos</returns>
        public IEnumerable<Articulo> GetAll()
        {
            try
            {
                return _db.Articulos.ToList().OrderBy(a => a.Nombre);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Recupera un artículo de la base de datos cuyo id coincida con el id parámetro
        /// </summary>
        /// <param name="id">Id del artículo</param>
        /// <returns>Entidad Artículo</returns>
        public Articulo GetById(int? id)
        {
            return _db.Articulos.Find(id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Articulo obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Articulo obj)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Articulo?> GetArticulosConMovimientosSegunFechas(DateTime fecha1, DateTime fecha2, int pagina, int cantidadRegistros)
        {
            try
            {
                if (pagina <= 1)
                    cantArticulos = _db.Articulos.Count();
                int numRegistrosAnteriores = 0;
                if (pagina > 1)
                {
                    numRegistrosAnteriores = cantidadRegistros * (pagina - 1);
                }
                return _db.MovimientosDeStock.Include(m => m.Articulo)
                                             .Where(m => m.Fecha > fecha1 && m.Fecha < fecha2)
                                             .Select(m => m.Articulo)
                                             .Distinct()
                                             .Skip(numRegistrosAnteriores)
                                             .Take(cantidadRegistros)
                                             .ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
