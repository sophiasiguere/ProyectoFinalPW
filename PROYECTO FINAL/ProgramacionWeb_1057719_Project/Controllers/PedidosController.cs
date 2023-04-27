using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramacionWeb_1057719_Project.Models;

namespace ProgramacionWeb_1057719_Project.Controllers
{
    public class PedidosController : Controller
    {
        private readonly DbBolsassiguereContext _dbbolsassiguere = new DbBolsassiguereContext();

        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Pedido> pedidos = Functions.APIServicePedidos.GetPedidos().Result;
            IEnumerable<Models.Cotizacion> cotizaciones = Functions.APIServiceCotizaciones.GetCotizaciones().Result;
            var result = from p in pedidos
                         join c in cotizaciones on p.NoPedido equals c.Id
                         select new BolsasSiguereModel.PedidoModel
                         {
                             Id= p.Id,
                             NoPedido= p.NoPedido,
                             TamañoHorizonte = p.TamañoHorizonte,
                             TamañoExtension = p.TamañoExtension,
                             TamañoAltura = p.TamañoAltura,
                             Calibre = p.Calibre,
                             ColorPantone = p.ColorPantone,
                             Material = p.Material,
                             Cliente = c.CorreoCliente,
                             Logo = p.Logo,
                         };
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pedidos = Functions.APIServicePedidos.GetPedido(id).Result;
            if (pedidos == null)
            {
                return NotFound();
            }

            return View(pedidos);
        }

        public ActionResult Create()
        {
            IEnumerable<Models.Cotizacion> cotizaciones = Functions.APIServiceCotizaciones.GetCotizaciones().Result;
            List<SelectListItem> Variable = cotizaciones.Select(info => new SelectListItem
            {
                Value = info.Id.ToString(),
                Text = info.Id.ToString()
            }).ToList();
            ViewBag.NoPedido = Variable;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int TamañoHorizonte, int TamañoExtension, int TamañoAltura, string Calibre, string ColorPantone, string Material, int NoPedido, string Logo)
        {
            Models.Pedido pedidos = new Models.Pedido
            {
                TamañoHorizonte = TamañoHorizonte,
                TamañoExtension = TamañoExtension,
                TamañoAltura = TamañoAltura,
                Calibre = Calibre,
                ColorPantone = ColorPantone,
                Material = Material,
                NoPedido = NoPedido,
                Logo = Logo
            };
            var pedido = Functions.APIServicePedidos.PostPedido(pedidos);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _dbbolsassiguere.Pedidos == null)
            {
                return NotFound();
            }
            var p = Functions.APIServicePedidos.GetPedido(id).Result;

            Models.Pedido pedido = new Models.Pedido
            {
                TamañoHorizonte = p.TamañoHorizonte,
                TamañoExtension = p.TamañoExtension,
                TamañoAltura = p.TamañoAltura,
                Calibre = p.Calibre,
                ColorPantone = p.ColorPantone,
                Material = p.Material,
                NoPedido = p.NoPedido,
                Logo = p.Logo
            };

            IEnumerable<Models.Cotizacion> cotizaciones = Functions.APIServiceCotizaciones.GetCotizaciones().Result;
            List<SelectListItem> Variable = cotizaciones.Select(info => new SelectListItem
            {
                Value = info.Id.ToString(),
                Text = info.Id.ToString()
            }).ToList();
            ViewBag.NoPedido = Variable;


            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int TamañoHorizonte, int TamañoExtension, int TamañoAltura, string calibre, string colorPantone, string material, int NoPedido, string Logo)
        {
            Models.Pedido pedidos = new Models.Pedido
            {
                Id= id,
                TamañoHorizonte = TamañoHorizonte,
                TamañoExtension = TamañoExtension,
                TamañoAltura = TamañoAltura,
                Calibre = calibre,
                ColorPantone = colorPantone,
                Material = material,
                NoPedido = NoPedido,
                Logo = Logo
            };
            var result = Functions.APIServicePedidos.PutPedido(pedidos,id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pedido = Functions.APIServicePedidos.GetPedido(id).Result;

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Functions.APIServicePedidos.DeletePedido(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
