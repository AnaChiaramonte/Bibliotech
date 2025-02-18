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
