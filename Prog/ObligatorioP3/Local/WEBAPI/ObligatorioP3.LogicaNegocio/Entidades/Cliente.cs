using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Cliente;
using ObligatorioP3.LogicaNegocio.Excepciones.Cliente;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades
{
    public class Cliente : IEntity, IValidable<Cliente>, ICliente
    {
        public int Id { get; set; }
        [Required, StringLength(1000, MinimumLength = 6)]
        public string RazonSocial { get; set; }
        [Required, StringLength(12)]
        public string RUT { get; set; }
        public Direccion Direccion { get; set; }
        

        private Cliente()
        {

        }

        public Cliente(int id, string razonSocial, string rut, string calle, string ciudad, int numero, double distancia)
        {
            Id = id;
            RazonSocial = razonSocial;
            RUT = rut;
            Direccion = new Direccion(calle,ciudad,numero,distancia);
            EsValido();
        }

        public void EsValido()
        {
            if (RazonSocial == null) throw new ClienteNoValidoException("Error, razon social nula");
            if (RUT == null) throw new ClienteNoValidoException("Error, RUT nulo");
            if (RUT.Length != 12) throw new ClienteNoValidoException("Error, RUT debe poseer 12 digitos");
            if (!RUT.All(char.IsDigit)) throw new ClienteNoValidoException("Error, RUT debe ser solo digitos");
            if (Direccion == null) throw new ClienteNoValidoException("Error, direccion nula");
        }
    }
}
