using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDto libroCreacionDto)
        {
            var libroDB = await _context.Libros
                .Include(a => a.autorLibro).FirstOrDefaultAsync(x=> x.Id == id);

            if (libroDB == null)
            {
                return BadRequest("No existe libro");
            }

            libroDB = _mapper.Map(libroCreacionDto, libroDB);

           await _context.SaveChangesAsync();
           return Ok(libroDB);

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Error con el formato del patch enviado");
            }

            var libroDb = await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);   
            if (libroDb == null)
            {
                return NotFound("No se encuentra libro con ese id");
            }

            var libroDto = _mapper.Map<LibroPatchDto>(libroDb);
            patchDocument.ApplyTo(libroDto, ModelState);

            var esValido = TryValidateModel(libroDto);

            if (!esValido)
            {
                return BadRequest("El modelo de datos no es valido");
            }

            _mapper.Map(libroDto, libroDb);

           await _context.SaveChangesAsync();

            return Ok(libroDb);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Libros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound("No se encuentra id");
            }
            _context.Remove(new Libros() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
