namespace WepApiAutores.Entidades
{
    public class Comentario
    {
        public int ID { get; set; }
        public string Contenido { get; set; }
        public int librosId { get; set; }
        public Libros Libros { get; set; }

    }
}
