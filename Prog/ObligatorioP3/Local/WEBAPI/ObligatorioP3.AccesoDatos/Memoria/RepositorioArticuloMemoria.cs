using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.AccesoDatos.Memoria
{
    public class RepositorioArticuloMemoria : IRepositorioArticulo
    {
        public void Add(Articulo obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articulo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articulo?> GetArticulosConMovimientosSegunFechas(DateOnly fecha1, DateOnly fecha2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articulo?> GetArticulosConMovimientosSegunFechas(DateTime fecha1, DateTime fecha2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articulo?> GetArticulosConMovimientosSegunFechas(DateTime fecha1, DateTime fecha2, int pagina, int cantidadRegistros)
        {
            throw new NotImplementedException();
        }

        public Articulo GetById(int? id)
        {
            throw new NotImplementedException();
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
    }
}
