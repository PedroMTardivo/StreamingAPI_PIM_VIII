using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsuariosController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario input)
        {
            if (string.IsNullOrWhiteSpace(input.Nome)) return BadRequest("Nome obrigatorio.");
            if (string.IsNullOrWhiteSpace(input.Email)) return BadRequest("Email obrigatorio.");
            var usuario = new Usuario { Nome = input.Nome, Email = input.Email };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
    }
}

