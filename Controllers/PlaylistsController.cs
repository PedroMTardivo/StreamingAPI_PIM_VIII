using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PlaylistsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<Playlist>> Create([FromBody] Playlist input)
        {
            if (string.IsNullOrWhiteSpace(input.Nome)) return BadRequest("Nome obrigatorio.");
            if (input.UsuarioId == 0) return BadRequest("UsuarioID obrigatorio.");
            var existsUser = await _db.Usuarios.AnyAsync(u => u.Id == input.UsuarioId);
            if (!existsUser) return BadRequest("UsuarioID inexistente.");
            var playlist = new Playlist { Nome = input.Nome, UsuarioId = input.UsuarioId };
            _db.Playlists.Add(playlist);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = playlist.Id }, playlist);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetAll()
        {
            var list = await _db.Playlists.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Playlist>> GetById(int id)
        {
            var playlist = await _db.Playlists
                .Include(p => p.Itens)
                .ThenInclude(i => i.Conteudo)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (playlist == null) return NotFound();
            return Ok(playlist);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Playlist>> Update(int id, [FromBody] Playlist input)
        {
            var playlist = await _db.Playlists.FindAsync(id);
            if (playlist == null) return NotFound();
            if (!string.IsNullOrWhiteSpace(input.Nome)) playlist.Nome = input.Nome;
            await _db.SaveChangesAsync();
            return Ok(playlist);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var playlist = await _db.Playlists.FindAsync(id);
            if (playlist == null) return NotFound();
            _db.Playlists.Remove(playlist);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{playlistId:int}/itens")]
        public async Task<ActionResult<Playlist>> AddItem(int playlistId, [FromBody] PlaylistItem input)
        {
            var existsPlaylist = await _db.Playlists.AnyAsync(p => p.Id == playlistId);
            if (!existsPlaylist) return NotFound("Playlist nao encontrada.");
            var existsConteudo = await _db.Conteudos.AnyAsync(c => c.Id == input.ConteudoId);
            if (!existsConteudo) return BadRequest("ConteudoID inexistente.");

            var already = await _db.PlaylistItens.AnyAsync(pi => pi.PlaylistId == playlistId && pi.ConteudoId == input.ConteudoId);
            if (already) return Conflict("Conteudo ja presente na playlist.");

            var item = new PlaylistItem { PlaylistId = playlistId, ConteudoId = input.ConteudoId };
            _db.PlaylistItens.Add(item);
            await _db.SaveChangesAsync();
            return await GetById(playlistId);
        }

        [HttpDelete("{playlistId:int}/itens/{conteudoId:int}")]
        public async Task<IActionResult> RemoveItem(int playlistId, int conteudoId)
        {
            var item = await _db.PlaylistItens.FindAsync(playlistId, conteudoId);
            if (item == null) return NotFound();
            _db.PlaylistItens.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

