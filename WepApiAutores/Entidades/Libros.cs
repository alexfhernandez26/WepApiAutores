namespace WepApiAutores.Entidades
{
    public class Libros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int autorId { get; set; }
        public Autor Autor { get; set; }

    }
}
