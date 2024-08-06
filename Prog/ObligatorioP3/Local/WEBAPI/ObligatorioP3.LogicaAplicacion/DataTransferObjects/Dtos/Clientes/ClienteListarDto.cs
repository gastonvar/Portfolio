using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes
{
    public class ClienteListarDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public string RUT { get; set; }
        [Required]
        public string Calle { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public double Distancia { get; set; }
    }
}
