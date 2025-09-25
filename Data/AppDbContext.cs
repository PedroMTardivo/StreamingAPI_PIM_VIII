using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Models;

namespace StreamingApi.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Criador> Criadores => Set<Criador>();
        public DbSet<Conteudo> Conteudos => Set<Conteudo>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Playlist> Playlists => Set<Playlist>();
        public DbSet<PlaylistItem> PlaylistItens => Set<PlaylistItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlaylistItem>()
                .HasKey(pi => new { pi.PlaylistId, pi.ConteudoId });

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Playlist)
                .WithMany(p => p.Itens)
                .HasForeignKey(pi => pi.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Conteudo)
                .WithMany(c => c.PlaylistItems)
                .HasForeignKey(pi => pi.ConteudoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conteudo>()
                .HasOne(c => c.Criador)
                .WithMany(cr => cr.Conteudos)
                .HasForeignKey(c => c.CriadorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

