using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.TiposDeMovimiento
{
    public class BorrarTipoDeMovimiento : IBorrarTipoDeMovimiento
    {
        private IRepositorioTipoDeMovimientoEF _repo;
        public BorrarTipoDeMovimiento(IRepositorioTipoDeMovimientoEF repo)
        {
            _repo = repo;
        }
        public void Ejecutar(int id)
        {
            _repo.Remove(id);
        }
    }
}
