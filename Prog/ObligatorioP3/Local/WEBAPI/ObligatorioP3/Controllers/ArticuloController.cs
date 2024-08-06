using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;

namespace ObligatorioP3.Web.Controllers
{
    public class ArticuloController : Controller
    {
        private IRepositorioArticulo _repoArticulos = new RepositorioArticuloEF(new ObligatorioP3Context());
        private IAltaArticulo _altaArticulo;
        private IGetAllArticulos _getAllArticulos;
        public ArticuloController()
        {
            _altaArticulo = new AltaArticulo(_repoArticulos);
            _getAllArticulos = new GetAllArticulos(_repoArticulos);
        }

        /// <summary>
        /// Retorna la vista del índice donde se listan los artículos
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    //El metodo getall en el repositorio los devuelve ordenados
                    var articulos = _getAllArticulos.Ejecutar();

                    if (articulos == null || articulos.Count() == 0)
                    {
                        ViewBag.Error = "No existen articulos";
                    }
                    ViewBag.Info = $"Hay {articulos.Count()} artículos registrados en total";
                    return View(articulos);
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
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Retorna la vista de creación de artículo
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Este método es el que se llama para crear el artículo, se comunica con el caso de uso
        /// </summary>
        /// <param name="artDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloAltaDto artDto)
        {
            try
            {
                _altaArticulo.Ejecutar(artDto);
                TempData["Mensaje"] = $"Artículo {artDto.Nombre} - {artDto.Codigo} creado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
