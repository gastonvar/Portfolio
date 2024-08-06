using Libreria.LogicaNegocio.Entidades.ParametrosConfigurables;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.MapeoDtos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.MovimientosStock;
using ObligatorioP3.LogicaNegocio.Entidades;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.MovimientosStock
{
    public class AltaMovimiento : IAltaMovimientoStock
    {
        private IRepositorioMovimiento _repositorioMovimientoStock;
        private IRepositorioUsuario _repositorioUsuario;
        private IRepositorioArticulo _repositorioArticulo;
        private IRepositorioTipoDeMovimientoEF _repositorioTipoDeMovimiento;

        private IRepositorioParametro _repositorioParametro;

        public AltaMovimiento(IRepositorioMovimiento repo, IRepositorioUsuario repositorioUsuario, IRepositorioArticulo repositorioArticulo, IRepositorioTipoDeMovimientoEF repositorioTipoDeMovimiento, IRepositorioParametro repositorioParametro)
        {
            _repositorioMovimientoStock = repo;
            _repositorioUsuario = repositorioUsuario;
            _repositorioArticulo = repositorioArticulo;
            _repositorioTipoDeMovimiento = repositorioTipoDeMovimiento;
            _repositorioParametro = repositorioParametro;
        }

        public void Ejecutar(MovimientoStockAltaDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("Movimiento nulo");
            }
            Parametro topeParametro= _repositorioParametro.GetParametro("topeMovimiento");
            int tope = int.Parse(topeParametro.Valor);
            if (dto.Cantidad > tope) throw new Exception("Tope supera el valor permitido");
            Usuario usuario = _repositorioUsuario.GetByEmail(dto.emailUsuario);
            Articulo articulo = _repositorioArticulo.GetById(dto.IdArticulo);
            TipoDeMovimiento tipo = _repositorioTipoDeMovimiento.GetById(dto.IdTipo);
            MovimientoStock movimiento = MovimientoStockMapper.FromDTO(dto, articulo, tipo, usuario);
            _repositorioMovimientoStock.Add(movimiento);
        }
    }
}
