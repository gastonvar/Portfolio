using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClasesObligatorioP2GVDS
{
    public abstract class Usuario : IValidacion
    {
        private static int _ultimoId = 0;
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }


        protected Usuario(string email, string contrasenia)
        {
            Id = _ultimoId++;
            Email = email;
            Contrasenia = contrasenia;

        }

        protected Usuario()
        {
            Id = _ultimoId++;
        }

        //Validaciones
        public virtual void EsValido()
        {

                if (this == null)
                {
                    throw new Exception($"Usuario nulo");
                }
                if (String.IsNullOrEmpty(Email))
                {
                    throw new Exception($"Email invalido");
                }
                if (!Email.Contains('@'))
                {
                    throw new Exception($"Email invalido, debe contener @");
                }
                if (Email[0] == '@')
                {
                    throw new Exception($"Email invalido, el @ no debe ir al inicio");
                }
                if (Email[Email.Length - 1] == '@')
                {
                    throw new Exception($"Email invalido, el @ no debe ir al final");
                }
                if (String.IsNullOrEmpty(Contrasenia) || !ValidarContraseniaExtras()|| Contrasenia.Length<5)
                {
                    throw new Exception($"Contraseña invalida, colocar al menos un número y una mayúscula");
                }
                if(Email.Contains(' ')||Contrasenia.Contains(' '))
                {
                throw new Exception($"Email o contraseña invalidos, no deben presentar espacios");
                }
          
        }
        //Es llamada en caso de error de validacion
        public static void ReducirId()
        {
            _ultimoId--;
        }

        //Método que valida la contraseña
        private bool ValidarContraseniaExtras()
        {

            bool mayus = false;
            bool num = false;
            foreach (char c in Contrasenia)
            {
                if (char.IsDigit(c))
                {
                    num = true;
                }
                if(char.IsUpper(c))
                {
                    mayus = true;
                }
            }
            return mayus && num;
        }


        public override string ToString()
        {
            return $"Usuario [ID: {Id}]\n[Email: {Email}]";
        }

        public override bool Equals(object? obj)
        {
            return obj is Usuario usuario &&
                   Email.ToLower() == usuario.Email.ToLower();
        }
    }
}
