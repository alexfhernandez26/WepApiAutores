using System.Text.Json.Serialization;

namespace WepApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
       // [JsonIgnore]
        public List<Libros> Libros { get; set; }
    }
}
