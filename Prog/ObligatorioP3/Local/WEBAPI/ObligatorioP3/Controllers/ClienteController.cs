using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Clientes;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Clientes;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;

namespace ObligatorioP3.Web.Controllers
{
    public class ClienteController : Controller
    {
        private IRepositorioCliente _repoClientes = new RepositorioClienteEF(new ObligatorioP3Context());
        private IFiltrarClientes _filtrarClientes;
        private IGetAllClientes _getAllClientes;
        
        
        public ClienteController()
        {
            _filtrarClientes = new FiltrarClientes(_repoClientes);
            _getAllClientes = new GetAllClientes(_repoClientes);
        }


        /// <summary>
        /// Lista de todos los clientes
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    var clientes = _getAllClientes.Ejecutar();
                    if (clientes == null || clientes.Count() == 0)
                    {
                        ViewBag.Mensaje = "No existen clientes / lista de clientes vacia";
                        return View();
                    }
                    ViewBag.Mensaje = $"Hay {clientes.Count()} en total";
                    return View(clientes);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
            else
            {
                TempData["Mensaje"] = "DEBES LOGUEARTE";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Metodo post que captura los valores de busqueda ingresados
        /// </summary>
        /// <param name="txt">Texto a buscar en las razones sociales</param>
        /// <param name="money">Monto para filtrar la suma total de todos los pedidos del cliente</param>
        /// <returns>Vistas segun resultados</returns>
        [HttpPost]
        public IActionResult Index(string txt, string money)
        {
            IEnumerable<ClienteListarDto> clientesFiltrados = null;
            try
            {
                if(txt!=null && money != null)
                {
                    ViewBag.Error = "Solo utilizar un metodo para filtrar";
                    return RedirectToAction(nameof(Index));
                }
                if(txt==null && money == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (txt != null) //Si el texto no es nulo, se ejecuta el caso de uso con el metodo filtrar texto
                {
                    clientesFiltrados = _filtrarClientes.FiltrarXTexto(txt);
                }
                if (money != null) //Si el money no es nulo, se ejecuta el mismo caso de uso con el metodo filtrar monto
                {
                    decimal numX = decimal.Parse(money);
                    clientesFiltrados = _filtrarClientes.FiltrarXMonto(numX);
                }
                if (clientesFiltrados == null || clientesFiltrados.Count() == 0)
                {
                    ViewBag.Mensaje = "No existen clientes con esas caracteristicas";
                    return View();
                }
                ViewBag.Mensaje = $"Hay {clientesFiltrados.Count()} en total";
                return View(clientesFiltrados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}
