using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios
{
    public interface IAltaUsuario
    {
        void Ejecutar(UsuarioAltaDto dto);
    }
}
