using System.Text.Json.Serialization;

namespace WepApiAutores.Entidades
{
    public class Libros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<AutoresLibros> autorLibro { get; set; }
    }
}
