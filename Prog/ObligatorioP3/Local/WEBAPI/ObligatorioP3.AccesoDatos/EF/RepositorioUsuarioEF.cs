using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        #region Propiedades y constructor
        private ObligatorioP3Context _db { get; set; }

        public RepositorioUsuarioEF(ObligatorioP3Context db)
        {
            _db = db;
        }
        #endregion
        public void Add(Usuario obj)
        {
            obj.EsValido();
            if (obj == null)
            {
                throw new ArgumentNullException("Error, autor nulo para cargar a la BD");
            }
            try
            {
                _db.Usuarios.Add(obj);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw new UsuarioNoValidoException($"Error interno. Envie a su programador: {ex.InnerException.Message}");
            }
            catch (Exception ex)
            {

                throw new UsuarioNoValidoException($"El usuario no se pudo agregar, más info: {ex.Message}");
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            try
            {
                return _db.Usuarios.ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Usuario GetById(int? id)
        {
            try
            {
                return _db.Usuarios.Find(id);
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
                var usuarioABorrar = _db.Usuarios.Find(id);
                if (usuarioABorrar == null) throw new UsuarioNoValidoException($"El usuario con id {id} NO EXISTE");
                _db.Usuarios.Remove(usuarioABorrar);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Remove(Usuario obj)
        {
            try
            {

                if (obj == null) throw new ArgumentNullException();
                _db.Usuarios.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(int id, Usuario obj)
        {
            try
            {
                var usuarioAUpdatear = _db.Usuarios.Find(id);
                if (usuarioAUpdatear == null)
                    throw new UsuarioNoValidoException($"No existe el usuario con el id {id}");
                usuarioAUpdatear.ModificarDatos(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public Usuario GetByEmail(string email)
        {
            try
            {
                //El new al email sustituye el utilizar "asEnumerable()"
                Usuario x = _db.Usuarios.AsEnumerable().FirstOrDefault(u => u.Email.ValorEmail == email);
                return x;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }


    }
}
