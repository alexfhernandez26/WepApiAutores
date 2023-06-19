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

            CreateMap<Libros,LibroCreacionDto>().ReverseMap()
                .ForMember(libro => libro.autorLibro, options => options.MapFrom(MapAutoresIdsLibros));
            CreateMap<LibroDto, Libros>().ReverseMap()
                .ForMember(libroDto => libroDto.Autores, option => option.MapFrom(MapLibroDtoAutores));
                
          
            CreateMap<Comentario, ComentarioCreacionDto>().ReverseMap();
            CreateMap<Comentario, ComentarioDto>().ReverseMap();
            
        }

        private List<AutorDto> MapLibroDtoAutores(Libros libros , LibroDto libroDto)
        {
            var respuesta = new List<AutorDto>();

            if (respuesta == null) { return respuesta; }

            foreach (var autor in libros.autorLibro)
            {
                respuesta.Add(new AutorDto()
                {
                    Id = autor.AutorId,
                    Nombre = autor.Autor.Nombre
                }
                ) ;
            }

            return respuesta;
        }

        private List<AutoresLibros> MapAutoresIdsLibros(LibroCreacionDto libroCreacionDto, Libros libros)
        {
            var respuesta = new List<AutoresLibros>();

            if (libroCreacionDto.AutorId == null) { return respuesta; }

            foreach (var autorid in libroCreacionDto.AutorId)
            {
                respuesta.Add(new AutoresLibros { AutorId = autorid });
            }

            return respuesta;
        }
    }
}
