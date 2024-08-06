using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.TiposDeMovimiento
{
    public class TipoDeMovimientoModificacionDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Aumento { get; set; }
    }
}
