using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WepApiAutores.Entidades;
using AutoMapper;
using WepApiAutores.Dtos;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> Get() 
        {
           var dataAutores =await _context.Autors.ToListAsync();

            return _mapper.Map<List<AutorDto>>(dataAutores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDto>> Get(int id)
        {
            var dataAutores = await _context.Autors.FirstOrDefaultAsync(autorBd => autorBd.Id == id);

            return _mapper.Map<AutorDto>(dataAutores);
        }

        [HttpGet("name")]
        public async Task<ActionResult<List<AutorDto>>> Get(string name)
        {
            var dataAutores = await _context.Autors.Where(autorBd => autorBd.Nombre.Contains(name)).ToListAsync();

            return _mapper.Map<List<AutorDto>>(dataAutores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(AutorCreacionDto autorCreacionDto)
        {
            if (autorCreacionDto == null)
            {
                return BadRequest("Error al guardar");
            }

            var autor = _mapper.Map<Autor>(autorCreacionDto);
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor,int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("el id no coincide");
            }
             _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok(autor);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Autors.AnyAsync(x => x.Id == id);

            if(!existe)
            {
                return NotFound("No se encuentra id");
            }
            _context.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
