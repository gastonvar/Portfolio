using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios
{
    public class LoginAPI : ILoginAPI
    {
        private IRepositorioUsuario _repo;
        public LoginAPI(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public Usuario? Ejecutar(string email, string contra)
        {
            Usuario? usuEncontrado = _repo.GetByEmail(email);
            if (usuEncontrado != null)
            {
                if (usuEncontrado.Contrasena.ContrasenaEncriptada == Usuario.EncriptarContraseña(contra))
                {
                    return usuEncontrado;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
