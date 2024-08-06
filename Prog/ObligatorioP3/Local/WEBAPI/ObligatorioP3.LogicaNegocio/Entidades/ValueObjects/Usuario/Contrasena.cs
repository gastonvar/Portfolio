using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Usuario
{
    [ComplexType]
    public record Contrasena
    {
        public string ContrasenaNoEncriptada { get; set; }
        public string? ContrasenaEncriptada { get; set; }

        public Contrasena()
        {

        }
        public Contrasena(string contra)
        {
            if (contra == null) throw new ArgumentNullException("Error, contraseña nula");
            if (!ValidarContrasena(contra)) throw new ContrasenaNoValidaException("Error, contrasena invalida, largo mínimo 6 y may/min dígito y \\\".;,!\\\" -\"");
            ContrasenaNoEncriptada = contra;
        }
        public Contrasena(string contra, string contraEncriptada)
        {
            if (contra == null) throw new ArgumentNullException("Error, contraseña nula");
            if (!ValidarContrasena(contra)) throw new ContrasenaNoValidaException("Error, contrasena invalida, largo mínimo 6 y may/min dígito y \".;,!\" -");
            ContrasenaNoEncriptada = contra;
            ContrasenaEncriptada = contraEncriptada;
        }
        static bool ValidarContrasena(string contra)
        {
            bool contieneMin = false;
            bool contieneMay = false;
            bool contieneSigno = false;

            foreach (char c in contra)
            {
                if (char.IsUpper(c))
                {
                    contieneMay = true;
                }
                else if (char.IsLower(c))
                {
                    contieneMin = true;
                }
                else if (char.IsDigit(c) || char.IsPunctuation(c))
                {
                    contieneSigno = true;
                }
            }

            return contra.Length >= 6 && contieneMin && contieneMay && contieneSigno && contra.Any(char.IsDigit);
        }

    }
}
