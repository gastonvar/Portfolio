using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClasesObligatorioP2GVDS
{
    public abstract class Publicacion : IValidacion
    {
        private static int _ultimoId { get; set; }
        public int Id { get; set; }
        public Miembro Autor { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaPublicacion { get; set; }
        private List<Reaccion> _listaReacciones = new List<Reaccion>();

        protected Publicacion()
        {
            Id = _ultimoId++;
            FechaPublicacion = DateTime.Now;
        }

        protected Publicacion(Miembro autor, string titulo, string contenido)
        {
            Id = _ultimoId++;
            Autor = autor;
            Titulo = titulo;
            Contenido = contenido;
            FechaPublicacion = DateTime.Now;
        }

        //Constructor para crear copias:
        protected Publicacion(Miembro autor, string titulo, string contenido, int id)
        {
            Id = id;
            Autor = autor;
            Titulo = titulo;
            Contenido = contenido;
            FechaPublicacion = DateTime.Now;
        }
        //Devuelve la lista de reacciones de la publicacion
        public List<Reaccion> GetReacciones() { return _listaReacciones; }
        
        //Valida una reaccion recibida por parametro y la agrega a la lista de reacciones de la publicacion
        public void AltaReaccion(Reaccion r)
        {
            try
            {
                r.EsValido();

                if (!ContieneReaccionDe(r.Miembro))
                {
                    _listaReacciones.Add(r);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {

                throw new Exception("Usted ya reaccionó");
            }
 
        }
        //Busca si entre las reacciones existe alguna del miembro recibido en parametro
        private bool ContieneReaccionDe(Miembro miembro)
        {
            foreach(Reaccion r in _listaReacciones)
            {
                if (r.Miembro == miembro)
                {
                    return true;
                }
            }
            return false;
        }

        //Validaciones
        public virtual void EsValido()
        {

                if (this == null)
                {
                    throw new Exception($"Publicación nula");
                }
                Autor.EsValido();
                if (String.IsNullOrEmpty(Titulo))
                {
                    throw new Exception($"Titulo no valido");
                }
                if (String.IsNullOrEmpty(Contenido))
                {
                    throw new Exception($"Titulo no valido");
                }
        }

            
        //Es llamada en caso de error de validacion
        public static void ReducirId()
        {
            _ultimoId--;
        }
        //Cuenta los likes de la publicacion
        public int ContarLikes()
        {
            int ret = 0;
            foreach (Reaccion r in _listaReacciones)
            {
                if (r.Like)
                {
                    ret++;
                }
            }
            return ret;
        }
        //Cuenta los dislikes de la publicacion
        public int ContarDislikes()
        {
            int ret = 0;
            foreach (Reaccion r in _listaReacciones)
            {
                if (!r.Like)
                {
                    ret++;
                }
            }
            return ret;
        }

        //Metodo virtual para calcular el valor de aceptacion de la publicacion, es virtual pues Comentario lo utiliza como esta declarado abajo, Post necesita un paso extra.
        public virtual int CalcularVA()
        {
            return ContarLikes() * 5 + ContarDislikes() * -2;
        }

        public override string ToString()
        {
            return $"[ID: {Id}][Fecha: {FechaPublicacion}][Autor: {Autor.Nombre}]";
        }
    }
}
