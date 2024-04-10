using Orders.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Orders.Shared.Entities
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; } //ya tiene establecido autoIncrement

        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]//maximo de caracteres
        [Required(ErrorMessage = "El campo {0} es requerido.")]//el campo name es obligatorio
        public string Name { get; set; } = null!; //not null es decir es obligatorio
        //relacion 1:N con state
        public ICollection<State>? States { get; set; }//un country tiene una lista de estados, que puede ser nula

        [Display(Name ="Departamentos / Estados")]
        public int StatesNumber => States == null || States.Count == 0 ? 0 : States.Count; //si la lista de states es nula o al contar el numero de estados=0 devuelve 0 si no devuelve el States.Count el numero de estados de la lista
    }
}