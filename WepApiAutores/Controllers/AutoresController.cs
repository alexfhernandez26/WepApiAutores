using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepApiAutores.Entidades;

namespace WepApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext context)
        {
            this._context = context;
        }
      

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get() 
        {
           var dataAutores =await _context.Autors.ToListAsync();

            return Ok(dataAutores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            if (autor == null)
            {
                return BadRequest("Error al guardar");
            }
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
