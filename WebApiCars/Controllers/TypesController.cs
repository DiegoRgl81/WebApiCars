using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCars.Entidades;

namespace WebApiCars.Controllers
{
    [ApiController]
    [Route("api/Tipos")]
    public class TypesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TypesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tipo>> GetById(int id)
        {
            return await dbContext.Tipos.FirstOrDefaultAsync(x => x.Id == id);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Tipo tipo)
        {
            var existeAutomovil = await dbContext.Tipos.AnyAsync(x => x.Id == tipo.CarId);

            if (!existeAutomovil)
            {
                return BadRequest($"No existe el automovil con el id: {tipo.CarId}");
            }

            dbContext.Add(tipo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Tipo tipo, int id)
        {
            var exist = await dbContext.Tipos.AnyAsync(x => x.Id == id);

            if(!exist)
            {
                return NotFound("El tipo especificado no existe. ");
            }

            if(tipo.Id != id)
            {
                return BadRequest("El id del tipo no coincide con el establecido en la URL. ");
            }

            dbContext.Update(tipo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Tipos.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            dbContext.Remove(new Tipo { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
