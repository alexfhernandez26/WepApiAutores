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
        public async Task<ActionResult<LibroDto>> Get(int id)
        {
            var dataLibro = await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (dataLibro == null)
            {
                return BadRequest($"No existe ningun libro con el id {id}");
            }

            return _mapper.Map<LibroDto>(dataLibro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDto librosCreacionDto)
        {
            //var existeAutor = await _context.Autors.AnyAsync(x => x.Id == libros.AutorId);
            //if (!existeAutor)
            //{
            //    return NotFound($"No existe autor con el id {libros.AutorId}");
            //}

            var libros = _mapper.Map<Libros>(librosCreacionDto);

            _context.Add(libros);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
