﻿using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;
using System.Net;

namespace Orders.Frontend.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountriesIndex
    {
        private int currentPage = 1;
        private int totalPages;
        [Inject] private IRepository repository { get; set; }=null;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        public List<Country>? Countries { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await LoadAsync();
            
        }
        /*
        private async Task LoadAsync()
        {
            var responseHttp = await repository.GetAsync<List<Country>>("api/countries"); //estamos yendo al backend y rellenando una lista con los paises || se pone la url del controlador del back
            
            if (responseHttp.Error) 
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Countries = responseHttp.Response;
        }
        */
        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task LoadAsync(int page = 1)
        {
            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }
            var ok = await LoadListAsync(page);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private async Task<bool> LoadListAsync(int page)
        {
            var url = $"api/countries?page={page}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await repository.GetAsync<List<Country>>(url);
            //var responseHttp = await repository.GetAsync<List<Country>>($"api/countries?page={page}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Countries = responseHttp.Response;
            return true;
        }

        private async Task LoadPagesAsync()
        {
            var url = "api/countries/totalPages";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"?filter={Filter}";
            }

            var responseHttp = await repository.GetAsync<int>(url);
            //var responseHttp = await repository.GetAsync<int>("api/countries/totalPages");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            await ApplyFilterAsync();
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task DeleteAsync(Country country) 
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions //variable que es una ventana indicandote el problema
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de eliminar el pais {country.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value); //una variable que recoge la respuesta del usuario
            if (confirm)//si el usuario si que quiere perder los cambios sale del formulario
            {
                return;
            }

            var responseHttp = await repository.DeleteAsync<Country>($"api/countries/{country.Id}");
            if (responseHttp.Error)
            {
                if(responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound) //si el pais no existe
                {
                    navigationManager.NavigateTo("/countries");//mandamos a la de countries es decir recargamos la pagina
                }
                else
                { 
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con éxito.");
        }
    }
}
