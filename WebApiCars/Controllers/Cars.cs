using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCars.Entidades;

namespace WebApiCars.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CarsController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Car car)
        {
            dbContext.Add(car);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]   //api/cars
        [HttpGet("listado")]    //api/cars/listado
        [HttpGet("/listado")]      // /listado
        public async Task<ActionResult<List<Car>>> Get()
        {
            return await dbContext.Cars.Include(x => x.Tipos).ToListAsync();
        }

        [HttpGet("primero")] //api/cars/primero
        public async Task<ActionResult<Car>> PrimerAutomovil([FromHeader] int valor, [FromQuery] string automovil, [FromQuery] int automovilId)
        {
            return await dbContext.Cars.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await dbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Car>> Get([FromRoute] string nombre)
        {
            var car = await dbContext.Cars.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet("{id:int}/{parametro?}")]
        public ActionResult<Car> Get(int id, string parametro)
        {
            var car = dbContext.Cars.FirstOrDefault(x => x.Id == id);

            //if (car == null)
            //{
            //    return NotFound();
            //}

            return car;
        }








        ///------------------------------------------------------------------------------------------------///////////
        [HttpPut]
        public async Task<ActionResult> Put(Car car, int id)
        {
            if (car.Id != id)
            {
                return BadRequest("El ID del automovil no concide con el establecido en la base de datos");
            }

            dbContext.Update(car);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Cars.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Car()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        /* public ActionResult<List<Car>> Get()
         {
             return new List<Car>()
             {
                 new Car() { Id = 1, Name = "Chevrolet Cavalier Turbo"},
                 new Car() { Id = 2, Name = "Nissan Versa 2014"}
             };
         }*/
    }
}
