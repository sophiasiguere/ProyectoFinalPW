using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2_Web_SophiaSiguere.Models;

namespace Proyecto2_Web_SophiaSiguere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionsController : ControllerBase
    {
        private readonly DbBolsassiguereContext _context;

        public CotizacionsController(DbBolsassiguereContext context)
        {
            _context = context;
        }

        // GET: api/Cotizacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotizacion>>> GetCotizacions()
        {
          if (_context.Cotizacions == null)
          {
              return NotFound();
          }
            return await _context.Cotizacions.ToListAsync();
        }

        // GET: api/Cotizacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cotizacion>> GetCotizacion(int id)
        {
          if (_context.Cotizacions == null)
          {
              return NotFound();
          }
            var cotizacion = await _context.Cotizacions.FindAsync(id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            return cotizacion;
        }

        // PUT: api/Cotizacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizacion(int id, Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(cotizacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotizacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cotizacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cotizacion>> PostCotizacion(Cotizacion cotizacion)
        {
          if (_context.Cotizacions == null)
          {
              return Problem("Entity set 'DbBolsassiguereContext.Cotizacions'  is null.");
          }
            _context.Cotizacions.Add(cotizacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCotizacion", new { id = cotizacion.Id }, cotizacion);
        }

        // DELETE: api/Cotizacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizacion(int id)
        {
            if (_context.Cotizacions == null)
            {
                return NotFound();
            }
            var cotizacion = await _context.Cotizacions.FindAsync(id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            _context.Cotizacions.Remove(cotizacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CotizacionExists(int id)
        {
            return (_context.Cotizacions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
