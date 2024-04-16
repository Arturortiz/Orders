using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.DTOs;
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
                //.Include(c => c.States)//esto indica como el inner join el sql
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>()
            {
                wasSuccess = true,
                Result = countries
            };
            
        }
        //----------------------------------------------------------------------------------------------------
        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))//si el filtro no es nulo o vacio
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower())); //iguala el nombre el minucula con el filtro introducido en minuscula
            }

            return new ActionResponse<IEnumerable<Country>>
            {
                wasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        //cambiamos la paginacion segun el filtro que pongamos ya que tambien va a afectar
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));//donde el nombre del pais contenga el filtro introducido
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                wasSuccess = true,
                Result = totalPages
            };
        }
    }
}
