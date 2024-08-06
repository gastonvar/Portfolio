using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.Memoria
{
    public class RepositorioClienteMemoria : IRepositorioCliente
    {
        public void Add(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> FiltrarXMonto(decimal money)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> FiltrarXTexto(string txt)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cliente GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Cliente obj)
        {
            throw new NotImplementedException();
        }
    }
}
