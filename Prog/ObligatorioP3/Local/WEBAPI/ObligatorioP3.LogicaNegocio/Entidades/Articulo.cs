using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Excepciones.Articulo;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public class Articulo : IEntity, IValidable<Articulo>, IArticulo
    {
        #region Propiedades y constructores
        public int Id { get;set; }
        [Required, StringLength(1000, MinimumLength = 6)]
        public string Nombre { get; set; }
        [Required, StringLength(1000, MinimumLength = 5)]
        public string Descripcion { get; set; }
        [Required, StringLength(13)]
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        private Articulo() { }

        public Articulo(string nombre, string descripcion, string codigo, decimal precio, int stock) 
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Codigo = codigo;
            Precio = precio;
            Stock = stock;
            EsValido();
        }

        public Articulo(int id, string nombre, string descripcion, string codigo, decimal precio, int stock)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Codigo = codigo;
            Precio = precio;
            Stock = stock;
            EsValido();
        }
        #endregion
        public void EsValido()
        {
            if (Nombre == null) throw new ArticuloNoValidoException("Error, nombre nulo");
            if (Descripcion == null) throw new ArticuloNoValidoException("Error, descripcion nula");
            if (Codigo == null) throw new ArticuloNoValidoException("Error, Codigo nulo");
            if (Precio == null) throw new ArticuloNoValidoException("Error, Precio nulo");
            if (Stock == null) throw new ArticuloNoValidoException("Error, Stock nulo");
            if (Descripcion.Length < 5) throw new ArticuloNoValidoException("Error, la descripcion debe contener al menos 5 caracteres");
            if (Codigo.Trim().Length != 13) throw new ArticuloNoValidoException("Error, el código debe contener 13 digitos");
            if (Codigo.Contains(" ")) throw new ArticuloNoValidoException("Error, el código debe contener 13 digitos");
            if (!Codigo.All(char.IsDigit)) throw new ArticuloNoValidoException("Error, el código debe contener 13 digitos");
            if (Precio < 0) throw new ArticuloNoValidoException("Error, Precio menor a 0");
            if (Stock < 0) throw new ArticuloNoValidoException("Error, Stock menor a 0");
            if(Nombre.Length<10 || Nombre.Length > 200) { throw new ArticuloNoValidoException("Error, Nombre de articulo fuera de rango de caracteres: de 10 a 200"); }
            
        }
    }
}
