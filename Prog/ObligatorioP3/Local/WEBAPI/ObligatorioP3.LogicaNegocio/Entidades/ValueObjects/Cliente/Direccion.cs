using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Cliente
{
    [ComplexType]
    public record Direccion
    {
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public int Numero { get; set; }
        public double Distancia { get; set; }

        public Direccion()
        {

        }
        public Direccion(string calle, string ciudad, int numero, double distancia)
        {
            Calle = calle;
            Ciudad = ciudad;
            Numero = numero;
            Distancia = distancia;
            EsValido();
        }

        public void EsValido()
        {
            if (Calle == null) throw new ArgumentNullException("Error, calle nula");
            if (Ciudad == null) throw new ArgumentNullException("Error, ciudad nula");
            if (Numero == null) throw new ArgumentNullException("Error, numero de local nulo");
            if (Distancia == null) throw new ArgumentNullException("Error, direccion nula");
        }
    }
}
