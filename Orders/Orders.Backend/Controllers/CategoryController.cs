using Orders.Backend.UnitsOfWork.Interfaces;

namespace Orders.Backend.Controllers
{
    public class CategoryController : GenericController<Category>
    {
        //min 47 vid 11
        public CategoryController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork) //a los controladores le injectamos la unidad de trabajo
        { 
        }
    }
}
