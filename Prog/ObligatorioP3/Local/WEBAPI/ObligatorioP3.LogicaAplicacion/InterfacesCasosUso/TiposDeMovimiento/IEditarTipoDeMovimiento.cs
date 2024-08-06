using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento
{
    public interface IEditarTipoDeMovimiento
    {
        void Ejecutar(int id, TipoDeMovimientoModificacionDto tipoModificado);
    }
}
