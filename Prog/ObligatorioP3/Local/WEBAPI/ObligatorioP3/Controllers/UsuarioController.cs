using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Usuarios;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Usuarios;
using ObligatorioP3.LogicaNegocio.Entidades.EntidadDeAutenticacion;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.Web.Models.UsuariosModels;

namespace ObligatorioP3.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private IRepositorioUsuario _repoUsuarios = new RepositorioUsuarioEF(new ObligatorioP3Context());
        private IAltaUsuario _altaUsuario;
        private IEliminarUsuario _eliminarUsuario;
        private IModificarUsuario _modificarUsuario;
        private ILoginUsuario _loginUsuario;
        private IGetAllUsuarios _getAllUsuarios;
        private IGetUsuario _getUsuario;
        public UsuarioController()
        {
            _altaUsuario = new AltaUsuario(_repoUsuarios);
            _eliminarUsuario = new EliminarUsuario(_repoUsuarios);
            _modificarUsuario = new ModificarUsuario(_repoUsuarios);
            _loginUsuario = new LoginUsuario(_repoUsuarios);
            _getAllUsuarios = new GetAllUsuarios(_repoUsuarios);
            _getUsuario = new GetUsuario(_repoUsuarios);
        }
        /// <summary>
        /// Lista de usuarios
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    var usuarios = _getAllUsuarios.Ejecutar();

                    if (usuarios == null || usuarios.Count() == 0)
                    {
                        ViewBag.Error = "No existen usuarios";
                    }
                    ViewBag.Info = $"Hay {usuarios.Count()} usuarios registrados en total";
                    return View(usuarios);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
            else
            {
                ViewBag.Error = "DEBES LOGUEARTE";
                return RedirectToAction("Index","Home");
            }
           
        }
        /// <summary>
        /// Devuelve la vista del login
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return View();
            }
            else
            {
                ViewBag.Error = "Ya hay un usuario logueado";
                return RedirectToAction("Index","Home");
            }
        }
        /// <summary>
        /// Se busca un usuario en el sistema mediante los datos del usuarioLogin
        /// </summary>
        /// <param name="usuarioLogin">Es un model de la vista de login que contiene un Email y una Contraseña</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UsuarioLoginModel usuarioLogin)
        {

            Usuario? usu = _loginUsuario.Ejecutar(usuarioLogin.Email, usuarioLogin.Contrasena);
            if (usu != null)
            {
                HttpContext.Session.SetString("Rol", "admin");
                HttpContext.Session.SetString("Email", usu.Email.ValorEmail);
                HttpContext.Session.SetInt32("Id",usu.Id);
                TempData["Info"] = $"Bienvenid@ {usu.NombreCompleto.Nombre + " " + usu.NombreCompleto.Apellido}, has iniciado sesion correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "ERROR AL INICAR SESION";
                return RedirectToAction("Index","Home");
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Usuario");
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                return View();
            }
            else
            {
                TempData["Mensaje"] = "DEBES LOGUEARTE";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioAltaDto usuDto)
        {
            try
            {
                _altaUsuario.Ejecutar(usuDto);
                TempData["Mensaje"] = $"Usuario {usuDto.Nombre} {usuDto.Apellido} creado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                if (id == null)
                {
                    ViewBag.Error = "Se requiere el id del autor";
                    return RedirectToAction("Index", "Home");
                }
                else
                    try
                    {
                        UsuarioListarDto dto = _getUsuario.GetById(id);
                        UsuarioModificacionDto mod = new UsuarioModificacionDto()
                        {
                            Id = dto.Id,
                            Nombre = dto.Nombre,
                            Apellido = dto.Apellido,
                            Email = dto.Email,
                            Contrasena = dto.Contrasena,
                            ContrasenaEncriptada = dto.ContrasenaEncriptada,
                            Rol = dto.Rol
                        };
                        if (dto != null)
                        {
                            return View(mod);
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                return View();
            }
            else
            {
                ViewBag.Mensaje = "DEBES LOGUEARTE";
                return RedirectToAction("Index","Home");
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioModificacionDto usuDto)
        {
            try
            {
                _modificarUsuario.Ejecutar(id, usuDto);
                return RedirectToAction("Index","Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuDto);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    UsuarioListarDto dto = _getUsuario.GetById(id.GetValueOrDefault());
                    if (dto != null) return View(dto);
                    ViewBag.Mensaje = $"No existen usuarios con el id {id}";
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
            else
            {
                ViewBag.Mensaje = "DEBES LOGUEARTE";
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, UsuarioListarDto dto)
        {
            try
            {
                ViewBag.Mensaje = $"Usuario {dto.Email} eliminado correctamente";
                _eliminarUsuario.Ejecutar(id.GetValueOrDefault());
                return RedirectToAction("Index","Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ha ocurrido un error al eliminar al usuario: {dto.Email}: "+ex.Message;
                return View();
            }
        }
    }
}
