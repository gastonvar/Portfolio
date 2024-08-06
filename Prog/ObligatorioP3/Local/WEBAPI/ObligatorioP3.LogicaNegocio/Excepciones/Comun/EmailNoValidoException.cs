using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Excepciones.Comun
{
    public class EmailNoValidoException:Exception
    {
        public EmailNoValidoException()
        {
        }

        public EmailNoValidoException(string? message) : base(message)
        {
        }

        public EmailNoValidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
