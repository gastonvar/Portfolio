using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento
{
    public class EditarTipoDeMovimiento : IEditarTipoDeMovimiento
    {
        private IRepositorioTipoDeMovimientoEF _repo;
        public EditarTipoDeMovimiento(IRepositorioTipoDeMovimientoEF repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Caso de uso de modificar.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipoModificado"></param>
        public void Ejecutar(int id, TipoDeMovimientoModificacionDto tipoModificado)
        {
            if (tipoModificado.Aumento && !tipoModificado.Nombre.Contains(" (I)"))
            {
                tipoModificado.Nombre += " (I)";
            }
            if (!tipoModificado.Aumento && !tipoModificado.Nombre.Contains(" (E)"))
            {
                tipoModificado.Nombre += " (E)";
            }
            TipoDeMovimiento tipo = TipoDeMovimientoMappers.FromDTO(tipoModificado);
            _repo.Update(id, tipo);
        }
    }
}
