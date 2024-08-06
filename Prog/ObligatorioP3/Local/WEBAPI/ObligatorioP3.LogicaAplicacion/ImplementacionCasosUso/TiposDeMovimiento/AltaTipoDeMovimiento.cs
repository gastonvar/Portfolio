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
    public class AltaTipoDeMovimiento : IAltaMovimiento
    {
        private IRepositorioTipoDeMovimientoEF _repo;

        public AltaTipoDeMovimiento(IRepositorioTipoDeMovimientoEF repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Mappea el UsuarioAltaDto en un Usuario, encripta su contraseña,llama al metodo Add del repositorioEF de Usuario.
        /// </summary>
        /// <param name="dto"></param>
        public void Ejecutar(TipoDeMovimientoAltaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("TipoDeMovimiento nulo");
            }
            if (dto.Aumento &&!dto.Nombre.Contains(" (I)")) {
                dto.Nombre += " (I)";
            }
            if (!dto.Aumento && !dto.Nombre.Contains(" (E)"))
            {
                dto.Nombre += " (E)";
            }
            TipoDeMovimiento tipo = TipoDeMovimientoMappers.FromDTO(dto);
                _repo.Add(tipo);


        }
    }
}
