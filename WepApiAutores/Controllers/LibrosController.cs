using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WepApiAutores.Dtos;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LibrosController(ApplicationDbContext context, IMapper mapper)
        {

            this._context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<List<LibroDto>>> Get()
        {
            var dataLibro =  await _context.Libros.ToListAsync();

            if (dataLibro == null)
            {
                return BadRequest("No existe ningun libro");
            }

            return _mapper.Map<List<LibroDto>>(dataLibro);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDtoConAutores>> Get(int id)
        {
            var dataLibro = await _context.Libros.Include(librobd => librobd.autorLibro).
                ThenInclude(autoresLibrosDb => autoresLibrosDb.Autor).FirstOrDefaultAsync(x => x.Id == id);

            if (dataLibro == null)
            {
                return BadRequest($"No existe ningun libro con el id {id}");
            }

            return _mapper.Map<LibroDtoConAutores>(dataLibro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDto librosCreacionDto)
        {
            if (librosCreacionDto.AutorId == null)
            {
                return BadRequest("No se puede insertar libro sin autor");
            }
            var AutoresIds = await _context.Autors.Where
                (autorBd => librosCreacionDto.AutorId.Contains(autorBd.Id)).Select(x => x.Id).ToListAsync();

            if(AutoresIds.Count != librosCreacionDto.AutorId.Count)
            {
                return BadRequest("uno de los ids del autor insertado no existe");
            }

            var libros = _mapper.Map<Libros>(librosCreacionDto);

            _context.Add(libros);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
