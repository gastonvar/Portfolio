using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioMovimiento : IRepositorioDisminuido<MovimientoStock>
    {
        public IEnumerable<Object> GetGrouped();

        public IEnumerable<MovimientoStock> Filtrar(int idArticulo, int idTipo, int pagina, int cantidadRegistros);

        int ObtenerStockActual(int idArticulo);
    }
}
