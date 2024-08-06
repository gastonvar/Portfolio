using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public class MovimientoStock : IMovimientoStock
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey(nameof(Articulo))]
        public int IdArticulo { get; set; }
        public Articulo? Articulo { get; set; }
        [ForeignKey(nameof(Tipo))]
        public int IdTipo { get; set; }
        public TipoDeMovimiento? Tipo { get; set; }
        [ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int Cantidad { get; set; }

        public MovimientoStock()
        {
            
        }

        public MovimientoStock(int id, DateTime fecha, int idArticulo, Articulo articulo, int idTipo, TipoDeMovimiento tipo, int idUsuario, Usuario usuario, int cantidad)
        {
            Id = id;
            Fecha = fecha;
            IdArticulo = idArticulo;
            Articulo = articulo;
            IdTipo = idTipo;
            Tipo = tipo;
            IdUsuario = idUsuario;
            Usuario = usuario;
            Cantidad = cantidad;
            EsValido();
        }

        public MovimientoStock(DateTime fecha, int idArticulo, Articulo articulo, int idTipo, TipoDeMovimiento tipo, int idUsuario, Usuario usuario, int cantidad)
        {
            Fecha = fecha;
            IdArticulo = idArticulo;
            Articulo = articulo;
            IdTipo = idTipo;
            Tipo = tipo;
            IdUsuario = idUsuario;
            Usuario = usuario;
            Cantidad = cantidad;
            EsValido();
        }

        public void EsValido()
        {
            if (this.Articulo == null) throw new Exception("Error, articulo de movimiento nula");
            if (this.Tipo == null) throw new Exception("Error, tipo de movimiento nula");
            if (this.Usuario == null) throw new Exception("Error, usuario de movimiento nula");
            if (this.Cantidad <= 0) throw new Exception("Error, las unidades deben ser 1 o más");
            if (this.Usuario.Rol != "encargado") throw new Exception("Error, el usuario no es un encargado");
            if (this.Tipo == null) throw new Exception("Error, tipo de movimiento inválido");
        }
    }
}
