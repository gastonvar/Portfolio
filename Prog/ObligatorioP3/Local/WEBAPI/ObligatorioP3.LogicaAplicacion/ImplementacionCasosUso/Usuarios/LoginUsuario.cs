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
    public class LoginUsuario : ILoginUsuario
    {
        private IRepositorioUsuario _repositorioUsuario;
        public LoginUsuario(IRepositorioUsuario repo)
        {
            _repositorioUsuario = repo;
        }
        /// <summary>
        /// Valida que los datos ingresados conincidan con un usuario del sistema.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="contra">En este caso la contraseña llega sin encriptar, se la encripta y compara a la contraseña encriptada del usuario encontrado.</param>
        /// <returns></returns>
        public Usuario? Ejecutar(string email, string contra)
        {
            Usuario? usuEncontrado = _repositorioUsuario.GetByEmail(email);
            if (usuEncontrado != null)
            {
                if(usuEncontrado.Contrasena.ContrasenaEncriptada == Usuario.EncriptarContraseña(contra))
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
