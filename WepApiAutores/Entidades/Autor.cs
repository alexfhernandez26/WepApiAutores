﻿namespace WepApiAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<Libros> Libros { get; set; }
    }
}
