﻿using ObligatorioP3.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesEntidades
{
    internal interface IMovimientoStock:IEntity,IValidable<MovimientoStock>
    {
    }
}