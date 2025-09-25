namespace StreamingApi.Api.Models
{
    public class PlaylistItem
    {
        public int PlaylistId { get; set; }
        public Playlist? Playlist { get; set; }

        public int ConteudoId { get; set; }
        public Conteudo? Conteudo { get; set; }
    }
}

