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
    public class MovimientoListarDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int IdArticulo { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public int IdTipoMovmiento { get; set; }
        [Required]
        public string NombreTipoMovimiento { get; set; }
        [Required]
        public bool Aumento { get; set; }
        [Required]
        public int Coeficiente { get; set; }
    }
}
