using Microsoft.EntityFrameworkCore;
using WebApiCars.Entidades;

namespace WebApiCars
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Tipo> Tipos { get; set; }
    }
}
