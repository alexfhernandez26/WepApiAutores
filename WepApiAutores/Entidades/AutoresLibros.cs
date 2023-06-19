namespace WepApiAutores.Entidades
{
    public class AutoresLibros
    {
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public int LibrosId { get; set; }
        public Libros Libros { get; set; }

    }
}
