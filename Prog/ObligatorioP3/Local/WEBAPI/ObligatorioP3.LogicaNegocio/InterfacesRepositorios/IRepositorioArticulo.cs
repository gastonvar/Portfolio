using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioArticulo : IRepositorio<Articulo>
    {
        IEnumerable<Articulo?> GetArticulosConMovimientosSegunFechas(DateTime fecha1, DateTime fecha2, int pagina, int cantidadRegistros);
    }
}
