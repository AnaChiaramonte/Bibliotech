using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bibliotech.Data;
using Bibliotech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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


        // [HttpPost("login")]
        // Criar um metodo de login usando o login do Identity
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        //{
        //    if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
        //    {
        //        return BadRequest("Email e senha são obrigatórios.");
        //    }
        //    var user = await _context.Users.Where(u => u.Email == loginModel.Email).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return Unauthorized("Usuário não encontrado.");
        //    }
        //    var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
        //    if (!result)
        //    {
        //        return Unauthorized("Senha incorreta.");
        //    }
        //    // Gerar o Token e retornar para o usuarios
        //    var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, "Login");
        //    if (token == null)
        //    {
        //        return Unauthorized("Erro ao gerar o token de autenticação.");
        //    }
        //    Response.Cookies.Append("AuthToken", token, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //        SameSite = SameSiteMode.Strict,
        //        Expires = DateTimeOffset.UtcNow.AddHours(1)
        //    });

        //    // Retornar o usuário autenticado
        //    return Ok(new
        //    {
        //        Token = token
        //    });
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
                return BadRequest("Email e senha são obrigatórios.");

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result)
                return Unauthorized("Senha incorreta.");

            // Claims do Identity
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim("AspNet.Identity.SecurityStamp", user.SecurityStamp ?? "")
    };

            // Claims customizadas do usuário
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Configurações do JWT
            var jwtKey = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Key"];
            var jwtIssuer = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Issuer"];
            var jwtAudience = HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var expires = DateTime.UtcNow.AddHours(1);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                ExpiraEm = expires
            });
        }



        //[HttpPost("registrar")]
        // Criar um metodo de registro de Identity recenbendo o email e senha
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null || string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.Password))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }
            var user = new IdentityUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // Adicionar o usuário ao papel de Leitor
            await _userManager.AddToRoleAsync(user, "Leitor");
            return Ok(user);
        }



    }
}
