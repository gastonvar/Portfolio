using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento
{
    public class TipoDeMovimientoListarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Aumento { get; set; }
        public int Coeficiente { get; set; }
        
    }
}
