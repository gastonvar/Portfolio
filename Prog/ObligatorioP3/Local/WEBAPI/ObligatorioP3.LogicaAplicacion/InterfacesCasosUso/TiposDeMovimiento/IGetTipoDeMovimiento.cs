using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento
{
    public interface IGetTipoDeMovimiento
    {
        TipoDeMovimientoListarDTO GetById(int? id);
        IEnumerable<TipoDeMovimientoListarDTO> GetAll();
    }
}
