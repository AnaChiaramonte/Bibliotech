using Bibliotech.Data;
using Bibliotech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotechDBContext _context;

        public LivrosController(BibliotechDBContext context)
        {
            _context = context;
        }

        // GET: api/Livros
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livros>>> GetLivros()
        {
            return await _context.livros.ToListAsync();
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livros>> GetLivro(Guid id)
        {
            var livro = await _context.livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(Guid id, Livros livro)
        {
            if (id != livro.LivrosId)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
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

        // POST: api/Livros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Livros>> PostLivro(Livros livro)
        {
            _context.livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivro", new { id = livro.LivrosId }, livro);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(Guid id)
        {
            var livro = await _context.livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.livros.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LivroExists(Guid id)
        {
            return _context.livros.Any(e => e.LivrosId == id);
        }

        // GET: api/Livros/searchByTitle
        [AllowAnonymous]
        [HttpGet("searchByTitle")]
        public async Task<ActionResult<IEnumerable<Livros>>> SearchLivrosByTitle(string title)
        {
            var livros = await _context.livros
                .Where(l => l.Titulo.Contains(title))
                .ToListAsync();

            if (livros == null || livros.Count == 0)
            {
                return NotFound();
            }

            return livros;
        }

        // GET: api/Livros/searchByCategory
        [AllowAnonymous]
        [HttpGet("searchByCategory")]
        public async Task<ActionResult<IEnumerable<Livros>>> SearchLivrosByCategory(string category)
        {
            var livros = await _context.livros
                .Include(l => l.Categoria)
                .Where(l => l.Categoria.Nome.Contains(category) || l.Categoria.Genero.Contains(category))
                .ToListAsync();

            if (livros == null || livros.Count == 0)
            {
                return NotFound();
            }

            return livros;
        }
    }
}
