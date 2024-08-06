using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Excepciones.NombreCompleto
{
    public class NombreCompletoNoValidoException:Exception
    {
        public NombreCompletoNoValidoException()
        {
        }

        public NombreCompletoNoValidoException(string? message) : base(message)
        {
        }

        public NombreCompletoNoValidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
