using System.ComponentModel.DataAnnotations;

namespace StreamingApi.Api.Models
{
    /// <summary>
    /// Representa um conteúdo de mídia no sistema de streaming
    /// </summary>
    public class Conteudo
    {
        /// <summary>
        /// Identificador único do conteúdo
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Título do conteúdo
        /// </summary>
        /// <example>Podcast sobre Tecnologia</example>
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Título deve ter entre 2 e 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Tipo/categoria do conteúdo
        /// </summary>
        /// <example>Podcast</example>
        [Required(ErrorMessage = "Tipo é obrigatório")]
        [StringLength(50, ErrorMessage = "Tipo deve ter no máximo 50 caracteres")]
        public string Varchar { get; set; } = string.Empty;

        /// <summary>
        /// Caminho do arquivo de mídia no servidor
        /// </summary>
        /// <example>uploads/abc123.mp3</example>
        public string? ArquivoPath { get; set; }

        /// <summary>
        /// Tipo MIME do arquivo
        /// </summary>
        /// <example>audio/mp3</example>
        public string? ContentType { get; set; }

        /// <summary>
        /// ID do criador responsável por este conteúdo
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "CriadorId é obrigatório")]
        public int CriadorId { get; set; }

        /// <summary>
        /// Criador responsável por este conteúdo
        /// </summary>
        public Criador? Criador { get; set; }

        /// <summary>
        /// Itens de playlist que contêm este conteúdo
        /// </summary>
        public List<PlaylistItem> PlaylistItems { get; set; } = new();
    }

    /// <summary>
    /// DTO para criação de um novo conteúdo
    /// </summary>
    public class CriarConteudoRequest
    {
        /// <summary>
        /// Título do conteúdo
        /// </summary>
        /// <example>Vídeo Tutorial de Programação</example>
        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Título deve ter entre 2 e 200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>
        /// Tipo/categoria do conteúdo
        /// </summary>
        /// <example>Tutorial</example>
        [Required(ErrorMessage = "Tipo é obrigatório")]
        [StringLength(50, ErrorMessage = "Tipo deve ter no máximo 50 caracteres")]
        public string Varchar { get; set; } = string.Empty;

        /// <summary>
        /// ID do criador responsável
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "CriadorId é obrigatório")]
        public int CriadorId { get; set; }
    }

    /// <summary>
    /// DTO para atualização de conteúdo
    /// </summary>
    public class AtualizarConteudoRequest
    {
        /// <summary>
        /// Título do conteúdo
        /// </summary>
        /// <example>Vídeo Tutorial Atualizado</example>
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Título deve ter entre 2 e 200 caracteres")]
        public string? Titulo { get; set; }

        /// <summary>
        /// Tipo/categoria do conteúdo
        /// </summary>
        /// <example>Tutorial Avançado</example>
        [StringLength(50, ErrorMessage = "Tipo deve ter no máximo 50 caracteres")]
        public string? Varchar { get; set; }
    }
}

