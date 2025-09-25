namespace StreamingApi.Api.Models
{
    public class Criador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public List<Conteudo> Conteudos { get; set; } = new();
    }
}

