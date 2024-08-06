using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.MovimientosStock
{
    public class MovimientoStockAltaDTO
    {
        [Required]
        public int IdArticulo { get; set; }
        [Required]
        public int IdTipo { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public string emailUsuario { get; set; }
    }
}
