using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Responses
{
    public class ActionResponse<T> //Las respuestas, queremos que nos devuelva una respuesta de T es decir una respuesta de countries de gategories etc.
    {
        public bool wasSuccess { get; set; }// si fue exitoso o no
        public string? Message { get; set; } // ? puede ser nulo porque va a ser un mensage que se lanze solo cuando halla error
        public T? Result { get; set; } //una respuesta del tipo de actionResponse que estemos realizando, tambien puede ser nulo porque si todo sale bien devuelve el objeto sino devuelve el mensaje de arriba de error
        
    }
}
