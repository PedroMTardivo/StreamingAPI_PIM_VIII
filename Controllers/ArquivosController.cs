using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de arquivos de mídia
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArquivosController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadsPath;

        public ArquivosController(AppDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
            _uploadsPath = Path.Combine(_environment.ContentRootPath, "uploads");
            
            if (!Directory.Exists(_uploadsPath))
            {
                Directory.CreateDirectory(_uploadsPath);
            }
        }

        /// <summary>
        /// Faz upload de um arquivo de mídia para um conteúdo específico
        /// </summary>
        /// <param name="conteudoId">ID do conteúdo que receberá o arquivo</param>
        /// <param name="arquivo">Arquivo de mídia (MP4, MP3, AVI, MOV)</param>
        /// <returns>Resultado do upload</returns>
        /// <response code="200">Arquivo enviado com sucesso</response>
        /// <response code="400">Arquivo inválido ou tipo não suportado</response>
        /// <response code="404">Conteúdo não encontrado</response>
        /// <remarks>
        /// **Tipos de arquivo suportados:**
        /// - MP4 (video/mp4)
        /// - MP3 (audio/mp3, audio/mpeg)
        /// - AVI (video/avi)
        /// - MOV (video/mov)
        /// 
        /// **Tamanho máximo:** 50MB
        /// </remarks>
        [HttpPost("upload/{conteudoId:int}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> UploadArquivo(int conteudoId, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            var conteudo = await _db.Conteudos.FindAsync(conteudoId);
            if (conteudo == null)
                return NotFound("Conteúdo não encontrado.");

            // Validar tipo de arquivo
            var allowedTypes = new[] { "video/mp4", "audio/mp3", "audio/mpeg", "video/avi", "video/mov" };
            if (!allowedTypes.Contains(arquivo.ContentType))
                return BadRequest("Tipo de arquivo não suportado. Use MP4, MP3, AVI ou MOV.");

            // Validar tamanho do arquivo (50MB)
            if (arquivo.Length > 50 * 1024 * 1024)
                return BadRequest("Arquivo muito grande. Tamanho máximo: 50MB.");

            // Gerar nome único para o arquivo
            var extension = Path.GetExtension(arquivo.FileName);
            var fileName = $"{conteudoId}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadsPath, fileName);

            // Salvar arquivo
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // Atualizar banco
            conteudo.ArquivoPath = fileName;
            conteudo.ContentType = arquivo.ContentType;
            await _db.SaveChangesAsync();

            return Ok(new { message = "Arquivo enviado com sucesso.", fileName });
        }

        /// <summary>
        /// Faz download de um arquivo de mídia
        /// </summary>
        /// <param name="fileName">Nome do arquivo para download</param>
        /// <returns>Arquivo de mídia</returns>
        /// <response code="200">Arquivo encontrado e enviado</response>
        /// <response code="404">Arquivo não encontrado</response>
        /// <remarks>
        /// Este endpoint serve arquivos de mídia diretamente.
        /// O arquivo será reproduzido no navegador ou baixado dependendo do tipo.
        /// </remarks>
        [HttpGet("download/{fileName}")]
        [ProducesResponseType(typeof(FileResult), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult DownloadArquivo(string fileName)
        {
            var filePath = Path.Combine(_uploadsPath, fileName);
            
            if (!System.IO.File.Exists(filePath))
                return NotFound("Arquivo não encontrado.");

            var conteudo = _db.Conteudos.FirstOrDefault(c => c.ArquivoPath == fileName);
            var contentType = conteudo?.ContentType ?? "application/octet-stream";

            return PhysicalFile(filePath, contentType, fileName);
        }

        /// <summary>
        /// Remove o arquivo associado a um conteúdo
        /// </summary>
        /// <param name="conteudoId">ID do conteúdo</param>
        /// <returns>Resultado da operação</returns>
        /// <response code="200">Arquivo removido com sucesso</response>
        /// <response code="404">Conteúdo não encontrado</response>
        /// <remarks>
        /// Esta operação remove tanto o arquivo físico quanto a referência no banco de dados.
        /// O conteúdo permanece, mas sem arquivo associado.
        /// </remarks>
        [HttpDelete("remove/{conteudoId:int}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> RemoverArquivo(int conteudoId)
        {
            var conteudo = await _db.Conteudos.FindAsync(conteudoId);
            if (conteudo == null)
                return NotFound("Conteúdo não encontrado.");

            if (!string.IsNullOrEmpty(conteudo.ArquivoPath))
            {
                var filePath = Path.Combine(_uploadsPath, conteudo.ArquivoPath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                conteudo.ArquivoPath = null;
                conteudo.ContentType = null;
                await _db.SaveChangesAsync();
            }

            return Ok(new { message = "Arquivo removido com sucesso." });
        }
    }
}