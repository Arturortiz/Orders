using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; } //ya tiene establecido autoIncrement

        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]//maximo de caracteres
        [Required(ErrorMessage = "El campo {0} es requerido.")]//el campo name es obligatorio
        public String Name { get; set; } = null!; //not null es decir es obligatorio
    }
}