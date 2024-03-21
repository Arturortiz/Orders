using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;

namespace Orders.Frontend.Shared
{
    public partial class GenericList<Titem>
    {
        [Parameter] public RenderFragment? Loading {  get; set; } //lo que yo quiero que me muestre mi componente cuando esta cargando
        [Parameter] public RenderFragment? NoRecord {  get; set; } //lo que yo quiero que me muestre mi componente cuando no encuentra registros (paises por ejemplo)
        [EditorRequired, Parameter] public RenderFragment Body { get; set; } = null!;// el cuerpo
        [EditorRequired, Parameter] public List<Titem> MyList { get; set; } = null!;//la lista de productos
    }
}
