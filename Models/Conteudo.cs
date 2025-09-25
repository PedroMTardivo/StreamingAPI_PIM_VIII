namespace StreamingApi.Api.Models
{
    public class Conteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Varchar { get; set; } = string.Empty; // tipo de conteúdo
        public string? ArquivoPath { get; set; } // caminho do arquivo de mídia
        public string? ContentType { get; set; } // tipo MIME (video/mp4, audio/mp3, etc.)

        public int CriadorId { get; set; }
        public Criador? Criador { get; set; }

        public List<PlaylistItem> PlaylistItems { get; set; } = new();
    }
}

