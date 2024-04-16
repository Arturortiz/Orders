using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>//: ControllerBase //heredar de controllerBase
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork) //a los controladores le injectamos la unidad de trabajo
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        //estos dos HttpGet van a devolver los paises o el pais junto a sus estados o ciudades
        [HttpGet("full")]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if (response.wasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }
        //--------------------------------------------------------------
        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var response = await _countriesUnitOfWork.GetAsync(pagination);
            if (response.wasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] PaginationDTO pagination)//hacemos un override de totalPages pq sino usaría el del genericController
        {
            var action = await _countriesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.wasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        //--------------------------------------------------------------
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _countriesUnitOfWork.GetAsync(id);
            if (response.wasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
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
