using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramacionWeb_1057719_Project.Models;

namespace ProgramacionWeb_1057719_Project.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly DbBolsassiguereContext _dbbolsassiguere = new DbBolsassiguereContext();
        public async Task<IActionResult> Index()
        {
            IEnumerable<Models.Cotizacion> cotizaciones = Functions.APIServiceCotizaciones.GetCotizaciones().Result;
            return View(cotizaciones);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cotizaciones = Functions.APIServiceCotizaciones.GetCotizacion(id).Result;

            if (cotizaciones == null)
            {
                return NotFound();
            }

            return View(cotizaciones);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string correoCliente, string telefonoCliente, DateTime FechaCotizacion)
        {
            Models.Cotizacion cotizacion = new Models.Cotizacion
            {
                CorreoCliente = correoCliente,
                TelefonoCliente = telefonoCliente,
                FechaCotizacion = FechaCotizacion,
            };
            var result = Functions.APIServiceCotizaciones.PostCotizacion(cotizacion);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cotizaciones = Functions.APIServiceCotizaciones.GetCotizacion(id).Result;
            if (cotizaciones == null)
            {
                return NotFound();
            }
            return View(cotizaciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string correoCliente, string telefonoCliente, DateTime FechaCotizacion)
        {
            Models.Cotizacion cotizacion = new Models.Cotizacion
            {
                Id = id,
                CorreoCliente = correoCliente,
                TelefonoCliente = telefonoCliente,
                FechaCotizacion = FechaCotizacion,
            };
            Functions.APIServiceCotizaciones.PutCotizacion(cotizacion, id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cotizaciones = Functions.APIServiceCotizaciones.GetCotizacion(id).Result;
            if (cotizaciones == null)
            {
                return NotFound();
            }
            return View(cotizaciones);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Functions.APIServiceCotizaciones.DeleteCotizacion(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
