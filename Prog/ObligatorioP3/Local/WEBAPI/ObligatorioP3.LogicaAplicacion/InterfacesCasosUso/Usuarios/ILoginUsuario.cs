using Microsoft.Extensions.Primitives;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios
{
    public interface ILoginUsuario
    {
        public Usuario? Ejecutar(string email, string contra);
    }
}
