using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos
{
    public class ArticuloAltaDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int Stock { get; set; }     
    }
}
