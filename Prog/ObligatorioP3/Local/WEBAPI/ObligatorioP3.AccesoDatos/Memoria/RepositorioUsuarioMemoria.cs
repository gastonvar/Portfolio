using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.Memoria
{
    public class RepositorioUsuarioMemoria : IRepositorioUsuario
    {
        public void Add(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Usuario obj)
        {
            throw new NotImplementedException();
        }
    }
}
