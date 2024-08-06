using ObligatorioP3.LogicaNegocio.Excepciones.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun
{
    [ComplexType]
    public record Email
    {
        public string ValorEmail { get; init; }

        public Email()
        {

        }
        public Email(string valorEmail)
        {
            if (valorEmail == null) throw new ArgumentNullException(nameof(valorEmail), "Error, email nulo");
            ValidarEmail(valorEmail);
            ValorEmail = valorEmail;
        }

        public static void ValidarEmail(string valorEmail)
        {
            if (valorEmail.Length < 6 || !valorEmail.Contains("@")|| valorEmail[0]=='@' || valorEmail[valorEmail.Length-1]=='@' || !valorEmail.Contains(".") || valorEmail[valorEmail.Length-1]=='.')
                throw new EmailNoValidoException("Email no válido. Largo mínimo 6, debe incluir un arroba y dominio");
        }

        public int CompareTo(Email other)
        {
            if (other == null)
                return 1;

            return string.Compare(this.ValorEmail, other.ValorEmail, StringComparison.OrdinalIgnoreCase);
        }
    }
}
