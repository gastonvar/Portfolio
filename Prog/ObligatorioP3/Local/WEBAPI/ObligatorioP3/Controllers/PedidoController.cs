using Humanizer;
using Libreria.AccesoDatos.EF;
using Libreria.LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.AccesoDatos.EF;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Articulos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.ArticulosPedido;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Clientes;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Pedidos;
using ObligatorioP3.LogicaAplicacion.DataTransferObjects.Dtos.Usuarios;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Clientes;
using ObligatorioP3.LogicaAplicacion.ImplementacionCasosUso.Pedidos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Articulos;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Clientes;
using ObligatorioP3.LogicaAplicacion.InterfacesCasosUso.Pedidos;
using ObligatorioP3.LogicaNegocio.InterfacesRepositorios;
using ObligatorioP3.Web.Models.PedidosModels;

namespace ObligatorioP3.Web.Controllers
{
    public class PedidoController : Controller
    {
        private IRepositorioPedido _repoPedidos = new RepositorioPedidoEF(new ObligatorioP3Context());
        private IRepositorioParametro _repositorioParametro = new RepositorioParametroEF(new ObligatorioP3Context());
        private IAltaPedido _altaPedido;
        private IGetAllPedidos _getAllPedidos;
        private IGetAllPedidosAnulados _getAllPedidosAnulados;
        private IFiltrarPedidos _filtrarPedidos;
        private IGetPedido _getPedido;
        private IAnularPedido _anularPedido;

        private IRepositorioCliente _repoClientes = new RepositorioClienteEF(new ObligatorioP3Context());
        private IGetAllClientes _getAllClientes;
        private IGetCliente _getCliente;

        private IRepositorioArticulo _repoArticulos = new RepositorioArticuloEF(new ObligatorioP3Context());
        private IGetAllArticulos _getAllArticulos;
        private IGetArticulo _getArticulo;
        public PedidoController()
        {
            _altaPedido = new AltaPedido(_repoPedidos, _repositorioParametro);
            _getAllPedidos = new GetAllPedidos(_repoPedidos);
            _getAllPedidosAnulados = new GetAllPedidosAnulados(_repoPedidos);
            _filtrarPedidos = new FiltrarPedidos(_repoPedidos);
            _getPedido = new GetPedido(_repoPedidos);
            _anularPedido = new AnularPedido(_repoPedidos);

            _getAllClientes = new GetAllClientes(_repoClientes);
            _getCliente = new GetCliente(_repoClientes);

            _getAllArticulos = new GetAllArticulos(_repoArticulos);
            _getArticulo = new GetArticulo(_repoArticulos);
        }

        /// <summary>
        /// Retorna la visa del índice donde se listan todos los pedidos
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    HttpContext.Session.Remove("articulos");
                    var pedidos = _getAllPedidos.Ejecutar();

                    if (pedidos == null || pedidos.Count() == 0)
                    {
                        ViewBag.Error = "No existen pedidos";
                    }
                    ViewBag.Info = $"Hay {pedidos.Count()} pedidos registrados en total";
                    return View(pedidos);
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
        /// Ejecuta el caso de uso que filtra los pedidos sin entregar por fecha de emisión y retorna la vista del índice con esos resultados
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(DateTime? date)
        {
            IEnumerable<PedidoListarDto>? pedidosFiltrados = null;
            try
            {
                if (date == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (date != null)
                {
                    pedidosFiltrados = _filtrarPedidos.Filtrar((DateTime)date);
                }
                if (pedidosFiltrados == null || pedidosFiltrados.Count() == 0)
                {
                    ViewBag.Mensaje = "No existen pedidos pendientes emitidos en esta fecha";
                    return View();
                }
                ViewBag.Mensaje = $"Hay {pedidosFiltrados.Count()} en total";
                return View(pedidosFiltrados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Ejecuta el filtro de pedidos anulados y los retorna en la vista del índice
        /// </summary>
        /// <returns></returns>
        public IActionResult ListarAnulados()
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    var pedidosAnulados = _getAllPedidosAnulados.Ejecutar();

                    if (pedidosAnulados == null || pedidosAnulados.Count() == 0)
                    {
                        ViewBag.Error = "No existen pedidos anulados";
                    }
                    ViewBag.Info = $"Hay {pedidosAnulados.Count()} pedidos anulados en total";
                    return View("index", pedidosAnulados);
                }
                catch (Exception ex) 
                {
                    ViewBag.Error = ex.Message;
                    return View("index");
                }
            }
            else
            {
                TempData["Mensaje"] = "DEBES LOGUEARTE";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Retorna la vista de creación del pedido
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            try
            {
                if (HttpContext.Session.GetString("Rol") != null)
                {
                    PedidoAltaModel model = new PedidoAltaModel();
                    model.clientes = _getAllClientes.Ejecutar();
                    model.articulos = _getAllArticulos.Ejecutar();
                    model.lineas = new List<ArticulosPedidoDto>();
                    string lineasString = HttpContext.Session.GetString("articulos");
                    if (lineasString != null)
                    {
                        String[] articulosCant = lineasString.Split("/");
                        foreach (string i in articulosCant)
                        {
                            if (i != "")
                            {
                                String[] lineaString = i.Split("-");
                                ArticuloListarDto articulo = _getArticulo.Ejecutar(Int32.Parse(lineaString[0]));
                                if (articulo.Id == Int32.Parse(lineaString[0]))
                                {
                                    ArticulosPedidoDto linea = new ArticulosPedidoDto();
                                    linea.ArticuloListarDto = articulo;
                                    linea.Unidades = Int32.Parse(lineaString[1]);
                                    linea.PrecioUnitario = articulo.Precio;
                                    model.lineas.Add(linea);
                                }
                            }
                        }
                    }

                    return View(model);
                }
                else
                {
                    TempData["Mensaje"] = "DEBES LOGUEARTE";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("Index","Pedido");
            }
        }

        /// <summary>
        /// Maneja la generación de una variable de sesión que almacena los ids y cantidades de los artículos  que se agregan al pedido
        /// </summary>
        /// <param name="opcionSeleccionada"></param>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Agregar(int opcionSeleccionada, int cantidad)
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                if (HttpContext.Session.GetString("articulos") == null)
                {
                    HttpContext.Session.SetString("articulos", "");
                }
                if (cantidad <= 0)
                {
                    TempData["Error"] = "Debe seleccionar al menos 1 unidad";
                    return RedirectToAction("Create");
                }
                ArticuloListarDto articuloSeleccionado = null;
                var articulos = _getAllArticulos.Ejecutar();
                foreach (var articulo in articulos)
                {
                    if(articulo.Id == opcionSeleccionada)
                    {
                        articuloSeleccionado = articulo;
                        string lineas = HttpContext.Session.GetString("articulos");
                        if (!lineas.Contains($"{opcionSeleccionada}-"))
                        {
                            lineas += $"{opcionSeleccionada}-{cantidad}/";
                            HttpContext.Session.SetString("articulos", lineas);
                        }
                        else
                        {
                            TempData["Error"] = "Error, ese articulo ya ha sido agregado";
                            return RedirectToAction("Create");
                        }
                    }
                }

                TempData["Mensaje"] = $"Se agregaron {cantidad} unidades del artículo: {articuloSeleccionado.Nombre}";
                    return RedirectToAction("Create");
            }
            else
            {
                TempData["Mensaje"] = "DEBES LOGUEARTE";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Limpia la variable de sesión y elimina todos los artículos agregados
        /// </summary>
        /// <returns></returns>
        public IActionResult limpiar()
        {
            HttpContext.Session.Remove("articulos");
            return RedirectToAction("Create");

        }

        /// <summary>
        /// Crea el dto Pedido para dar de alta. para esto desglosa la variable de sesión y recupera todos los artículos necesarios para armar las líneas
        /// </summary>
        /// <param name="tipoPedido"></param>
        /// <param name="fechaEntrega"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(bool tipoPedido, DateTime fechaEntrega, int cliente)
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                   PedidoAltaDto pedidoAltaDto = new PedidoAltaDto();
                    pedidoAltaDto.LineasDto = new List<ArticulosPedidoDto>();
                    pedidoAltaDto.FechaEntrega = fechaEntrega;
                    pedidoAltaDto.ClienteDto = _getCliente.Ejecutar(cliente);
                    string lineasString = HttpContext.Session.GetString("articulos");
                    if (lineasString != null)
                    {
                        String[] articulosCantSesion = lineasString.Split("/");
                        foreach (string i in articulosCantSesion)
                        {
                            if (i != "")
                            {
                                String[] lineaString = i.Split("-");
                                ArticuloListarDto articulo = _getArticulo.Ejecutar(Int32.Parse(lineaString[0]));
                                    if (articulo != null)
                                    {
                                        ArticulosPedidoDto linea = new ArticulosPedidoDto();
                                        linea.ArticuloListarDto = articulo;
                                        linea.Unidades = Int32.Parse(lineaString[1]);
                                        linea.PrecioUnitario = articulo.Precio;
                                        pedidoAltaDto.LineasDto.Add(linea);
                                    }
                            }
                        }
                    }
                    else
                    {
                        TempData["Error"] = "El pedido no puede ir vacío";
                        return RedirectToAction("Create");
                    }
                    _altaPedido.Ejecutar(pedidoAltaDto, tipoPedido);
                    TempData["Mensaje"] = "El pedido ha sido creado correctamente";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Create");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Retorna la vista que muestra los detalles del pedido que se va a anular
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
            public IActionResult Delete(int? id)
        {
            if (HttpContext.Session.GetString("Rol") != null)
            {
                try
                {
                    PedidoListarDto dto = _getPedido.GetById(id.GetValueOrDefault());
                    if (dto != null) return View(dto);
                    ViewBag.Mensaje = $"No existe un pedido con el id {id}";
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
                return RedirectToAction("Index", "Home");
            }

        }

        /// <summary>
        /// Llama al caso de uso que anula un pedido. (No lo borra, es una baja lógica se podría decir)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, PedidoListarDto dto)
        {
            try
            {
                ViewBag.Mensaje = $"Pedido {dto.Id} eliminado correctamente";
                _anularPedido.Ejecutar(id.GetValueOrDefault());
                return RedirectToAction("Index", "Pedido");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Ha ocurrido un error al eliminar el pedido: {dto.Id}: " + ex.Message;
                return View();
            }
        }
    }
}
