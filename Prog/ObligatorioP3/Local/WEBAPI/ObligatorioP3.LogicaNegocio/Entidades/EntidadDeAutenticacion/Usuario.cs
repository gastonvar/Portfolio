using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Comun;
using ObligatorioP3.LogicaNegocio.Entidades.ValueObjects.Usuario;
using ObligatorioP3.LogicaNegocio.Excepciones.Comun;
using ObligatorioP3.LogicaNegocio.Excepciones.NombreCompleto;
using ObligatorioP3.LogicaNegocio.Excepciones.Usuario;
using ObligatorioP3.LogicaNegocio.InterfacesEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion
{
    public class Usuario : IEntity, IValidable<Usuario>, IUsuario
    {
        #region Propiedades y constructores
        public int Id { get; set; }
        [Required, StringLength(1000, MinimumLength = 6)]
        public Email Email { get; set; }
        [Required, StringLength(1000, MinimumLength = 2)]
        public NombreCompleto NombreCompleto { get; set; }
        [Required]
        public Contrasena Contrasena { get; set; }
        [Required]
        public string Rol { get; set; }

        public Usuario()
        {

        }

        public Usuario(string email, string nombre, string apellido, string contra, string rol)
        {
            Email = new Email(email);
            NombreCompleto = new NombreCompleto(nombre, apellido);
            Contrasena = new Contrasena(contra);
            Rol = rol;
            EsValido();
        }

        public Usuario(int id, string email, string nombre, string apellido, string contra, string rol)
        {

            Id = id;
            Email = new Email(email);
            NombreCompleto = new NombreCompleto(nombre,apellido);
            Contrasena = new Contrasena(contra);
            Rol = rol;
            EsValido();
        }

        #endregion

        public void EsValido()
        {
            if (Id == null) throw new UsuarioNoValidoException("Error, id nulo");
            if (Email == null) throw new EmailNoValidoException("Error, email nulo");
            if (NombreCompleto == null) throw new NombreCompletoNoValidoException("Error, nombre y/o apellido nulos");
            if (Contrasena == null) throw new ContrasenaNoValidaException("Error, contrasena nula");
            if (Rol!="admin"||Rol!="encargado") throw new UsuarioNoValidoException("Error, rol inexistente");
        }

        public void ModificarDatos(Usuario obj)
        {
            if (obj == null) throw new ArgumentNullException("Los datos para modificar el usuario son invalidos");
            obj.EsValido();
            this.Email = obj.Email;
            this.NombreCompleto = obj.NombreCompleto;
            this.Contrasena.ContrasenaNoEncriptada = obj.Contrasena.ContrasenaNoEncriptada;
            this.Contrasena.ContrasenaEncriptada = EncriptarContraseña(obj.Contrasena.ContrasenaNoEncriptada);
            this.Rol = obj.Rol;
        }
        /// <summary>
        /// Metodo de encriptacion de contraseña mediante SHA256
        /// </summary>
        /// <param name="contraseña">String de contraseña a encriptar</param>
        /// <returns>String recibido encriptado</returns>
        public static string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en una matriz de bytes
                byte[] contraseñaBytes = Encoding.UTF8.GetBytes(contraseña);

                // Calcular el hash de la contraseña
                byte[] hashBytes = sha256.ComputeHash(contraseñaBytes);

                // Convertir el hash en una cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
