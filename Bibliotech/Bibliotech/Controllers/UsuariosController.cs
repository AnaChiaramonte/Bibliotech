using System.Security.Claims;
using Bibliotech.Data;
using Bibliotech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BibliotechDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosController(BibliotechDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Getusuarios()
        {
            return await _context.usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Obter o ID do usuário de forma mais segura
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado");
            }

            usuario.UserId = Guid.Parse(userId);
            usuario.Email = user.Email;
            usuario.Senha = user.PasswordHash;

            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.usuarios.Any(e => e.UsuarioId == id);
        }

        [HttpGet("role/{email}")]
        public async Task<ActionResult<string>> GetUserRoleByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                return Ok("Leitor");
            }
            return Ok(roles.FirstOrDefault());
        }


        // Criar um endpoint que busca o usuário a partir do e-mail
        [HttpGet("byEmail/{email}")]
        public async Task<ActionResult<Usuario>> GetUsuarioByEmail(string email)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado");
            }
            return Ok(usuario);
        }

        // Criar um endpoint que coloca o usuário como administrador
        [HttpPost("makeAdmin/{email}")]
        public ActionResult MakeUserAdmin(string email)
        {
            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            var result = _userManager.AddToRoleAsync(user, "Admin").Result;
            if (!result.Succeeded)
            {
                return BadRequest("Erro ao adicionar o usuário ao papel de administrador");
            }
            return Ok("Usuário adicionado ao papel de administrador com sucesso");
        }
    }
}
