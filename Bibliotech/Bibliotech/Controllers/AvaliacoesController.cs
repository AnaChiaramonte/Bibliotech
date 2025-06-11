using Biblioteca.Models;
using Bibliotech.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly BibliotechDBContext _context;

        public AvaliacoesController(BibliotechDBContext context)
        {
            _context = context;
        }

        // GET: api/Avaliacoes
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> Getavaliacoes()
        {
            return await _context.avaliacoes.ToListAsync();
        }

        // GET: api/Avaliacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> GetAvaliacao(int id)
        {
            var avaliacao = await _context.avaliacoes.FindAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return avaliacao;
        }

        // PUT: api/Avaliacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAvaliacao(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.AvaliacaoId)
            {
                return BadRequest();
            }

            _context.Entry(avaliacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvaliacaoExists(id))
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

        // POST: api/Avaliacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao avaliacao)
        {
            _context.avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvaliacao", new { id = avaliacao.AvaliacaoId }, avaliacao);
        }

        // DELETE: api/Avaliacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvaliacao(int id)
        {
            var avaliacao = await _context.avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            _context.avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.avaliacoes.Any(e => e.AvaliacaoId == id);
        }
    }
}
