using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.InterfacesEntidades
{
    public interface IUsuario
    {
        void ModificarDatos(Usuario obj);
        //string EncriptarContraseña(string contraseña); No se puede declarar en la clase como metodo estatico, preferimos dejarlo estatico y quitarlo de la interfaz.
    }
}
