namespace WepApiAutores.Dtos
{
    public class LibroCreacionDto
    {
        public string Titulo{ get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public List<int> AutorId { get; set; }
    }
}
