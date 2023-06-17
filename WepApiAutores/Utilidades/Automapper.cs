using WepApiAutores.Dtos;
using WepApiAutores.Entidades;
using AutoMapper;
namespace WepApiAutores.Utilidades
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Autor, AutorCreacionDto>().ReverseMap();

            CreateMap<Autor, AutorDto>().ReverseMap();

            CreateMap<Libros, LibroCreacionDto>().ReverseMap();

            CreateMap<Libros, LibroDto>().ReverseMap();
        }
    }
}
