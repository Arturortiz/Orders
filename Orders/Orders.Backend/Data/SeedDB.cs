using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        public SeedDB(DataContext context) //un datacontext pq quiero que se enlace con la base de datos
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();//se va a encargar de crear la base de datos de establecer todas las migraciones pendientes
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
        }

        public async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())//si no hay categorias
            {
                _context.Countries.Add(new Country { Name = "España" });
                _context.Countries.Add(new Country { Name = "Portugal" });
                _context.Countries.Add(new Country { Name = "Alemania" });
                _context.Countries.Add(new Country { Name = "Italia" });
                _context.Countries.Add(new Country { Name = "Francia" });
                _context.Countries.Add(new Country { Name = "Bélgica" });
                _context.Countries.Add(new Country { Name = "Holanda" });
                await _context.SaveChangesAsync();
            }
        }
        public async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())//si no hay categorias
            {
                _context.Categories.Add(new Category { Name = "Bebida" });
                _context.Categories.Add(new Category { Name = "Carne" });
                _context.Categories.Add(new Category { Name = "Pescado" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Cocina" });
                _context.Categories.Add(new Category { Name = "Bricolage" });
                _context.Categories.Add(new Category { Name = "Lácteos" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
