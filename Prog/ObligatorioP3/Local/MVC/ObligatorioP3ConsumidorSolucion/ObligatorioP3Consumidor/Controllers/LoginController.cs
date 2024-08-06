using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP3Consumidor.Models.Login;
using System.Text.Json;
using System.Text;

namespace ObligatorioP3Consumidor.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7057/api/";
        //Configuracion para deserializar el json y evitar errores por mayusculas y minusculas
        private readonly JsonSerializerOptions _jsonOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginModel);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = _httpClient.PostAsync("Usuario/Login", body).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var content = respuesta.Content.ReadAsStringAsync().Result;

                    var token = JsonSerializer.Deserialize<LoginToken>(content, _jsonOptions);
                    if (token == null)
                    {
                        ViewBag.Error = "No se ha podido deserializar el token";
                        return View(loginModel);
                    }
                    if (token.Rol == "admin")
                    {
                        ViewBag.Error = "Anda pal obligatorio 1 SOS ADMIN";
                        return View();
                    }
                    HttpContext.Session.SetString("token", token.Token);
                    HttpContext.Session.SetString("rol", token.Rol);
                    HttpContext.Session.SetString("email", token.Email);
                    //Agregar el token a las cabeceras de las peticiones, para que el servidor lo pueda validar
                    //No olvidar que el token debe ser enviado en todas las peticiones
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");
                    return RedirectToAction("Create", "Movimientos");
                }
                else
                {
                    ViewBag.Error = respuesta.Content.ReadAsStringAsync().Result;
                    return View(loginModel);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(loginModel);
            }
        }

        public ActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("rol");
            HttpContext.Session.Remove("email");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            TempData["Mensaje"] = "Cierre de sesión correcto";
            return RedirectToAction("Login");
        }
    }
}
