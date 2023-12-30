using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesObligatorioP2GVDS
{
    public class Comentario : Publicacion
    {
        public Comentario() : base()
        {

        }

        public Comentario(Miembro autor, string titulo, string contenido) : base(autor, titulo, contenido)
        {
        }

        //Aplica la validacion de Publicacion
        public override void EsValido()
        {
            base.EsValido();
        }
       
        public override string ToString()
        {
            return base.ToString() + $"\nCOMENTARIO:\n[Contenido: {Contenido}]\n[Fecha de publicacion: {FechaPublicacion}]\n[Valor de aceptacion: {CalcularVA()}]\n\n";
        }

        //Devuelve el metodo CalcularVA base, heredado de Publicacion
        public override int CalcularVA()
        {
            return base.CalcularVA();
        }

    }
}
