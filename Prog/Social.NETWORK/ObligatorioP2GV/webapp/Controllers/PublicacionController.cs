using ClasesObligatorioP2GVDS;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    public class PublicacionController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        private IWebHostEnvironment Environment;

        public PublicacionController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }


        //Lista los posts segun el rol del usuario logueado
        //Si el usuario logueado es un miembro, devuelve una vista que muestra posts y comentarios
        //Si el usuario logueado es un administrador, devuelve una vista que solo muestra posts 
        public IActionResult Listar()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null)
            {
                if (HttpContext.Session.GetString("LogueadoRol") == "a")
                {
                    return View("ListarPostAdmin",s.GetPosts());
                }
                if(HttpContext.Session.GetString("LogueadoRol") == "m")
                {
                    //Devuelve una lista de posts filtrados segun las restricciones de visualizacion (amistad, privado, bloqueado, propio)
                    return View("ListarPostMiembro", s.GetPostsFiltradosParaMiembros(HttpContext.Session.GetInt32("LogueadoId")));
                }
            }
            return RedirectToAction("Index", "Home");

        }

        //El administrador puede banear un post
        public IActionResult Banear(int id) {
            if (HttpContext.Session.GetString("LogueadoRol") == "a")
            {
                Post aBanear = s.BuscarPostXId(id);
                s.BanearPost(aBanear);
                List<Post> lp = s.GetPosts();
                return RedirectToAction("Listar", "Publicacion", lp);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        //El miembro le da like a una publicacion
        public IActionResult DarLike(int id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                Publicacion c = s.BuscarPublicacionXId(id);
                try
                {
                    c.AltaReaccion(new Reaccion(true, s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"))));
                    TempData["msgPost"] = $"Diste [Like] al {c.GetType().Name}: " + c.Titulo;
                }
                catch (Exception)
                {
                    TempData["msgPost"] = $"Ya habias reaccionado al {c.GetType().Name}: " + c.Titulo;
                }
                return View("ListarPostMiembro", s.GetPostsFiltradosParaMiembros(HttpContext.Session.GetInt32("LogueadoId")));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           }
        //El miembro le da dislike a una publicacion
        public IActionResult DarDisLike(int id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                Publicacion c = s.BuscarPublicacionXId(id);
                try
                {
                    c.AltaReaccion(new Reaccion(false, s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"))));
                    TempData["msgPost"] = $"Diste [Dislike] al {c.GetType().Name}: " + c.Titulo;
                }
                catch (Exception)
                {
                    TempData["msgPost"] = $"Ya habias reaccionado al {c.GetType().Name}: " + c.Titulo;
                }
                return View("ListarPostMiembro", s.GetPostsFiltradosParaMiembros(HttpContext.Session.GetInt32("LogueadoId")));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Devuelve una vista con un form para realizar un posteo
        public IActionResult Postear()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol") == "m")
            {
            if (!s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId")).Bloqueado)
            {
                    return View();
                }
            else
            {
                    TempData["msgIndex"] = "USUARIO BANEADO: Usted no puede realizar posts";
             }
            }
            return RedirectToAction("Index", "Home");

        }

        //El miembro postea
        [HttpPost]
        public IActionResult Postear(Post p, IFormFile archivo)
        {
            p.Autor = s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"));
            if(archivo != null)
            {
                try
                {
                    string ruta = Environment.WebRootPath + "//img//Post//";
                    string extension = Path.GetExtension(archivo.FileName);
                    string filename = "p" + p.Id + extension;
                    if (extension == ".jpg" || extension == ".png")
                    {
                        FileStream stream = new FileStream(ruta + filename, FileMode.Create);
                        archivo.CopyTo(stream);
                        stream.Close();

                        p.Imagen = filename;
                        s.AltaPublicacion(p);
                        ViewBag.msgPostear = "Publicado correctamente";
                    }
                    else
                    {
                        throw new Exception("Formato de imagen no valido");
                    }
                }
                catch (Exception e)
                {

                    ViewBag.msgPostear = "Ha ocurrido un error: " + e.Message;
                }
            }
            else
            {
                ViewBag.msgPostear = "Falta imagen";
            }
            return View();
        }

        //El miembro comenta un post
        [HttpPost]
        public IActionResult ComentarPost(string titulo, string contenido,int postId)
        {
            if (!s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId")).Bloqueado)
            {
                try
                {
                    Comentario c = new Comentario(s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId")), titulo, contenido);
                    Post p = s.BuscarPostXId(postId);
                    s.AltaPublicacion(c);
                    p.AgregarComentario(c);
                    TempData["msgPost"] = "Comentaste correctamente el post: " + p.Titulo;
                }
                catch (Exception e)
                {
                    TempData["msgPost"] = "Ha habido un error al comentar: " + e.Message;
                }
                return View("ListarPostMiembro", s.GetPostsFiltradosParaMiembros(HttpContext.Session.GetInt32("LogueadoId")));
            }
            else
            {
                TempData["msgIndex"] = "USUARIO BANEADO: Usted no puede comentar posts";
                return RedirectToAction("Index", "Home");
            }
        }

        //Muestra el buscador para los miembros
        public IActionResult Buscador()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol")=="m")
            {
                return View(s.GetPublicaciones());
            }
            return RedirectToAction("Index","Home");
        }
        //Recibe el criterio y el VA, va al sistema a buscar las publicaciones que contengan el criterio y tengan
        //va superior al recibido como parametro
        [HttpPost]
        public IActionResult Buscador(string criterio, int va)
        {
            List<Publicacion> listaFiltrada = s.BuscarPublicacionesXCriterioVA(criterio, va);
            return View(listaFiltrada);
        }
    }
}
