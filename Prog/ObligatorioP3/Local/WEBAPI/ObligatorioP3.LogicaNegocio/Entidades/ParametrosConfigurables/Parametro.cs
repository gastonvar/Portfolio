using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaNegocio.Entidades.ParametrosConfigurables
{
    public class Parametro
    {
        [Key]
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public Parametro(string nombre, string valor)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(valor))
                throw new ArgumentException("Parámetros vacíos");
                Nombre = nombre;
            Valor = valor;
        }
    }
}
