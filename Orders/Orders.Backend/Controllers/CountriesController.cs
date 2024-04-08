using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>//: ControllerBase //heredar de controllerBase
    {

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork) //a los controladores le injectamos la unidad de trabajo
        {
        }
        /*
        private readonly DataContext _context;

        public CountriesController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]//visualizar todos los countries, por eso lo hacemos sin parametros
        public async Task<IActionResult> GetAsync() 
        {
            return Ok(await _context.Countries.AsNoTracking().ToListAsync()); //añadido el AsNoTracking, para la lectura de consultas más complejas.
        }
        
        [HttpGet("{id}")]//visualizar el country por id
        public async Task<IActionResult> GetAsync(int id) 
        {
            var country= await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        
        [HttpPost]// insertar country
        public async Task<IActionResult> PostAsync(Country country) 
        {
            _context.Add(country);//añadimos a la base de datos
            await _context.SaveChangesAsync();//guarada los cambios en la base de datos
            return Ok(country);
        }
        
        [HttpPut]// modificar country
        public async Task<IActionResult> PutAsync(Country country) 
        {
            _context.Update(country);//añadimos a la base de datos
            await _context.SaveChangesAsync();//guarada los cambios en la base de datos
            return NoContent();
        }
        
        [HttpDelete]// eliminar country
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Remove(country);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        */
    }
}
