﻿namespace WepApiAutores.Dtos
{
    public class LibroDtoConAutores : LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public List<AutorDto> Autores { get; set; }
}
}
