using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConteudosController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ConteudosController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<Conteudo>> Create([FromBody] Conteudo input)
        {
            if (string.IsNullOrWhiteSpace(input.Titulo)) return BadRequest("Titulo obrigatorio.");
            if (string.IsNullOrWhiteSpace(input.Varchar)) return BadRequest("Tipo de conteudo (varchar) obrigatorio.");
            var existsCriador = await _db.Criadores.AnyAsync(c => c.Id == input.CriadorId);
            if (!existsCriador) return BadRequest("CriadorID inexistente.");

            var conteudo = new Conteudo { Titulo = input.Titulo, Varchar = input.Varchar, CriadorId = input.CriadorId };
            _db.Conteudos.Add(conteudo);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = conteudo.Id }, conteudo);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conteudo>>> GetAll()
        {
            var list = await _db.Conteudos.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Conteudo>> GetById(int id)
        {
            var conteudo = await _db.Conteudos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (conteudo == null) return NotFound();
            return Ok(conteudo);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Conteudo>> Update(int id, [FromBody] Conteudo input)
        {
            var conteudo = await _db.Conteudos.FindAsync(id);
            if (conteudo == null) return NotFound();
            if (!string.IsNullOrWhiteSpace(input.Titulo)) conteudo.Titulo = input.Titulo;
            if (!string.IsNullOrWhiteSpace(input.Varchar)) conteudo.Varchar = input.Varchar;
            if (input.CriadorId != 0)
            {
                var existsCriador = await _db.Criadores.AnyAsync(c => c.Id == input.CriadorId);
                if (!existsCriador) return BadRequest("CriadorID inexistente.");
                conteudo.CriadorId = input.CriadorId;
            }
            await _db.SaveChangesAsync();
            return Ok(conteudo);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var conteudo = await _db.Conteudos.FindAsync(id);
            if (conteudo == null) return NotFound();
            _db.Conteudos.Remove(conteudo);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

