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
    public class ResenhasController : ControllerBase
    {
        private readonly BibliotechDBContext _context;

        public ResenhasController(BibliotechDBContext context)
        {
            _context = context;
        }

        // GET: api/Resenhas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resenha>>> Getresenhas()
        {
            return await _context.resenhas.ToListAsync();
        }

        // GET: api/Resenhas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resenha>> GetResenha(Guid id)
        {
            var resenha = await _context.resenhas.FindAsync(id);

            if (resenha == null)
            {
                return NotFound();
            }

            return resenha;
        }

        // PUT: api/Resenhas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResenha(Guid id, Resenha resenha)
        {
            if (id != resenha.ResenhaId)
            {
                return BadRequest();
            }

            _context.Entry(resenha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResenhaExists(id))
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

        // POST: api/Resenhas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Resenha>> PostResenha(Resenha resenha)
        {
            _context.resenhas.Add(resenha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResenha", new { id = resenha.ResenhaId }, resenha);
        }

        // DELETE: api/Resenhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResenha(Guid id)
        {
            var resenha = await _context.resenhas.FindAsync(id);
            if (resenha == null)
            {
                return NotFound();
            }

            _context.resenhas.Remove(resenha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResenhaExists(Guid id)
        {
            return _context.resenhas.Any(e => e.ResenhaId == id);
        }

        // POST: api/Resenhas/review
        [HttpPost("review")]
        public async Task<ActionResult<Resenha>> PostReview(Resenha resenha)
        {
            resenha.DataResenha = DateOnly.FromDateTime(DateTime.Now);
            _context.resenhas.Add(resenha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResenha", new { id = resenha.ResenhaId }, resenha);
        }
    }
}
