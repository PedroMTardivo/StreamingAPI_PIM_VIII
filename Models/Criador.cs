using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StreamingApi.Api.Models
{
    /// <summary>
    /// Representa um criador de conteúdo no sistema de streaming
    /// </summary>
    public class Criador
    {
        /// <summary>
        /// Identificador único do criador
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do criador de conteúdo
        /// </summary>
        /// <example>João Silva</example>
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Lista de conteúdos criados por este criador
        /// </summary>
        public List<Conteudo> Conteudos { get; set; } = new();
    }

    /// <summary>
    /// DTO para criação de um novo criador
    /// </summary>
    public class CriarCriadorRequest
    {
        /// <summary>
        /// Nome do criador
        /// </summary>
        /// <example>Maria Santos</example>
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
    }
}

