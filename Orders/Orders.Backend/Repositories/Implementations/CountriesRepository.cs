using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;
        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var countries = await _context.Countries
                .Include(c => c.States)//esto indica como el inner join el sql
                .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if(countries == null)
            {
                new ActionResponse<Country>()
                {
                    wasSuccess = false,
                    Message = "Pais no existe"
                };
            }
            return new ActionResponse<Country>()
            {
                wasSuccess = true,
                Result = countries
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries//nos va a mostrar los countries con su lista de estados.
                .Include(c => c.States)//esto indica como el inner join el sql
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>()
            {
                wasSuccess = true,
                Result = countries
            };
            
        }
    }
}
