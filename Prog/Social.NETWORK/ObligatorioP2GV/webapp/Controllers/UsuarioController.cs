using Microsoft.AspNetCore.Mvc;
using ClasesObligatorioP2GVDS;
using ClasesObligatorioP2GVDS.Models;

namespace webapp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        //Hace el login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
                Usuario buscada = s.Login(email, pass);

                if (buscada != null)
                {
                    //se tiene que logear
                    HttpContext.Session.SetInt32("LogueadoId", buscada.Id);
                    HttpContext.Session.SetString("LogueadoEmail", buscada.Email);
                    if (buscada is Miembro)
                    {
                        HttpContext.Session.SetString("LogueadoRol", "m");
                    }
                    else if (buscada is Administrador)
                    {
                        HttpContext.Session.SetString("LogueadoRol", "a");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.msg = "Credenciales incorrectas";
                    return View();
                }
        }
        //Hace el registro
        public IActionResult Registro()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Registro(Miembro m)
        {
                try
                {
                s.AltaUsuario(m);
                Login(m.Email, m.Contrasenia);
                return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                ViewBag.msg = "ERROR AL REGISTRARSE: " + e.Message;
                return View();
                }
        }
        //Limpia la sesion
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //Lista los usuarios ordenados para el Administrador
        public IActionResult Listar()
        {
            
            if (HttpContext.Session.GetString("LogueadoId") != null &&HttpContext.Session.GetString("LogueadoRol") == "a")
            {
                List<Miembro> lm = s.GetMiembros();
                lm.Sort();
                return View(lm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        //Bloquea un usuario (solo el admin es capaz de hacerlo)
        public IActionResult Bloquear(int id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "a")
            {
                if (id != null && id != 0)
                {
                    Miembro aBloquear = s.BuscarMiembroXId(id);
                    s.BloquearMiembro(aBloquear);
                    List<Miembro> lm = s.GetMiembros();
                    return RedirectToAction("Listar", "Usuario", lm);
                }
            }
            return RedirectToAction("index", "Home");
        }
        //Muestra la lista de usuarios para enviar solicitud de amistad
        public IActionResult Invitar()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol")=="m")
            {
                return View(s.GetMiembrosFiltradosXAmistad(HttpContext.Session.GetInt32("LogueadoId")));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        //Recibe un id de usuario y le envia la invitacion de amistad
        public IActionResult EnviarInvitacion(int id)
        {
            if(HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                try
                {
                    s.CrearInvitacionXId(HttpContext.Session.GetInt32("LogueadoId"), id);
                    Miembro m = s.BuscarMiembroXId(id);
                    TempData["msgInvitar"] = "Invitacion enviada a: " + m.Nombre + " " + m.Apellido;
                }
                catch (Exception e)
                {
                    TempData["msgInvitar"] = e.Message;
                }
                return RedirectToAction("Invitar", "Usuario", s.GetMiembros());

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        //Muestra las invitaciones de amistad recibidas
        public IActionResult ResponderInvitacion()
        {

            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                Miembro m = s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"));

                return View(s.GetInvitacionesXMiembro(m));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //Recibe un id de usuario, busca la invitacion y la acepta
        public IActionResult AceptarInvitacion(int id)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                try
                {
                    Invitacion i = s.BuscarInvitacionXIdMiembros(HttpContext.Session.GetInt32("LogueadoId"), id);
                    i.AceptarInvitacion();
                    Miembro mX = s.BuscarMiembroXId(id);
                    TempData["msgInvitacion"] = "Invitacion aceptada a: " + mX.Nombre + " " + mX.Apellido;
                }
                catch (Exception e)
                {
                    TempData["msgInvitacion"] = e.Message + "o no ha iniciado sesion.";

                }
                Miembro m = s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"));
                return RedirectToAction("ResponderInvitacion", "Usuario", s.GetInvitacionesXMiembro(m));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            
        }
        //Recibe un id de usuario, busca la invitacion y la rechaza
        public IActionResult RechazarInvitacion(int id)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetString("LogueadoRol") == "m")
            {
                try
            {
                Invitacion i = s.BuscarInvitacionXIdMiembros(HttpContext.Session.GetInt32("LogueadoId"), id);
                i.RechazarInvitacion();
                    Miembro mX = s.BuscarMiembroXId(id);
                TempData["msgInvitacion"] = "Invitacion rechazada a: " + mX.Nombre + " " + mX.Apellido;
            }
            catch (Exception e)
            {
                TempData["msgInvitacion"] = e.Message + "o no ha iniciado sesion.";

            }
            Miembro m = s.BuscarMiembroXId(HttpContext.Session.GetInt32("LogueadoId"));
            return RedirectToAction("ResponderInvitacion", "Usuario", s.GetInvitacionesXMiembro(m));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        //Muestra a tus amigos
        public IActionResult VerAmigos()
        {
            if(HttpContext.Session.GetInt32("LogueadoId")!=null && HttpContext.Session.GetInt32("LogueadoId") != 0)
            {
                return View(s.ObtenerAmigosDe((int)HttpContext.Session.GetInt32("LogueadoId")));
            }
            return RedirectToAction("Index", "Home");
        }

        //Muestra el chat de esa persona
        public IActionResult EnviarMensaje(int id)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") != null && HttpContext.Session.GetInt32("LogueadoId") != 0)
            {
                ViewBag.NombreEnvia = s.BuscarMiembroXId((int)HttpContext.Session.GetInt32("LogueadoId")).Nombre;
                ViewBag.IdEnvia = s.BuscarMiembroXId((int)HttpContext.Session.GetInt32("LogueadoId")).Id;
                ViewBag.NombreRecibe = s.BuscarMiembroXId(id).Nombre;
                ViewBag.IdRecibe = id;
                ViewBag.IdAGuardar = id;
                return View(s.ObtenerChatDe((int)HttpContext.Session.GetInt32("LogueadoId"), id));
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult EnviarMensaje(int IdAGuardar, string Contenido)
        {
            Mensaje m = new Mensaje((int)HttpContext.Session.GetInt32("LogueadoId"),IdAGuardar,Contenido);
            s.AltaMensaje(m);
            return View(s.ObtenerChatDe((int)HttpContext.Session.GetInt32("LogueadoId"), IdAGuardar));
        }
    }
}
