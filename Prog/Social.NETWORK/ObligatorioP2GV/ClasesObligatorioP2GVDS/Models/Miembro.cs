using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesObligatorioP2GVDS
{
    public class Miembro : Usuario, IComparable<Miembro>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        private List<Miembro> _listaAmigos = new List<Miembro>();
        public bool Bloqueado { get; set; }


        public Miembro(string nombre, string apellido, DateTime fechaNacimiento, string email, string contrasenia) : base(email, contrasenia)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Bloqueado = false;            
        }

        public Miembro() : base()
        {
            Bloqueado = false;
        }
        //Validaciones
        public override void EsValido()
        {
            base.EsValido();
                if (String.IsNullOrEmpty(Nombre))
                {
                    throw new Exception($"Nombre invalido");
                }
                if (String.IsNullOrEmpty(Apellido))
                {
                    throw new Exception($"Apellido invalido");
                }
                if(Nombre.Contains(' ')||Apellido.Contains(' '))
                {
                    throw new Exception($"Nombre o Apellido con espacio");
                }
                if (!(char.IsUpper(Nombre[0]) && char.IsUpper(Apellido[0])))
                {
                    throw new Exception($"Nombre o Apellido sin mayuscula");
                }
                if (FechaNacimiento < new DateTime(1900, 12, 31))
                {
                    throw new Exception($"Fecha no valida");
                }
        }
        //Devuelve la lista de amigos del miembro
        public List<Miembro> GetListaAmigos()
        {
            return _listaAmigos;
        }
        //Agrega un amigo a un miembro
        public void AgregarAmigo(Miembro m)
        {
            _listaAmigos.Add(m);
        }

        //Le pregunta a el miembro en que se invoca la funcion si el miembro recibido por parametro está incluido en su lista de amigos,
        //Tambien comprueba si el parametro recibido es uno mismo, se sobreentiende que un miembro es amigo de si mismo.
        public bool SonAmigos(Miembro m1)
        {
            return _listaAmigos.Contains(m1) || m1.Equals(this);
        }
        public bool SonAmigosXId(int? id)
        {
            //Busca a un miembro con la id recibida por parametro dentro de la lista de amigos
            foreach(Miembro m in _listaAmigos)
            {
                if(m.Id == id)
                {
                    //Si lo encuentra devuelve true
                    return true;
                }
            }
            if(this.Id == id)
            {
                //Si no lo encuentra se pregunta si es su misma id, si este es el caso entonces devuelve true
                return true;
            }
            return false;
        }

        //En la porcion final del ToString en la parte 
        public override string ToString()
        {
            return base.ToString() + $"Miembro: [Nombre: {Nombre}]\n[Apellido: {Apellido}]\n[Fecha de nacimiento: {FechaNacimiento}]";
        }


public int CompareTo(Miembro? other)
        {
            if (Apellido[0].CompareTo(other.Apellido[0]) > 0)
            {
                return 1;
            }
            else if (Apellido[0].CompareTo(other.Apellido[0]) < 0)
            {
                return -1;
            }
            else
            {
                if (Nombre[0].CompareTo(other.Nombre[0]) > 0)
                {
                    return 1;
                }else if(Nombre[0].CompareTo(other.Nombre[0]) < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
