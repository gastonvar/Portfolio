﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3Consumidor.Models.Movimientos
{
    public class MovimientoListarAgrupadoDTO
    {
        public string Ano {  get; set; }
        public IEnumerable<MovimientoCantidadDto> MovimientoCantidad { get; set;}
        public int Total {  get; set; }
    }
}
