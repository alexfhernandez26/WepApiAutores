using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context)
        {

            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Libros>>> Get()
        {
            var dataLibro =  await _context.Libros.Include(x => x.Autor).ToListAsync();

            if (dataLibro == null)
            {
                return BadRequest("No existe ningun libro");
            }

            return Ok(dataLibro);
        }
        //[HttpGet("id:int")]
        //public async Task<ActionResult<Libros>> Get(int id)
        //{
        //    var dataLibro = await _context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);

        //    if (dataLibro == null)
        //    {
        //        return BadRequest($"No existe ningun libro con el id {id}");
        //    }

        //    return Ok(dataLibro);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Post(Libros libros)
        //{
        //    var existeAutor = await _context.Autors.AnyAsync(x => x.Id == libros.autorId );
        //    if (!existeAutor)
        //    {
        //        return NotFound($"No existe autor con el id {libros.autorId}");
        //    }
        //    var dataLibro = await _context.Libros.ToListAsync();

        //    if (libros == null)
        //    {
        //        return BadRequest("Debe insertar el libro");
        //    }

        //    _context.Libros.Add(libros);
        //    await _context.SaveChangesAsync();

        //    return Ok(dataLibro);
        //}
    }
}
