using Orders.Backend.UnitsOfWork.Interfaces;

namespace Orders.Backend.Controllers
{
    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenericUnitOfWork _unitOfWork;

        public GenericController(IGenericUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //el actionResult indica como la respuesta de la accion

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action = await _unitOfWork.GetAsync();
            if (action.wasSucces)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action = await _unitOfWork.GetAsync(id);
            if (action.wasSucces)
            {
                return Ok(action.Result);
            }
            return NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync()
        {
            var action = await _unitOfWork.AddAsync();
            if (action.wasSucces)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(T model)
        {
            var action = await _unitOfWork.UpdateAsync(model);
            if (action.wasSucces)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _unitOfWork.UpdateAsync(id);
            if (action.wasSucces)
            {
                return NoContent();
            }
            return BadRequest(action.Message);
        }
    }
}
