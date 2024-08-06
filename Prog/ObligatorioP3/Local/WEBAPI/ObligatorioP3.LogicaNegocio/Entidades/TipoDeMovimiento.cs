using Microsoft.EntityFrameworkCore;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Usuario;
using ObligatorioP3.LogicaNegocio.Excepciones.TipoDeMovimiento;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    [Index(nameof(Nombre),IsUnique =true)]
    public class TipoDeMovimiento : ITipoDeMovimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Aumento { get; set; }

        public int Coeficiente { get; set; }
        public TipoDeMovimiento()
        {
            
        }
        public TipoDeMovimiento(int id, string nombre, bool aumento)
        {
            Id = id;
            Nombre = nombre;
            Aumento = aumento;
            if (Aumento) { Coeficiente = 1; }
            else { Coeficiente = -1; }
            EsValido();
        }

        public TipoDeMovimiento(string nombre, bool aumento)
        {
            Nombre = nombre;
            Aumento = aumento;
            if (Aumento) { Coeficiente = 1; }
            else { Coeficiente = -1; }
            EsValido();
        }

        public void EsValido()
        {
            if (this.Nombre == null) throw new TipoDeMovimientoNoValidoException("Error, nombre de tipo nulo");
            if (this.Aumento == null) throw new TipoDeMovimientoNoValidoException("Error, aumento de tipo nulo");
        }
        public void ModificarDatos(TipoDeMovimiento obj) { 
            this.Nombre = obj.Nombre;
            this.Aumento = obj.Aumento; 
            this.Coeficiente = obj.Coeficiente;
            EsValido();
        }
    }
}
