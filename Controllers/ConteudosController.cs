using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de conteúdos de mídia
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ConteudosController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ConteudosController(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Cria um novo conteúdo de mídia
        /// </summary>
        /// <param name="input">Dados do conteúdo a ser criado</param>
        /// <returns>O conteúdo criado com seu ID</returns>
        /// <response code="201">Conteúdo criado com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        /// <example>
        /// {
        ///   "titulo": "Podcast sobre Tecnologia",
        ///   "varchar": "Podcast",
        ///   "criadorId": 1
        /// }
        /// </example>
        [HttpPost]
        [ProducesResponseType(typeof(Conteudo), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<Conteudo>> Create([FromBody] CriarConteudoRequest input)
        {
            if (string.IsNullOrWhiteSpace(input.Titulo)) 
                return BadRequest("Título é obrigatório.");
            if (string.IsNullOrWhiteSpace(input.Varchar)) 
                return BadRequest("Tipo de conteúdo é obrigatório.");
            
            var existsCriador = await _db.Criadores.AnyAsync(c => c.Id == input.CriadorId);
            if (!existsCriador) 
                return BadRequest("CriadorID inexistente.");

            var conteudo = new Conteudo 
            { 
                Titulo = input.Titulo, 
                Varchar = input.Varchar, 
                CriadorId = input.CriadorId 
            };
            _db.Conteudos.Add(conteudo);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = conteudo.Id }, conteudo);
        }

        /// <summary>
        /// Lista todos os conteúdos de mídia
        /// </summary>
        /// <returns>Lista de todos os conteúdos</returns>
        /// <response code="200">Lista de conteúdos retornada com sucesso</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Conteudo>), 200)]
        public async Task<ActionResult<IEnumerable<Conteudo>>> GetAll()
        {
            var list = await _db.Conteudos.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        /// <summary>
        /// Busca um conteúdo específico por ID
        /// </summary>
        /// <param name="id">ID do conteúdo</param>
        /// <returns>Dados do conteúdo</returns>
        /// <response code="200">Conteúdo encontrado</response>
        /// <response code="404">Conteúdo não encontrado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Conteudo), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conteudo>> GetById(int id)
        {
            var conteudo = await _db.Conteudos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (conteudo == null) return NotFound();
            return Ok(conteudo);
        }

        /// <summary>
        /// Atualiza um conteúdo existente
        /// </summary>
        /// <param name="id">ID do conteúdo a ser atualizado</param>
        /// <param name="input">Novos dados do conteúdo</param>
        /// <returns>Conteúdo atualizado</returns>
        /// <response code="200">Conteúdo atualizado com sucesso</response>
        /// <response code="400">Dados inválidos fornecidos</response>
        /// <response code="404">Conteúdo não encontrado</response>
        /// <example>
        /// {
        ///   "titulo": "Título Atualizado",
        ///   "varchar": "Novo Tipo"
        /// }
        /// </example>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Conteudo), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Conteudo>> Update(int id, [FromBody] AtualizarConteudoRequest input)
        {
            var conteudo = await _db.Conteudos.FindAsync(id);
            if (conteudo == null) return NotFound();
            
            if (!string.IsNullOrWhiteSpace(input.Titulo)) 
                conteudo.Titulo = input.Titulo;
            if (!string.IsNullOrWhiteSpace(input.Varchar)) 
                conteudo.Varchar = input.Varchar;
            
            await _db.SaveChangesAsync();
            return Ok(conteudo);
        }

        /// <summary>
        /// Exclui um conteúdo específico
        /// </summary>
        /// <param name="id">ID do conteúdo a ser excluído</param>
        /// <returns>Resultado da operação</returns>
        /// <response code="204">Conteúdo excluído com sucesso</response>
        /// <response code="404">Conteúdo não encontrado</response>
        /// <remarks>
        /// ⚠️ **ATENÇÃO**: Esta operação exclui o conteúdo permanentemente.
        /// Se houver arquivos associados, eles também serão removidos.
        /// </remarks>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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