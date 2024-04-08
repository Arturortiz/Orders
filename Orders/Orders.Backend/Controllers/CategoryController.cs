using Microsoft.AspNetCore.Mvc;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : GenericController<Category>
    {
        //min 47 vid 11
        public CategoryController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork) //a los controladores le injectamos la unidad de trabajo
        { 
        }
    }
}
