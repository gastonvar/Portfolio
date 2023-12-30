using ClasesObligatorioP2GVDS.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ClasesObligatorioP2GVDS
{
    public class Sistema
    {
        #region Singleton
        private static Sistema _instancia = null;
        private Sistema()
        {
            string s = Precarga();
        }

        public static Sistema GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Sistema();
            }
            return _instancia;
        }
        #endregion

        #region Listas
        //Declaracion de variables globales
        private List<Usuario> _listaUsuarios = new List<Usuario>();
        private List<Publicacion> _listaPublicaciones = new List<Publicacion>();
        private List<Invitacion> _listaInvitaciones = new List<Invitacion>();
        private List<Mensaje> _listaMensajes = new List<Mensaje>();
        #endregion

        #region Getters
        //Metodo para acceder a las listas
        public List<Usuario> GetUsuarios() { return _listaUsuarios; }
        public List<Publicacion> GetPublicaciones() { return _listaPublicaciones; }
        public List<Invitacion> GetInvitaciones() { return _listaInvitaciones; }
        public List<Mensaje> GetMensajes() { return _listaMensajes; }
        public List<Administrador> GetAdministradores()
        {
            List<Administrador> administradores = new List<Administrador>();
            foreach (Usuario u in _listaUsuarios)
            {
                if (u is Administrador)
                {
                    administradores.Add((Administrador)u);
                }
            }
            return administradores;
        }
        public List<Miembro> GetMiembros()
        {
            List<Miembro> miembros = new List<Miembro>();
            foreach (Usuario u in _listaUsuarios)
            {
                if (u is Miembro)
                {
                    miembros.Add((Miembro)u);
                }
            }
            return miembros;
        }

        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>();
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p is Post)
                {
                    posts.Add((Post)p);
                }
            }
            return posts;
        }

        public List<Comentario> GetComentarios()
        {
            List<Comentario> comentarios = new List<Comentario>();
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p is Comentario)
                {
                    comentarios.Add((Comentario)p);
                }
            }
            return comentarios;
        }

        #endregion

        #region Altas
        //Metodos de alta, validan al objeto recibido por parametro.

        //Recibe un usuario, lo valida y si no existe en el sistema lo agrega
        public void AltaUsuario(Usuario u)
        {
            try
            {
                u.EsValido();
                if (!_listaUsuarios.Contains(u))
                {
                    _listaUsuarios.Add(u);
                }
                else
                {
                    throw new Exception("Error, usuario ya registrado");
                }
            }
            catch (Exception e)
            {
                Usuario.ReducirId();
                throw e;
            }

        }

        //Valida la publicacion recibida como parametro y la agrega a la lista
        public void AltaPublicacion(Publicacion p)
        {
            try
            {
                p.EsValido();
                _listaPublicaciones.Add(p);
            }
            catch (Exception e)
            {
                Publicacion.ReducirId();
                throw e;
            }


        }

        //Valida la invitacion recibida como parametro y si aun no existe en el sistema entonces la agrega.
        public void AltaInvitacion(Invitacion i)
        {
            try
            {
                i.EsValido();
                if (!_listaInvitaciones.Contains(i))
                {
                    _listaInvitaciones.Add(i);
                }
                else
                {
                    throw new Exception("Invitacion ya existente");
                }
            }
            catch (Exception e)
            {
                Invitacion.ReducirId();
                throw e;
            }


        }

        //Alta mensaje
        public void AltaMensaje(Mensaje m)
        {
            try
            {
                m.EsValido();
                _listaMensajes.Add(m);
            }
            catch (Exception e)
            {
                Mensaje.ReducirId();
                throw e;
            }
        }
        #endregion


        //Recibe dos fechas como parametro, las valida y retorna una lista con los post realizados entre esas dos fechas
        //Si el contenido del post es mayor a 50 caracteres
        public List<Post> ObtenerPostEntre2Fechas(DateTime fec1, DateTime fec2)
        {
            if (fec1 < fec2)
            {
                List<Post> listaP = new List<Post>();
                foreach (Publicacion p in _listaPublicaciones)
                {
                    if (p.FechaPublicacion >= fec1 && p.FechaPublicacion <= fec2)
                    {
                        if (p is Post)
                        {
                            Post pAux1 = (Post)p;
                            Post pAux2 = new Post(pAux1.Autor, pAux1.Titulo, RecortarTexto(pAux1.Contenido), pAux1.Imagen, pAux1.Privado, pAux1.Id);
                            listaP.Add(pAux2);
                        }
                    }
                }

                if (listaP.Count > 0)
                {
                    listaP.Sort();
                    return listaP;
                }
                else
                {
                    throw new Exception($"Error, ninguna publicación encontrada entre {fec1} y {fec2}");
                }
            }
            else
            {
                throw new Exception("Error, fecha 1 es posterior a fecha 2");
            }


        }

        //Obtiene un miembro y busca las publicaciones de ese miembro en la lista de publicaciones, las agrega a una lista auxiliar y la retorna
        public List<Publicacion> BuscarPublicacionesXUsuario(Miembro miembro)
        {

            List<Publicacion> listaDePublicacionesXUsuario = new List<Publicacion>();
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p.Autor.Equals(miembro))
                {
                    listaDePublicacionesXUsuario.Add(p);
                }
            }
            if (listaDePublicacionesXUsuario.Count > 0)
            {
                return listaDePublicacionesXUsuario;
            }
            else
            {
                throw new Exception("Error, no se han encontrado publicaciones asociadas a este usuario.");
            }



        }

        //Si el texto recibido es mayor a 50 caracteres entonces lo recorta hasta 50, de otra manera lo devuelve.
        public string RecortarTexto(string txt)
        {
            if (txt.Length > 50)
            {
                string nuevotxt = "";
                for (int i = 0; i < 50; i++)
                {
                    nuevotxt += txt[i];
                }
                return nuevotxt;
            }
            else
            {
                return txt;
            }

        }

        //Obtiene un email y busca en el sistema un miembro con ese email asociado, si no lo encuentra lanza excepcion
        public Miembro GetMiembroXEmail(string email)
        {
            Miembro miembro = null;
            bool encontrado = false;
            for (int i = 0; i < _listaUsuarios.Count && !encontrado; i++)
            {
                Usuario u = _listaUsuarios[i];
                if (u.Email.ToLower().Equals(email.ToLower()))
                {
                    miembro = (Miembro)u;
                    encontrado = true;
                }
            }
            if (encontrado)
            {
                return miembro;
            }
            else
            {
                throw new Exception("Error, miembro no presente en el sistema.");
            }

        }

        //Lista los posts segun un miembro recibido como parametro.
        public List<Publicacion> ListarPostComentadosXMiembro(Miembro m)
        {
            List<Publicacion> retorno = new List<Publicacion>();
            //Recorremos la lista de posts.
            foreach (Post p in GetPosts())
            {
                //Recorremos los comentarios del post.
                foreach (Comentario c in p.GetListaComentarios())
                {
                    //Si el autor es el mismo que el recibido por parametro, guardamos el post en la lista.
                    if (c.Autor.Equals(m) && !retorno.Contains(p))
                    {
                        retorno.Add(p);
                    }
                }
            }
            if (retorno.Count > 0)
            {
                return retorno;
            }
            else
            {
                throw new Exception("Error, no se han encontrado posts asociados a los comentarios de este usuario.");
            }

        }

        //Devuelve una lista de miembros con la mayor cantidad de publicaciones
        public DTOMaximoCantidadPublicaciones MiembrosConMasPublicaciones()
        {
            int max = 0;
            List<Miembro> retorno = new List<Miembro>();
            foreach (Usuario u in _listaUsuarios)
            {
                if (u is Miembro)
                {
                    Miembro mAux = (Miembro)u;
                    if (GetCantidadPublicaciones(mAux) > max)
                    {
                        retorno.Clear();
                        retorno.Add(mAux);
                        max = GetCantidadPublicaciones(mAux);
                    }
                    else if (GetCantidadPublicaciones(mAux) == max)
                    {
                        retorno.Add(mAux);
                    }
                }
            }
            if (retorno.Count > 0)
            {
                DTOMaximoCantidadPublicaciones DTO = new DTOMaximoCantidadPublicaciones();
                DTO.ListaMiembros = retorno;
                DTO.Cantidad = max;
                return DTO;
            }
            else
            {
                throw new Exception("No existen publicaciones de miembros.");
            }
        }

        //Transforma una lista de posts en una lista de publicaciones
        public List<Publicacion> TransformarListaPostAPublicacion(List<Post> lista)
        {
            List<Publicacion> ret = new List<Publicacion>();
            foreach (Post p in lista)
            {
                ret.Add(p);
            }
            return ret;
        }

        //Obtiene la cantidad de publicaciones de un miembro buscandolas en el sistema
        public int GetCantidadPublicaciones(Miembro m)
        {
            int ret = 0;
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p.Autor.Equals(m))
                {
                    ret++;
                }
            }
            return ret;
        }

        //Recibe dos usuarios por parametros y pregunta si son amigos (metodo no utilizado en esta primera entrega.)
        public bool SonAmigosSistema(Miembro m1, Miembro m2)
        {
            return m1.GetListaAmigos().Contains(m2) && m2.GetListaAmigos().Contains(m1);
        }










        //Método que permite llamar y ejecutar la precarga desde el Program
        public string LlamarPrecarga()
        {
            return Precarga();
        }

        private string Precarga()
        {
            //Mensaje que se recibirá en el Program, si no hay errores será vacío. Si hay errores devolverá los mensajes de las Altas y Los Agregar.          
            string mensajesDeError = "";

            //Usuarios y Administrador

            #region PrecargaUsuarios (Miembros y admin)
            Usuario ADMIN = new Administrador("admin@gmail.com", "Admin1234");
            Usuario m1 = new Miembro("Gastón", "Varela", new DateTime(2004, 3, 28), "gas@gmail.com", "Gas1234");
            Usuario m2 = new Miembro("Joaquín", "Rodriguez", new DateTime(1986, 7, 1), "joaq@gmail.com", "Joaq1234");
            Usuario m3 = new Miembro("Ana", "Fernandéz", new DateTime(1990, 7, 12), "ana@gmail.com", "Ana1234");
            Usuario m4 = new Miembro("Juan", "Perez", new DateTime(1999, 4, 23), "juan@gmail.com", "Juan1234");
            Usuario m5 = new Miembro("Juanchi", "Juanza", new DateTime(2003, 8, 12), "juanchi@gmail.com", "Juanchi1234");
            Usuario m6 = new Miembro("Diego", "Sabatel", new DateTime(1986, 12, 29), "diego@gmail.com", "Diego1234");
            Usuario m7 = new Miembro("Alejandra", "Almeida", new DateTime(1988, 3, 23), "alejandra@gmail.com", "Alejandra1234");
            Usuario m8 = new Miembro("Jenny", "González", new DateTime(1994, 5, 18), "jenny@gmail.com", "Jenny1234");
            Usuario m9 = new Miembro("Julieta", "Vadetto", new DateTime(2000, 11, 2), "julieta@gmail.com", "Julieta1234");
            Usuario m10 = new Miembro("Pepe", "Pérez", new DateTime(2003, 12, 4), "pepe@gmail.com", "Pepe1234");
            Usuario m11 = new Miembro("Gaspar", "Mondadol", new DateTime(2003, 12, 4), "gaspar@gmail.com", "Gaspar1234");
            Usuario m12 = new Miembro("Carlos", "Rodríguez", new DateTime(1990, 7, 15), "carlos@gmail.com", "Carlos1234");
            Usuario m13 = new Miembro("Laura", "Martínez", new DateTime(1985, 9, 8), "laura@gmail.com", "Laura1234");
            Usuario m14 = new Miembro("Fernando", "López", new DateTime(1998, 2, 10), "fernando@gmail.com", "Fernando1234");
            Usuario m15 = new Miembro("Ana", "Sánchez", new DateTime(1992, 6, 25), "ana2@gmail.com", "Ana1234");
            Usuario m16 = new Miembro("Martín", "Gómez", new DateTime(1987, 11, 14), "martin@gmail.com", "Martin1234");
            Usuario m17 = new Miembro("Lucía", "Hernández", new DateTime(1995, 4, 3), "lucia@gmail.com", "Lucia1234");
            Usuario m18 = new Miembro("Pablo", "Díaz", new DateTime(2001, 8, 20), "pablo@gmail.com", "Pablo1234");
            Usuario m19 = new Miembro("Isabel", "Torres", new DateTime(1993, 10, 7), "isabel@gmail.com", "Isabel1234");
            Usuario m20 = new Miembro("Ricardo", "Ramírez", new DateTime(2002, 1, 30), "ricardo@gmail.com", "Ricardo1234");
            Usuario m21 = new Miembro("María", "Fernández", new DateTime(1989, 12, 12), "maria@gmail.com", "Maria1234");
            Usuario m22 = new Miembro("Jorge", "Gutiérrez", new DateTime(1996, 3, 5), "jorge@gmail.com", "Jorge1234");
            Usuario m23 = new Miembro("Natalia", "Luna", new DateTime(2000, 5, 18), "natalia@gmail.com", "Natalia1234");
            Usuario m24 = new Miembro("Andrés", "Ortega", new DateTime(1986, 9, 22), "andres@gmail.com", "Andres1234");
            Usuario m25 = new Miembro("Valentina", "Castro", new DateTime(1994, 4, 8), "valentina@gmail.com", "Valentina1234");
            Usuario m26 = new Miembro("Héctor", "Mendoza", new DateTime(1999, 11, 3), "hector@gmail.com", "Hector1234");
            Usuario m27 = new Miembro("Sofía", "Peralta", new DateTime(2004, 2, 15), "sofia@gmail.com", "Sofia1234");
            Usuario m28 = new Miembro("Eduardo", "Guerrero", new DateTime(1984, 7, 9), "eduardo@gmail.com", "Eduardo1234");
            Usuario m29 = new Miembro("Camila", "Rojas", new DateTime(1991, 12, 28), "camila@gmail.com", "Camila1234");
            Usuario m30 = new Miembro("Alejandro", "Navarro", new DateTime(1997, 6, 7), "alejandro@gmail.com", "Alejandro1234");
            List<Usuario> listaUsuariosPrecarga = new List<Usuario> { ADMIN, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19, m20, m21, m22, m23, m24, m25, m26, m27, m28, m29, m30 };

            foreach (Usuario m in listaUsuariosPrecarga)
            {
                try
                {
                    AltaUsuario(m);
                }
                catch (Exception e)
                {
                    mensajesDeError += "\nError en precarga miembro";
                }
            }

            #endregion

            //Invitaciones

            #region PrecargaInvitaciones
            Invitacion i1 = new Invitacion((Miembro)m1, (Miembro)m2);
            Invitacion i2 = new Invitacion((Miembro)m1, (Miembro)m3);
            Invitacion i3 = new Invitacion((Miembro)m1, (Miembro)m4);
            Invitacion i4 = new Invitacion((Miembro)m1, (Miembro)m5);
            Invitacion i5 = new Invitacion((Miembro)m1, (Miembro)m6);
            Invitacion i6 = new Invitacion((Miembro)m1, (Miembro)m7);
            Invitacion i7 = new Invitacion((Miembro)m1, (Miembro)m8);
            Invitacion i8 = new Invitacion((Miembro)m1, (Miembro)m9);
            Invitacion i9 = new Invitacion((Miembro)m1, (Miembro)m10);
            Invitacion i10 = new Invitacion((Miembro)m2, (Miembro)m3);
            Invitacion i11 = new Invitacion((Miembro)m2, (Miembro)m4);
            Invitacion i12 = new Invitacion((Miembro)m2, (Miembro)m5);
            Invitacion i13 = new Invitacion((Miembro)m2, (Miembro)m6);
            Invitacion i14 = new Invitacion((Miembro)m2, (Miembro)m7);
            Invitacion i15 = new Invitacion((Miembro)m2, (Miembro)m8);
            Invitacion i16 = new Invitacion((Miembro)m2, (Miembro)m9);
            Invitacion i17 = new Invitacion((Miembro)m2, (Miembro)m10);
            Invitacion i18 = new Invitacion((Miembro)m2, (Miembro)m11);
            Invitacion i19 = new Invitacion((Miembro)m1, (Miembro)m11);
            Invitacion i20 = new Invitacion((Miembro)m4, (Miembro)m10);
            Invitacion i21 = new Invitacion((Miembro)m5, (Miembro)m10);
            Invitacion i22 = new Invitacion((Miembro)m4, (Miembro)m8);
            Invitacion i23 = new Invitacion((Miembro)m5, (Miembro)m3);
            Invitacion i24 = new Invitacion((Miembro)m4, (Miembro)m6);
            Invitacion i25 = new Invitacion((Miembro)m5, (Miembro)m11);

            Invitacion i26 = new Invitacion((Miembro)m4, (Miembro)m11);
            Invitacion i27 = new Invitacion((Miembro)m5, (Miembro)m7);
            Invitacion i29 = new Invitacion((Miembro)m4, (Miembro)m5);


            Invitacion r1 = new Invitacion((Miembro)m6, (Miembro)m7);
            Invitacion r2 = new Invitacion((Miembro)m7, (Miembro)m8);
            Invitacion r3 = new Invitacion((Miembro)m8, (Miembro)m9);

            Invitacion p1 = new Invitacion((Miembro)m3, (Miembro)m8);
            Invitacion p2 = new Invitacion((Miembro)m4, (Miembro)m7);
            Invitacion p3 = new Invitacion((Miembro)m5, (Miembro)m6);
            List<Invitacion> listaInvitacionAceptadaPrecarga = new List<Invitacion> { i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15, i16, i17, i18, i19, i20, i21, i22, i23, i24, i25, i26, i27, i29 };
            List<Invitacion> listaInvitacionRechazadaPrecarga = new List<Invitacion> { r1, r2, r3 };
            List<Invitacion> listaInvitacionPendientePrecarga = new List<Invitacion> { p1, p2, p3 };
            //Aceptadas
            foreach (Invitacion i in listaInvitacionAceptadaPrecarga)
            {
                try
                {
                    AltaInvitacion(i);
                    i.AceptarInvitacion();
                }
                catch (Exception e)
                {

                    mensajesDeError += $"\nPrecarga: {e.Message} Id invitacion: {i.Id}";
                }
            }
            //Rechazadas
            foreach (Invitacion i in listaInvitacionRechazadaPrecarga)
            {
                try
                {
                    AltaInvitacion(i);
                    i.RechazarInvitacion();
                }
                catch (Exception e)
                {

                    mensajesDeError += $"\nPrecarga: {e.Message} Id invitacion: {i.Id}";
                }
            }
            //Pendientes
            foreach (Invitacion i in listaInvitacionPendientePrecarga)
            {
                try
                {
                    AltaInvitacion(i);
                }
                catch (Exception e)
                {

                    mensajesDeError += $"\nPrecarga: {e.Message} Id invitacion:{i.Id}";
                }
            }

            #endregion

            //Posts

            #region Posts
            Publicacion pos1 = new Post((Miembro)m1, "Agua salada", "Las aguas saladas del océano en Uruguay son conocidas por sus tonos azules y verdes, ofreciendo hermosas playas y oportunidades para actividades marinas. Tomo agua salada todas las mañanas y tengo los riñones como piña.", "oceano.png", false);
            Publicacion pos2 = new Post((Miembro)m2, "Karate", "El karate es un antiguo arte marcial japonés que se ha convertido en una disciplina globalmente reconocida y respetada.", "karate.png", false);
            Publicacion pos3 = new Post((Miembro)m3, "Caballos", "Los caballos en Uruguay son parte esencial de su cultura, utilizados en el trabajo rural y en emocionantes deportes como el polo. Su presencia en la Semana Criolla y en estancias de renombre demuestra su importancia en la vida uruguaya. ", "caballo.png", false);
            Publicacion pos4 = new Post((Miembro)m4, "Vacaciones", "Las vacaciones son un bálsamo para el alma, un merecido descanso en medio de la rutina diaria.", "playa.jpg", true);
            Publicacion pos5 = new Post((Miembro)m5, "Dinosaurios", "Los dinosaurios, esos gigantes antiguos, siguen fascinándonos. Su legado fósil nos conecta con un pasado misterioso y asombroso. ¿Cuál es tu dinosaurio favorito? 🦕🦖 #Dinosaurios ", "dinosaurio.jpg", true);
            List<Publicacion> listaPostsPrecarga = new List<Publicacion> { pos1, pos2, pos3, pos4, pos5 };
            foreach (Publicacion p in listaPostsPrecarga)
            {
                try
                {
                    AltaPublicacion(p);
                }
                catch (Exception e)
                {

                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }
            #endregion

            //Comentarios y Reacciones

            #region Comentarios
            Publicacion com1 = new Comentario((Miembro)m1, "ME ENCANTA EL AGUAS SALADA", "AMO EL AGUA");
            Publicacion com2 = new Comentario((Miembro)m2, "DEJA DE SER TAN NABO", "Como te va a gustar el agua salada, salame");
            Publicacion com3 = new Comentario((Miembro)m3, "Joaco, porque lo tratas mal?", "Te parece bien? Todo bien en casa?");

            Publicacion com4 = new Comentario((Miembro)m4, "gente violenta!!", "A ver si se calman un poco, así no se puede!!");
            Publicacion com5 = new Comentario((Miembro)m5, "El mejor arte", "De todos los que existen es el mejor!!, escucho comentarios");
            Publicacion com6 = new Comentario((Miembro)m6, "mi abuela empezó Karate!!!", "Tiene 104 años y es media rayada");

            Publicacion com7 = new Comentario((Miembro)m7, "Como hago para darle de comer a los caballos?", "Hace dos meses les estoy intentando dar milanesas y ni las tocan");
            Publicacion com8 = new Comentario((Miembro)m8, "Para al de arriba", "Como le vas a dar milanesas al caballo?");
            Publicacion com9 = new Comentario((Miembro)m9, "Que lindos!", "Amo la mortadela");

            Publicacion com10 = new Comentario((Miembro)m10, "Estoy en Rocha!!", "Rocha es el mejor lugar para vacacionar....");
            Publicacion com11 = new Comentario((Miembro)m1, "Para que sirven las vacaciones??", "Es ese ansiado tiempo en el que dejamos atrás las preocupaciones laborales y nos sumergimos en un mundo de posibilidades y aventuras. Ya sea explorando destinos exóticos, relajándonos en la playa con el sonido del mar como banda sonora o simplemente disfrutando de la tranquilidad en casa, las vacaciones nos ofrecen la oportunidad de recargar energías, reconectar con seres queridos y crear recuerdos que perdurarán en nuestro corazón.");
            Publicacion com12 = new Comentario((Miembro)m2, "Que lindas son las licencias", "AL FIN VACACIONESSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS!!!!!!!, ya no podía mas con mi laburo!!");

            Publicacion com13 = new Comentario((Miembro)m3, "Los dinosaurios no existen", "Nunca vi uno");
            Publicacion com14 = new Comentario((Miembro)m4, "Para el de arriba", "Tu cerebro no existe");
            Publicacion com15 = new Comentario((Miembro)m5, "TAS LOCO", "TODOS LOS POSTS SON ASI? DONDE ESTAN LOS MODS");
            List<Publicacion> listaComentariosPrecarga = new List<Publicacion> { com1, com2, com3, com4, com5, com6, com7, com8, com9, com10, com11, com12, com13, com14, com15 };



            foreach (Publicacion p in listaComentariosPrecarga)
            {
                try
                {
                    AltaPublicacion(p);
                }

                catch (Exception e)
                {

                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }

            //Para cargar los comentarios en la lista de comentarios de cada post
            //Creamos auxiliares para que los post y los comentarios dejen de ser publicaciones
            //Y así poder usar el método Agregar comentario
            Post pAux1 = (Post)pos1;
            List<Publicacion> listaComentariospAux1 = new List<Publicacion> { com1, com2, com3 };
            foreach (Publicacion p in listaComentariospAux1)
            {
                try
                {
                    pAux1.AgregarComentario((Comentario)p);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }

            Post pAux2 = (Post)pos2;
            List<Publicacion> listaComentariospAux2 = new List<Publicacion> { com4, com5, com6 };
            foreach (Publicacion p in listaComentariospAux2)
            {
                try
                {
                    pAux2.AgregarComentario((Comentario)p);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }
             
            Post pAux3 = (Post)pos3;
            
            List<Publicacion> listaComentariospAux3 = new List<Publicacion> { com7, com8, com9 };
            foreach (Publicacion p in listaComentariospAux3)
            {
                try
                {
                    pAux3.AgregarComentario((Comentario)p);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }


            Post pAux4 = (Post)pos4;
            List<Publicacion> listaComentariospAux4 = new List<Publicacion> { com10, com11, com12 };
            foreach (Publicacion p in listaComentariospAux4)
            {
                try
                {
                    pAux4.AgregarComentario((Comentario)p);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }



            Post pAux5 = (Post)pos5;
            List<Publicacion> listaComentariospAux5 = new List<Publicacion> { com13, com14, com15 };
            foreach (Publicacion p in listaComentariospAux5)
            {
                try
                {
                    pAux5.AgregarComentario((Comentario)p);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {p.Id}";
                }
            }
            #endregion




            #region Reacciones
            //Reacciones post
            Reaccion rea1 = new Reaccion(true, (Miembro)m1);
            Reaccion rea2 = new Reaccion(true, (Miembro)m2);
            Reaccion rea3 = new Reaccion(true, (Miembro)m3);
            Reaccion rea4 = new Reaccion(false, (Miembro)m4);
            Reaccion rea5 = new Reaccion(false, (Miembro)m5);

            Reaccion rea6 = new Reaccion(false, (Miembro)m6);
            Reaccion rea7 = new Reaccion(false, (Miembro)m7);
            Reaccion rea8 = new Reaccion(false, (Miembro)m8);
            Reaccion rea9 = new Reaccion(true, (Miembro)m9);
            Reaccion rea10 = new Reaccion(true, (Miembro)m10);

            //Reacciones comentario
            Reaccion rea11 = new Reaccion(true, (Miembro)m1);
            Reaccion rea12 = new Reaccion(true, (Miembro)m2);
            Reaccion rea13 = new Reaccion(true, (Miembro)m3);
            Reaccion rea14 = new Reaccion(true, (Miembro)m4);
            Reaccion rea15 = new Reaccion(true, (Miembro)m5);

            Reaccion rea16 = new Reaccion(false, (Miembro)m6);
            Reaccion rea17 = new Reaccion(false, (Miembro)m7);
            Reaccion rea18 = new Reaccion(false, (Miembro)m8);
            Reaccion rea19 = new Reaccion(false, (Miembro)m9);
            Reaccion rea20 = new Reaccion(false, (Miembro)m10);
            List<Reaccion> reaccionesPost1 = new List<Reaccion> { rea1, rea2, rea3, rea4, rea5 };
            List<Reaccion> reaccionesPost2 = new List<Reaccion> { rea6, rea7, rea8, rea9, rea10 };

            foreach (Reaccion r in reaccionesPost1)
            {
                try
                {
                    pos1.AltaReaccion(r);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {pos1.Id}";

                }
            }
            foreach (Reaccion r in reaccionesPost2)
            {
                try
                {
                    pos2.AltaReaccion(r);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {pos2.Id}";

                }
            }

            List<Reaccion> reaccionesComentario1 = new List<Reaccion> { rea11, rea12, rea13, rea14, rea15 };
            List<Reaccion> reaccionesComentario2 = new List<Reaccion> { rea16, rea17, rea18, rea19, rea20 };
            foreach (Reaccion r in reaccionesComentario1)
            {
                try
                {
                    com1.AltaReaccion(r);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {com1.Id}";
                }
            }
            foreach (Reaccion r in reaccionesComentario2)
            {
                try
                {
                    com2.AltaReaccion(r);
                }
                catch (Exception e)
                {
                    mensajesDeError += $"\nPrecarga: {e.Message} Id publicacion: {com2.Id}";
                }
            }


            #endregion

            return mensajesDeError;

        }
        //Método que comprueba que la precarga de las listas fue exitosa.
        public string ComprobarPrecarga()
        {
            if (_listaInvitaciones.Count < 34 || _listaPublicaciones.Count < 20 || _listaUsuarios.Count < 12)
            {
                return "HA HABIDO UN ERROR INESPERADO EN LA PRECARGA";
            }
            else
            {
                return "La precarga ha resultado exitosa.";
            }
        }

        //Busca en los usuarios alguno en el que correspondan email y contraseña
        public Usuario Login(string email, string pass)
        {
            foreach (Usuario u in _listaUsuarios)
            {
                if (u.Email.Equals(email) && u.Contrasenia.Equals(pass))
                {
                    return u;
                }
            }
            return null;
        }
        //Busca un miembro por id
        public Miembro BuscarMiembroXId(int? id)
        {
            foreach (Miembro u in GetMiembros())
            {
                if (u.Id.Equals(id))
                {
                    return u;
                }
            }
            return null;
        }
        //Bloquea un Miembro
        public void BloquearMiembro(Miembro aBloquear)
        {
            if (aBloquear.Bloqueado)
            {
                aBloquear.Bloqueado = false;
            }
            else
            {
                aBloquear.Bloqueado = true;
            }

        }

        public Publicacion BuscarPublicacionXId(int id)
        {
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }
        //Busca un post por su id
        public Post BuscarPostXId(int id)
        {
            foreach (Post p in GetPosts())
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }
        //Busca un comentario por su id
        public Comentario BuscarComentarioXId(int id)
        {
            foreach(Comentario c in GetComentarios())
            {
                if (c.Id.Equals(id))
                {
                    return c;
                }
            }
            return null;
        }
        //Banea un post
        public void BanearPost(Post aBanear)
        {
            if (aBanear.Censurado)
            {
                aBanear.Censurado = false;
            }
            else
            {
                aBanear.Censurado = true;
            }

        }
        //Crea una invitacion mediante la id de los usuarios
        public void CrearInvitacionXId(int? id1, int? id2)
        {
            if (id1 != null && id2 != null)
            {
                Miembro u1 = BuscarMiembroXId(id1);
                Miembro u2 = BuscarMiembroXId(id2);

                if(u1!=null && u2 != null)
                {
                    Invitacion i = new Invitacion(u1, u2);
                    AltaInvitacion(i);
                }
                else
                {
                    throw new Exception("ERROR UNO DE LOS MIEMBROS ES NULO");
                }

            }
            else
            {
                throw new Exception("ERROR UNO DE LOS MIEMBROS ES NULO");
            }

        }
        //Busca las invitaciones del miembro donde este es solicitado
        public List<Invitacion> GetInvitacionesXMiembro(Miembro m)
        {
            List<Invitacion> ret = new List<Invitacion>();
            foreach (Invitacion i in _listaInvitaciones)
            {
                if (i.Solicitado.Equals(m) && !ret.Contains(i))
                {
                    ret.Add(i);
                }
            }
            return ret;
        }
        //Busca una invitacion donde los id recibidos por parametros esten presentes
        public Invitacion? BuscarInvitacionXIdMiembros(int? id1, int? id2)
        {
            foreach (Invitacion i in _listaInvitaciones)
            {

                if (i.Solicitado.Id == id1 && i.Solicitante.Id == id2 || i.Solicitado.Id == id2 && i.Solicitante.Id == id1)
                {
                    return i;
                }


            }
            throw new Exception("Invitacion inexistente");
        }
        //Filtra los posts segun los requerimentos (censurado, amistad, autor, privacidad)
        public List<Post>? GetPostsFiltradosParaMiembros(int? id)
        {
            if (id != null)
            {
                int idaux = (int)id;

                List<Post> posts = new List<Post>();
                foreach (Publicacion p in _listaPublicaciones)
                {
                    if (p is Post post)
                    {
                        if (!post.Censurado) {
                            if (post.Privado && post.Autor.SonAmigosXId(id) || !post.Censurado && !post.Privado)
                            {
                                posts.Add((Post)p);
                            }
                        }

                    }
                }
                return posts;
            }
            return null;
        }
        //Devuelve una lista de miembros que no son amigos del miembro con id recibida en parametro
        public List<Miembro> GetMiembrosFiltradosXAmistad(int? id)
        {
            List<Miembro> ret = new List<Miembro>();
            foreach(Miembro m in GetMiembros())
            {
                if (!m.SonAmigosXId(id) && m.Id!=id)
                {
                    ret.Add(m);
                }
            }
            return ret;
        }
        //Busca publicaciones que contengan el criterio y tengan un VA superior al recibido
        public List<Publicacion> BuscarPublicacionesXCriterioVA(string criterio, int va)
        {
            List<Publicacion> ret = new List<Publicacion>();
            if(criterio == null)
            {
                criterio = "";
            }
            foreach(Publicacion p in _listaPublicaciones)
            {
                if(p.Titulo.ToLower().Contains(criterio.ToLower()) && p.CalcularVA() > va || p.Contenido.ToLower().Contains(criterio.ToLower()) && p.CalcularVA() > va)
                {
                    ret.Add(p);
                }
            }
            return ret;
        }

        public List<Miembro> ObtenerAmigosDe(int id)
        {
            List<Miembro> ret = new List<Miembro>();
            foreach (Miembro m in GetMiembros())
            {
                if (SonAmigosSistema(BuscarMiembroXId(id), m))
                {
                    ret.Add(m);
                }
            }
            return ret;
        }

        public List<Mensaje> ObtenerChatDe(int id1, int id2)
        {
            List<Mensaje> ret = new List<Mensaje>();
            foreach(Mensaje m in _listaMensajes)
            {
                if(m.IdRecibe == id1 && m.IdEnvia == id2 || m.IdEnvia == id1 && m.IdRecibe == id2)
                {
                    ret.Add(m);
                }
            }
            return ret;
        }
    }
}
