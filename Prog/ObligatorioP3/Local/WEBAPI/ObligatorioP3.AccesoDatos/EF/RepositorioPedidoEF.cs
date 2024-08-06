using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Excepciones.Articulo;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioPedidoEF : IRepositorioPedido
    {
        #region Propiedades y constructor
        private ObligatorioP3Context _db { get; set; }

        public RepositorioPedidoEF(ObligatorioP3Context db)
        {
            _db = db;
        }
        #endregion

        /// <summary>
        /// Agrega un nuevo pedido en la base de datos
        /// </summary>
        /// <param name="obj">Entidad Pedido</param>
        public void Add(Pedido obj)
        {
            obj.EsValido();
            if (obj == null)
            {
                throw new ArgumentNullException("Error, pedido nulo para cargar a la BD");
            }
            try
            {
                _db.Pedidos.Add(obj);
                if(obj.Lineas.Count > 0)
                {
                    foreach (var item in obj.Lineas)
                    {
                        _db.Entry(item.Articulo).State = EntityState.Unchanged;
                    }
                }
                _db.Entry(obj.Cliente).State = EntityState.Unchanged;
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new Exception($"Error interno. Envie a su programador: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {

                throw new Exception($"El pedido no se pudo agregar, más info: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera todos los pedidos de la base de datos
        /// </summary>
        /// <returns>IEnumerable de entidades pedido</returns>
        public IEnumerable<Pedido> GetAll()
        {
            try
            {
                return _db.Pedidos.AsNoTracking()
                                    .Include(p => p.Lineas)
                                        .ThenInclude(l => l.Articulo)
                                    .Include(p => p.Cliente)
                                    .ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Recupera todos los pedidos anulados de la base de datos
        /// </summary>
        /// <returns>IEnumerable de entidades pedido</returns>
        public IEnumerable<Pedido> GetAllAnulados()
        {
            try
            {
                return _db.Pedidos.AsNoTracking()
                                    .Include(p => p.Lineas)
                                        .ThenInclude(l => l.Articulo)
                                    .Include(p => p.Cliente)
                                    .Where(p => p.Anulado.Equals(true))
                                    .ToList().OrderByDescending(p=>p.Fecha);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Recupera los pedidos filtrando por fecha de emisión y que no hayan sido entregados aún
        /// </summary>
        /// <param name="date">Fecha de emisión</param>
        /// <returns>IEnumerable de Entidades pedido</returns>
        public IEnumerable<Pedido> Filtrar(DateTime date)
        {
            try
            {
                return _db.Pedidos.AsNoTracking()
                                    .Include(p => p.Lineas)
                                        .ThenInclude(l => l.Articulo)
                                    .Include(p => p.Cliente)
                                    .Where(p => p.Fecha.Date.Equals(date.Date) && p.FechaEntrega.Date > DateTime.Now.Date)
                                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Recupera un pedido específico usando el ID para encontrarlo
        /// </summary>
        /// <param name="id">Id del pedido que se quiere obtener</param>
        /// <returns>Entidad Pedido</returns>
        public Pedido? GetById(int? id)
        {
            try
            {
                return _db.Pedidos.AsNoTracking()
                                    .Include(p => p.Lineas)
                                        .ThenInclude(l => l.Articulo)
                                    .Include(p => p.Cliente)
                                    .FirstOrDefault(p => p.Id == id);
            }
            catch (Exception e) 
            { 
                throw e; 
            }
        }

        /// <summary>
        /// Recupera un pedido de la base de datos y coloca su valor "anulado" como "true"
        /// </summary>
        /// <param name="id">Id del pedido que se quiere anular</param>
        public void Anular(int id)
        {
            try
            {
                Pedido? pedidoParaAnular = _db.Pedidos.Include(p => p.Lineas)
                                                            .ThenInclude(l => l.Articulo)
                                                        .Include(p => p.Cliente)
                                                        .FirstOrDefault(p => p.Id == id);

                if(pedidoParaAnular != null)
                {
                    pedidoParaAnular.AnularPedido();
                }
                _db.SaveChanges();
            }
            catch (Exception e) 
            { 
                throw e; 
            }
        }

        public void Remove(int id)
        {

        }

        public void Remove(Pedido obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Pedido obj)
        {
            throw new NotImplementedException();
        }

    }
}
