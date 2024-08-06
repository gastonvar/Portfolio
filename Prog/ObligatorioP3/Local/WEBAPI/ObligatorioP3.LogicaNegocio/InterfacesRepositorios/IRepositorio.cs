using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorio<T> : IRepositorioDisminuido<T> where T : class
    {
        T GetById(int? id);
        void Update(int id, T obj);
        void Remove(int id);
        void Remove(T obj);
        IEnumerable<T> GetAll();
    }
}
