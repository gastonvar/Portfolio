using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPedido:IRepositorio<Pedido>
    {
        public IEnumerable<Pedido> GetAllAnulados();

        public IEnumerable<Pedido> Filtrar(DateTime date);

        public void Anular(int id);
    }
}
