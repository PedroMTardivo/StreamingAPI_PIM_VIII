namespace StreamingApi.Api.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public List<PlaylistItem> Itens { get; set; } = new();
    }
}

