using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.Memoria
{
    public class RepositorioPedidoMemoria : IRepositorioPedido
    {
        public void Add(Pedido obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetAllAnulados()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> Filtrar(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Pedido GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Anular(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> ObtenerClientesXMonto(double money)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
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
