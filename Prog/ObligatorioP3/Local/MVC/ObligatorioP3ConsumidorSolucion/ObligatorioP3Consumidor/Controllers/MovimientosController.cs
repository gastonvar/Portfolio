using Microsoft.AspNetCore.Mvc;
using ObligatorioP3Consumidor.Models.Login;
using ObligatorioP3Consumidor.Models.Movimientos;
using ObligatorioP3Consumidor.Models.Articulo;
using ObligatorioP3Consumidor.Models.TiposMovimientos;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using NuGet.Common;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace ObligatorioP3Consumidor.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7057/api/";
        //Configuracion para deserializar el json y evitar errores por mayusculas y minusculas
        private readonly JsonSerializerOptions _jsonOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public MovimientosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_url);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login", "Login");
            }
            try
            {
                var respuestaArticulos = _httpClient.GetAsync("Articulo").Result;
                var respuestaTipos = _httpClient.GetAsync("TipoDeMovimiento").Result;
                var articulosJson = respuestaArticulos.Content.ReadAsStringAsync().Result;
                var tiposJson = respuestaTipos.Content.ReadAsStringAsync().Result;
                if (respuestaArticulos.IsSuccessStatusCode)
                {
                    if (respuestaTipos.IsSuccessStatusCode)
                    {
                        Selectores selectores = new Selectores();
                        selectores.Articulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloListarDto>>(articulosJson);
                        selectores.Tipos = JsonConvert.DeserializeObject<IEnumerable<TipoDeMovimientoListarDTO>>(tiposJson);
                        return View(selectores);
                    }
                    else
                    {
                        ViewBag.Error = respuestaTipos.Content.ReadAsStringAsync().Result;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = respuestaArticulos.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int articulo, int tipo, int cantidad)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login", "Login");
            }
            try
            {
                MovimientoStockAltaDTO dto = new MovimientoStockAltaDTO();
                dto.IdArticulo = articulo;
                dto.IdTipo = tipo;
                dto.Cantidad = cantidad;
                dto.EmailUsuario = HttpContext.Session.GetString("email");
                var json = System.Text.Json.JsonSerializer.Serialize(dto);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", HttpContext.Session.GetString("token"));
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = _httpClient.PostAsync("MovimientoStock", body).Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    TempData["Mensaje"] = $"Se registró el movimiento exitosamente";
                    return RedirectToAction("Create");
                }
                else
                {
                    var errorContent = respuesta.Content.ReadAsStringAsync();
                    TempData["Error"] = errorContent.Result;
                    return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public IActionResult ListarAgrupados()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login","Login");
            }
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", HttpContext.Session.GetString("token"));
                var respuesta = _httpClient.GetAsync("MovimientoStock").Result;
                var json = respuesta.Content.ReadAsStringAsync().Result;
                if (respuesta.IsSuccessStatusCode)
                {
                    var retorno = JsonConvert.DeserializeObject<IEnumerable<MovimientoListarAgrupadoDTO>>(json);
                    return View(retorno);
                }
                else
                {
                    var errorContent = respuesta.Content.ReadAsStringAsync();
                    TempData["Error"] = errorContent.Result;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult GetFiltrados(int idArticulo, int idTipo, int pagina)
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login", "Login");
            }
            try
            {
                if (HttpContext.Session.GetString("preseleccionMov") == "")
                {
                    HttpContext.Session.SetString("preseleccionMov", $"{idArticulo}-{idTipo}");
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", HttpContext.Session.GetString("token"));
                var respuestaArticulos = _httpClient.GetAsync("Articulo").Result;
                var respuestaTipos = _httpClient.GetAsync("TipoDeMovimiento").Result;
                var articulosJson = respuestaArticulos.Content.ReadAsStringAsync().Result;
                var tiposJson = respuestaTipos.Content.ReadAsStringAsync().Result;
                var respuestaMovimientos = _httpClient.GetAsync($"MovimientoStock/{idArticulo}/{idTipo}/{pagina}").Result;
                var movimientosJson = respuestaMovimientos.Content.ReadAsStringAsync().Result;
                MovimientosYselectores movimientosYselectores = new MovimientosYselectores();
                Selectores selectores = new Selectores();
                if (respuestaArticulos.IsSuccessStatusCode)
                {
                    if (respuestaTipos.IsSuccessStatusCode)
                    {
                        selectores.Articulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloListarDto>>(articulosJson);
                        selectores.Tipos = JsonConvert.DeserializeObject<IEnumerable<TipoDeMovimientoListarDTO>>(tiposJson);
                        movimientosYselectores.Selectores = selectores;
                        if (respuestaMovimientos.IsSuccessStatusCode)
                        {
                            IEnumerable<MovimientoListarDTO> movimientos = JsonConvert.DeserializeObject<IEnumerable<MovimientoListarDTO>>(movimientosJson);
                            movimientosYselectores.Movimientos = movimientos;
                            ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                            ViewBag.Pagina = pagina++;
                            return View("FiltroPorArticuloYtipo", movimientosYselectores);
                        }
                        else
                        {
                            List<MovimientoListarDTO> movimientos = new List<MovimientoListarDTO>();
                            movimientosYselectores.Movimientos = movimientos;
                            ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                            ViewBag.Error = "No hay movimientos para mostrar";
                            return View("FiltroPorArticuloYtipo", movimientosYselectores);
                        }
                    }
                    else
                    {
                        ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                        ViewBag.Error = "Error interno al cargar los tipos al selector";
                        return View("FiltroPorArticuloYtipo", movimientosYselectores);
                    }
                }
                else
                {
                    ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                    ViewBag.Error ="Error interno al cargar los articulos al selector";
                    return View("FiltroPorArticuloYtipo", movimientosYselectores);
                }
            }
            catch (Exception ex)
            {
                ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                ViewBag.Error = ex.Message;
                return View("FiltroPorArticuloYtipo");
            }
        }



       [HttpGet]
        public IActionResult FiltroPorArticuloYtipo()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                TempData["Error"] = "Debes loguearte";
                return RedirectToAction("Login", "Login");
            }
            try
            {
                HttpContext.Session.SetString("preseleccionMov", "");
                var respuestaArticulos = _httpClient.GetAsync("Articulo").Result;
                var respuestaTipos = _httpClient.GetAsync("TipoDeMovimiento").Result;
                var articulosJson = respuestaArticulos.Content.ReadAsStringAsync().Result;
                var tiposJson = respuestaTipos.Content.ReadAsStringAsync().Result;
                if (respuestaArticulos.IsSuccessStatusCode)
                {
                    if (respuestaTipos.IsSuccessStatusCode)
                    {
                        Selectores selectores = new Selectores();
                        selectores.Articulos = JsonConvert.DeserializeObject<IEnumerable<ArticuloListarDto>>(articulosJson);
                        selectores.Tipos = JsonConvert.DeserializeObject<IEnumerable<TipoDeMovimientoListarDTO>>(tiposJson);
                        MovimientosYselectores movimientosYselectores = new MovimientosYselectores();
                        movimientosYselectores.Selectores = selectores;
                        ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                        return View(movimientosYselectores);
                    }
                    else
                    {
                        ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                        ViewBag.Error = respuestaTipos.Content.ReadAsStringAsync().Result;
                        return View();
                    }
                }
                else
                {
                    ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                    ViewBag.Error = respuestaArticulos.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.preseleccionMov = HttpContext.Session.GetString("preseleccionMov");
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
