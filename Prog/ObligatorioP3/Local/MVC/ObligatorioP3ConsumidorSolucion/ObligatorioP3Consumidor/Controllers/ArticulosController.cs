using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObligatorioP3Consumidor.Models.Articulo;
using System.Net.Http;
using System.Text.Json;

namespace ObligatorioP3Consumidor.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7057/api/";
        //Configuracion para deserializar el json y evitar errores por mayusculas y minusculas
        private readonly JsonSerializerOptions _jsonOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public ArticulosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }



        [HttpGet]
        public IActionResult FiltrarArticulosPorFechaMovimiento()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login","Login");
            }
            return View(new FechasYArticulosDTO() { fecha1 = DateOnly.FromDateTime(DateTime.Today), fecha2 = DateOnly.FromDateTime(DateTime.Today), listaArticulos = null});
        }

        [HttpPost]
        public IActionResult FiltrarArticulosPorFechaMovimiento(DateOnly fecha1, DateOnly fecha2, int pagina)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login", "Login");
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", HttpContext.Session.GetString("token"));
                string fecha1String = fecha1.ToString();
                string fecha2String = fecha2.ToString();
                string fecha1StringReparada = fecha1String.Replace('/','-');
                string fecha2StringReparada = fecha2String.Replace('/','-');
                var respuestaArticulos = _httpClient.GetAsync($"Articulo/{fecha1StringReparada}/{fecha2StringReparada}/{pagina}").Result;
                var articulosJson = respuestaArticulos.Content.ReadAsStringAsync().Result;
                FechasYArticulosDTO fechasyarticulos = new FechasYArticulosDTO();
                fechasyarticulos.fecha1 = fecha1;
                fechasyarticulos.fecha2 = fecha2;
                if (respuestaArticulos.IsSuccessStatusCode)
                {
                    fechasyarticulos.listaArticulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloListarDto>>(articulosJson);
                    if (fechasyarticulos.listaArticulos.Count() == 0)
                        fechasyarticulos.listaArticulos = null;
                    ViewBag.Pagina = pagina++;
                    return View(fechasyarticulos);
                }
                else
                {
                    List<ArticuloListarDto> articuloListarDtos = new List<ArticuloListarDto>();
                   fechasyarticulos.listaArticulos = articuloListarDtos;
                   ViewBag.Error = "No hay articulos para mostrar";
                   return View(fechasyarticulos);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " +e.Message;
                return View(new FechasYArticulosDTO()
                {
                    fecha1 = DateOnly.FromDateTime(DateTime.Now), fecha2 = DateOnly.FromDateTime(DateTime.Now)
                });
            }
        }
    }
}
