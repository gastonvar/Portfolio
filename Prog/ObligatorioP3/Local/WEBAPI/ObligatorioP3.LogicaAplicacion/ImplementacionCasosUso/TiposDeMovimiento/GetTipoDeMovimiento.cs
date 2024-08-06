using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento
{
    public class GetTipoDeMovimiento : IGetTipoDeMovimiento
    {
        private IRepositorioTipoDeMovimientoEF _repo;

        public GetTipoDeMovimiento(IRepositorioTipoDeMovimientoEF repo)
        {
            _repo = repo;
        }

        public IEnumerable<TipoDeMovimientoListarDTO> GetAll()
        {
            var tipos = _repo.GetAll();
            if (!tipos.Any()) throw new Exception("No hay tipos de movimiento");
            var tiposDTO = TipoDeMovimientoMappers.FromLista(tipos);
            return tiposDTO;
        }

        public TipoDeMovimientoListarDTO GetById(int? id)
        {
            var tipo = _repo.GetById(id);
            if (tipo == null) throw new TipoDeMovimientoNoValidoException("Error, no existe ningun tipo con esa id");
            var tipoDTO = TipoDeMovimientoMappers.ToDto(tipo);
            return tipoDTO;
        }

    }
}
