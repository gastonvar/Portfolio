using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Excepciones.Articulo
{
    public class ArticuloNoValidoException : Exception
    {
        public ArticuloNoValidoException()
        {
        }

        public ArticuloNoValidoException(string? message) : base(message)
        {
        }

        public ArticuloNoValidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
