using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesObligatorioP2GVDS
{
    public class Reaccion : IValidacion
    {
        public bool Like { get; set; }
        public Miembro Miembro { get; set; }

        public Reaccion()
        {

        }

        public Reaccion(bool like, Miembro miembro)
        {
            Like = like;
            Miembro = miembro;
        }
        
        //Validaciones
        public void EsValido()
        {
            if (Miembro == null)
            {
                throw new Exception("Error miebro nulo");
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Reaccion reaccion &&
                   reaccion.Miembro.Equals(Miembro);
        }
    }
}
