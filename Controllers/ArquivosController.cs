using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("upload/{conteudoId:int}")]
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

        [HttpGet("download/{fileName}")]
        public IActionResult DownloadArquivo(string fileName)
        {
            var filePath = Path.Combine(_uploadsPath, fileName);
            
            if (!System.IO.File.Exists(filePath))
                return NotFound("Arquivo não encontrado.");

            var conteudo = _db.Conteudos.FirstOrDefault(c => c.ArquivoPath == fileName);
            var contentType = conteudo?.ContentType ?? "application/octet-stream";

            return PhysicalFile(filePath, contentType, fileName);
        }

        [HttpDelete("remove/{conteudoId:int}")]
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