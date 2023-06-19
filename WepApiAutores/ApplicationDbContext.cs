using Microsoft.EntityFrameworkCore;
using WepApiAutores.Entidades;

namespace WepApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutoresLibros>()
                .HasKey(al => new { al.AutorId, al.LibrosId });

            modelBuilder.Entity<AutoresLibros>()
                .HasOne(al => al.Autor)
                .WithMany(al => al.autorLibro)
                .HasForeignKey(al => al.AutorId);

            modelBuilder.Entity<AutoresLibros>()
                .HasOne(al => al.Libros)
                .WithMany(al => al.autorLibro)
                .HasForeignKey(al => al.LibrosId);
        }
        public DbSet<Autor> Autors { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public DbSet<AutoresLibros> AutoresLibros { get; set; }
    }
}
