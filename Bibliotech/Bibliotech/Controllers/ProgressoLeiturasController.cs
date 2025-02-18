using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliotech.Data;
using Bibliotech.Models;

namespace Bibliotech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressoLeiturasController : ControllerBase
    {
        private readonly BibliotechDBContext _context;

        public ProgressoLeiturasController(BibliotechDBContext context)
        {
            _context = context;
        }

        // GET: api/ProgressoLeituras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgressoLeitura>>> Getprogressos()
        {
            return await _context.progressos.ToListAsync();
        }

        // GET: api/ProgressoLeituras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgressoLeitura>> GetProgressoLeitura(Guid id)
        {
            var progressoLeitura = await _context.progressos.FindAsync(id);

            if (progressoLeitura == null)
            {
                return NotFound();
            }

            return progressoLeitura;
        }

        // GET: api/ProgressoLeituras/user/{userId}/date/{date}
        [HttpGet("user/{userId}/date/{date}")]
        public async Task<ActionResult<object>> GetReadingProgress(Guid userId, DateTime date)
        {
            var progressos = await _context.progressos
                .Where(p => p.usuarioId == userId && p.DataAtualização.Date == date.Date)
                .OrderBy(p => p.DataAtualização)
                .ToListAsync();

            if (progressos.Count < 2)
            {
                return NotFound("Not enough data to calculate progress.");
            }

            var paginasLidas = progressos.Last().paginaLidas - progressos.First().paginaLidas;
            var progresso = (double)progressos.Last().paginaLidas / progressos.Last().totalPaginas * 100;

            return new
            {
                PaginasLidas = paginasLidas,
                Progresso = progresso
            };
        }

        // PUT: api/ProgressoLeituras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgressoLeitura(Guid id, ProgressoLeitura progressoLeitura)
        {
            if (id != progressoLeitura.ProgressoLeituraId)
            {
                return BadRequest();
            }

            _context.Entry(progressoLeitura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressoLeituraExists(id))
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

        // POST: api/ProgressoLeituras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgressoLeitura>> PostProgressoLeitura(ProgressoLeitura progressoLeitura)
        {
            _context.progressos.Add(progressoLeitura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgressoLeitura", new { id = progressoLeitura.ProgressoLeituraId }, progressoLeitura);
        }

        // DELETE: api/ProgressoLeituras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgressoLeitura(Guid id)
        {
            var progressoLeitura = await _context.progressos.FindAsync(id);
            if (progressoLeitura == null)
            {
                return NotFound();
            }

            _context.progressos.Remove(progressoLeitura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgressoLeituraExists(Guid id)
        {
            return _context.progressos.Any(e => e.ProgressoLeituraId == id);
        }
    }
}
