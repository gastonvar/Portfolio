using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento
{
    public class TipoDeMovimientoNoValidoException:Exception
    {
        public TipoDeMovimientoNoValidoException()
        {
        }

        public TipoDeMovimientoNoValidoException(string? message) : base(message)
        {
        }

        public TipoDeMovimientoNoValidoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
