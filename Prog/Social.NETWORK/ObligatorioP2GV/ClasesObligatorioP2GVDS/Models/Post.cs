using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClasesObligatorioP2GVDS
{
    public class Post : Publicacion, IComparable<Post>
    {
        private List<Comentario> _listaComentarios = new List<Comentario>();
        public string Imagen { get; set; }
        public bool Privado { get; set; }
        public bool Censurado { get; set; }

        public Post() : base()
        {

        }

        public Post(Miembro autor, string titulo, string contenido, string imagen, bool privado) : base(autor, titulo, contenido)
        {
            Imagen = imagen;
            Privado = privado;
            Censurado = false;
        }

        //Constructor para crear copias
        public Post(Miembro autor, string titulo, string contenido, string imagen, bool privado, int id) : base(autor, titulo, contenido, id)
        {
            Imagen = imagen;
            Privado = privado;
            Censurado = false;
        }

        //Validaciones.
        public override void EsValido()
        {
            base.EsValido();
                if (string.IsNullOrEmpty(Imagen))
                {
                    throw new Exception($"Imagen no valida");
                }
                string extensionImagen = Path.GetExtension(Imagen);
                if (extensionImagen != ".png" || extensionImagen != ".jpg")
                {
                    throw new Exception($"Formato imagen no valida");
                }
        }

        //Valida un comentario recibido por parametro y lo agrega a la lista de comentarios del post.
        //Prevé si el comentario es privado o no. Si no lo es, chequea que los miembros sean amigos.
        public void AgregarComentario(Comentario c)
        {
            if (!Privado)
            {
                c.EsValido();
                _listaComentarios.Add(c);
            }
            else
            {
                if (Autor.SonAmigos(c.Autor))
                {
                    c.EsValido();
                    _listaComentarios.Add(c);
                }
                else
                {
                    throw new Exception("Error, usted no es amigo del dueño del post");
                }
            }

        }

        //Método que devuelve la lista de comentarios del post
        public List<Comentario> GetListaComentarios() { return _listaComentarios; }

        public override string ToString()
        {
            return base.ToString() + $"\nPOST:\n[Titulo: {Titulo}]\n[Imagen: {Imagen}][Contenido: {Contenido}]\n[Valor de aceptacion: {CalcularVA()}]\n[Privado: {BoolATexto(Privado)}][Censurado: {BoolATexto(Censurado)}]\n\n";
        }

        //Método para no mostrar un bool (True o False) en ingles en el ToString.
        public string BoolATexto(bool x)
        {
            if (x)
            {
                return "SI";
            }
            else
            {
                return "NO";
            }
        }
        //Agrega la parte adicional a CalcularVA no presente en Publicacion y Comentario
        public override int CalcularVA()
        {
            int ret = base.CalcularVA();
            if (!Privado)
            {
                ret = ret + 10;
            }
            return ret;

        }

        


        public int CompareTo(Post? other)
        {
            if (Titulo.CompareTo(other.Titulo)>0)
            {
                return -1;
            }else if (Titulo.CompareTo(other.Titulo) < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
