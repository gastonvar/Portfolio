using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioMovimientoEF : IRepositorioMovimiento
    {
        #region Propiedades y constructor
        private ObligatorioP3Context _db { get; set; }
        private static int cantMovimientos = 0;

        public RepositorioMovimientoEF(ObligatorioP3Context db)
        {
            _db = db;
        }
        #endregion

        public int ObtenerStockActual(int idArticulo)
        {
            int stock = 0;
            stock = _db.MovimientosDeStock.Include(m => m.Tipo).Where(m => m.Articulo.Id == idArticulo).Sum(m => m.Cantidad * m.Tipo.Coeficiente);
            return stock;
        }

        public void Add(MovimientoStock obj)
        {
            obj.EsValido();
            if (obj == null) throw new ArgumentNullException("Error, movimiento nulo para cargar a la BD");
            int stockActual = ObtenerStockActual(obj.Articulo.Id);
            int cantidadMovida = obj.Cantidad * obj.Tipo.Coeficiente;
            if (cantidadMovida < 0)
            {
                if (stockActual + cantidadMovida < 0)
                    throw new Exception("Egreso excede stock");
            }
            try
            {
                _db.MovimientosDeStock.Add(obj);
                _db.Entry(obj.Articulo).State = EntityState.Unchanged;
                _db.Entry(obj.Usuario).State = EntityState.Unchanged;
                _db.Entry(obj.Tipo).State = EntityState.Unchanged;
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new Exception($"Error interno. Envie a su programador: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"El movimiento no se pudo agregar, más info: {ex.Message}");
            }
        }

        public IEnumerable<object> GetGrouped()
        {
            try
            {
                var resultados = _db.MovimientosDeStock
                                        .GroupBy(m => m.Fecha.Year)
                                        .Select(g => new
                                        {
                                            Ano = g.Key.ToString(),
                                            MovimientoCantidad = g
                                                .GroupBy(m => m.Tipo.Nombre)
                                                .Select(tg => new
                                                {
                                                    Nombre = tg.Key,
                                                    Cantidad = tg.Count()
                                                })
                                                .ToList(),
                                            Total = g.Count()
                                        })
                                        .ToList();
                return resultados;
            } catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MovimientoStock> Filtrar(int idArticulo, int idTipo, int numPagina, int cantidadRegistros)
        {
            try
            {
                if (numPagina <= 1)
                cantMovimientos = _db.MovimientosDeStock.Count();
                int numRegistrosAnteriores = 0;
                if (numPagina > 1)
                {
                    numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                }
                var movimientos = _db.MovimientosDeStock.AsNoTracking()
                                            .Include(m => m.Articulo)
                                            .Include(m => m.Tipo)
                                            .Include(m=>m.Usuario)
                                            .Where(m => m.IdArticulo == idArticulo && m.IdTipo == idTipo)
                                            .OrderByDescending(m => m.Fecha)
                                            .ThenBy(m => m.Cantidad)
                                            .Skip(numRegistrosAnteriores)
                                            .Take(cantidadRegistros)
                                            .ToList();
                if (!movimientos.Any())
                    cantMovimientos = 0;
                return movimientos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
