using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Excepciones.Usuario
{
    public class ContrasenaNoValidaException:Exception
    {
        public ContrasenaNoValidaException()
        {
        }

        public ContrasenaNoValidaException(string? message) : base(message)
        {
        }

        public ContrasenaNoValidaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
