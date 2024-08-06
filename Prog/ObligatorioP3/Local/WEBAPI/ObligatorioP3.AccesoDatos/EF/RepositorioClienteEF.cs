using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.EF
{
    public class RepositorioClienteEF : IRepositorioCliente
    {
        #region Propiedades y constructor
        private ObligatorioP3Context _db { get; set; }

        public RepositorioClienteEF(ObligatorioP3Context db)
        {
            _db = db;
        }
        #endregion
        public void Add(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> GetAll()
        {
            try
            {
                return _db.Clientes.ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Cliente GetById(int? id)
        {
            return _db.Clientes.Find(id);
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

        public IEnumerable<Cliente> FiltrarXTexto(string txt)
        {
            var clientes = _db.Clientes
            .Where(cli => cli.RazonSocial.Contains(txt));
            return clientes;
        }

        public IEnumerable<Cliente> FiltrarXMonto(decimal money)
        {
            var clientes = _db.Pedidos
                      .GroupBy(pedido => new Cliente (pedido.Cliente.Id, pedido.Cliente.RazonSocial, pedido.Cliente.RUT, pedido.Cliente.Direccion.Calle,pedido.Cliente.Direccion.Ciudad,pedido.Cliente.Direccion.Numero,pedido.Cliente.Direccion.Distancia)) // Agrupamos los pedidos por cliente
                      .Where(grupo => grupo.Sum(pedido => pedido.PrecioFinal) > money) // Filtramos los grupos cuya suma de precios finales sea mayor a money
                      .Select(grupo => grupo.Key) // Seleccionamos los clientes de los grupos filtrados
                      .ToList();
            return clientes;
        }
    }
}
