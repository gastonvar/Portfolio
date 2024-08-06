using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioTipoDeMovimientoEF : IRepositorioTipoDeMovimientoEF
    {
        private ObligatorioP3Context _db;

        public RepositorioTipoDeMovimientoEF(ObligatorioP3Context db)
        {
            _db = db;
        }

        public void Add(TipoDeMovimiento obj)
        {
            obj.EsValido();
            if (obj == null)
            {
                throw new ArgumentNullException("Error, autor nulo para cargar a la BD");
            }
            try
            {
                _db.TiposDeMovimiento.Add(obj);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new TipoDeMovimientoNoValidoException($"Error interno. Envie a su programador: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {

                throw new TipoDeMovimientoNoValidoException($"El tipo de movimiento no se pudo agregar, más info: {ex.Message}");
            }
        }

        public IEnumerable<TipoDeMovimiento> GetAll()
        {
            try
            {
                return _db.TiposDeMovimiento.ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public TipoDeMovimiento GetById(int? id)
        {
            try
            {
                return _db.TiposDeMovimiento.Find(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var tipoABorrar = _db.TiposDeMovimiento.Find(id);
                if (tipoABorrar == null) throw new TipoDeMovimientoNoValidoException($"El tipo con id {id} NO EXISTE");
                if (TipoDeMovimientoEnUso(tipoABorrar)) throw new TipoDeMovimientoNoValidoException($"El tipo con id {id} está en uso y no puede ser borrado");
                _db.TiposDeMovimiento.Remove(tipoABorrar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool TipoDeMovimientoEnUso(TipoDeMovimiento tipo) { 
            return _db.MovimientosDeStock.Include(movimiento => movimiento.Tipo)
                                         .Where(movimiento=>movimiento.Tipo.Nombre.Equals(tipo.Nombre))
                                         .Any();
        }

        public void Remove(TipoDeMovimiento obj)
        {
            try
            {

                if (obj == null) throw new ArgumentNullException();
                _db.TiposDeMovimiento.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(int id, TipoDeMovimiento obj)
        {
            try
            {
                var tipoAUpdatear = _db.TiposDeMovimiento.Find(id);
                if (tipoAUpdatear == null)
                    throw new TipoDeMovimientoNoValidoException($"No existe el tipo con el id {id}");
                tipoAUpdatear.ModificarDatos(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
