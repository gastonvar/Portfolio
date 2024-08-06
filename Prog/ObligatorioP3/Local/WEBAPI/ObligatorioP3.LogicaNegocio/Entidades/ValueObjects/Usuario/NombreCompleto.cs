using ObligatorioP3.LogicaNegocio.Excepciones.NombreCompleto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Usuario
{
    [ComplexType]
    public record NombreCompleto
    {
        public string Nombre { get; init; }
        public string Apellido { get; init; }

        public NombreCompleto()
        {

        }
        public NombreCompleto(string nombre, string apellido)
        {
            if (nombre == null || apellido == null)
                throw new NombreCompletoNoValidoException("El nombre y apellido del usuario no pueden ser nulos");
            if (!NombreValido(nombre))
                throw new NombreCompletoNoValidoException($"{nombre} no es un nombre válido ");
            if (!NombreValido(apellido))
                throw new NombreCompletoNoValidoException($"{apellido} no es un apellido válido ");
 
            Nombre = FormatearNombre(nombre);
            Apellido = FormatearNombre(apellido);
        }

        static bool NombreValido(string Nombre)
        {
            bool sinCaracteres = true;
            char[] caracteresNoPermitidos = { '-', '_', '.', ',', ';', ':', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '{', '}', '|', '\\', '/', '<', '>', '?', '~', '`', '\'', '\"', '+', '=', ' ', '\t', '\n', '\r' };


            foreach (char c in caracteresNoPermitidos)
            {
                if (Nombre.Contains(c))
                {
                    sinCaracteres = false;
                }
            }


            return Nombre.Length >= 2 && !Nombre.Any(c => char.IsDigit(c)) && sinCaracteres;
        }

        static string FormatearNombre(string nombre)
        {
            string ret = "";
            nombre = nombre.ToLower();
            ret = nombre[0].ToString().ToUpper();
            for(int i = 1; i < nombre.Length; i++)
            {
                ret += nombre[i];
            }

            return ret;
        }
    }
}
