using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCars.Entidades
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no acepta mas de 30 caracteres.")]
        public string Name { get; set; }
        [Range(1980, 2022, ErrorMessage = "El rango aceptado va desde 1980 hasta 2022 dentro del campo Age.")]
        [NotMapped]
        public int Age { get; set; }

        [NotMapped]
        [CreditCard]
        public string Card { get; set; }


        [NotMapped]
        [Url]
        public string Url { get; set; }


        public List<Tipo> Tipos { get; set; }
    }
}
