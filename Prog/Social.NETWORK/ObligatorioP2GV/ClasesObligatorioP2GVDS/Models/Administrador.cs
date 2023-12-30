using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesObligatorioP2GVDS
{
    public class Administrador : Usuario
    {
        public Administrador(string email, string contrasenia) : base(email, contrasenia)
        {
            //Usuario se lleva a su constructor todos los parametros, pues Administrador no difiere en atributos con su clase base.
        }
        public Administrador() : base()
        {

        }
        //Aplica la validacion de Usuario
        public override void EsValido()
        {
            base.EsValido();
        }
        //Aplica el ToString de Usuario
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
