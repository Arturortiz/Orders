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

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())//si no hay categorias
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())//si no hay categorias
            {
                _context.Countries.Add(new Country
                {
                    Name = "España",
                    States =
                    [
                        new State()
                        {
                            Name="Cáceres",
                            Cities=
                            [
                                new City(){Name="Trujillo"},
                                new City(){Name="Cáceres"},
                                new City(){Name="Plasencia"},
                                new City(){Name="Alcántara"},
                                new City(){Name="Coria"},
                                new City(){Name="Guadalupe"}
                            ]
                        },
                        new State()
                        {
                            Name="Zamora",
                            Cities=
                            [
                                new City(){Name="Benavente"},
                                new City(){Name="Toro"},
                                new City(){Name="Zamora"},
                                new City(){Name="Fuentelapeña"},
                                new City(){Name="San Vitero"},
                                new City(){Name="Mahíde"}
                            ]
                        },
                        new State()
                        {
                            Name="Almeria",
                            Cities=
                            [
                                new City(){Name="Vera"},
                                new City(){Name="Almeria"},
                                new City(){Name="Mojácar"},
                                new City(){Name="Carboneras"},
                                new City(){Name="Garrucha"},
                                new City(){Name="Viator"}
                            ]
                        }
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                   [
                       new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new() { Name = "Medellín" },
                                new() { Name = "Itagüí" },
                                new() { Name = "Envigado" },
                                new() { Name = "Bello" },
                                new() { Name = "Rionegro" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new() { Name = "Usaquen" },
                                new() { Name = "Champinero" },
                                new() { Name = "Santa fe" },
                                new() { Name = "Useme" },
                                new() { Name = "Bosa" },
                            ]
                        },
                    ]
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Orlando" },
                                new() { Name = "Miami" },
                                new() { Name = "Tampa" },
                                new() { Name = "Fort Lauderdale" },
                                new() { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });
            }
            await _context.SaveChangesAsync();
        }
        /*
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
        */
    }
}
