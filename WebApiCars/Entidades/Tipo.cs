namespace WebApiCars.Entidades
{
    public class Tipo
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
