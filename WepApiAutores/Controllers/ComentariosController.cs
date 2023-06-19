using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepApiAutores.Dtos;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDto>>> Get(int libroId)
        {
            var comentarios = await _context.Comentarios.Where(x => x.librosId == libroId).ToListAsync();

            if (comentarios == null)
            {
                return NotFound("Este libro no tiene comentarios");
            }

            var comentarioDb = _mapper.Map<List<ComentarioDto>>(comentarios);

            return Ok(comentarioDb);

        }

        [HttpPost]
        public async Task<IActionResult> Post(int libroId, ComentarioCreacionDto comentarioCreacionDto)
        {
            var existeLibro = await _context.Libros.AnyAsync(libroDb => libroDb.Id == libroId);

            if (!existeLibro)
            {
                return NotFound("El libro al que le quiere hacer el comentario no existe");
            }

            var comentario = _mapper.Map<Comentario>(comentarioCreacionDto);
            comentario.librosId = libroId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();  
            return Ok(comentario);
        }
    }
}
