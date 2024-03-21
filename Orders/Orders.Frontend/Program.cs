using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Orders.Frontend;
using Orders.Frontend.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//conectarse el front con el back
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7154/") });//ubicacion del backend
builder.Services.AddScoped<IRepository, Repository>();// cada vez que llamen a Irepository va a pasar la implementacion de Repository

await builder.Build().RunAsync();
