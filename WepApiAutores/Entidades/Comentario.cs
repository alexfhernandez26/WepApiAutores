namespace WepApiAutores.Entidades
{
    public class Comentario
    {
        public int ID { get; set; }
        public int Contenido { get; set; }
        public int libroId { get; set; }
        public Libros Libros { get; set; }

    }
}
