using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Countries
{
    public partial class CountryForm //partial indica que a la hora de compilar CountryForm el .razor y el .cs se compilan como una sola
    {
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public Country Country { get; set; } = null!; //el pais que voy a editar
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; } //el codigo cuando grave el pais
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }// el codigo cuando cancele el pais
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!; // injectamos el sweetAlert que lo usaremos para el diseño del formulario
        public bool FormPostedSuccessfully { get; set; } //nos indicara si el formulario se ejecutara bien o no

        protected override void OnInitialized()
        {
            editContext = new(Country);
        }


        private async Task OnBeforeInternalNavigation(LocationChangingContext context) //formulario, me pregunta si yo me sali del formulario sin haber guardado los cambios
        {
            var formWasEdited = editContext.IsModified(); //una variable que indica si se han realizado cambios
            if(!formWasEdited || FormPostedSuccessfully) //si no se han realizado cambios no pasa nada
            {
                return;
            }
            //else
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions //variable que es una ventana indicandote el problema
            { 
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });

            var confirm = !string.IsNullOrEmpty(result.Value); //una variable que recoge la respuesta del usuario
            if( confirm )//si el usuario si que quiere perder los cambios sale del formulario
            {
                return;
            }
            //else
            context.PreventNavigation();// si el usuario quiere guardar los cambios obliga a mantener el formulario
        }
    }
}
