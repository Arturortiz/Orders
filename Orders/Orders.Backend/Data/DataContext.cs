using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;
//database-datacontext-repository-unitofwork-controller
namespace Orders.Backend.Data
{
    public class DataContext : IdentityDbContext<User>/*DbContext*/
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) //para conectarse a la base de datos
        { 
        }

        //creacion de las tablas
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            //esto lo hacemos pq puede haber ciudades que se llamen igual pero sean de distintos paises, nos permite que halla dos albuquerques pero en distintos paises o estados.
            modelBuilder.Entity<City>().HasIndex(x=> new {x.StateId, x.Name}).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x=> new {x.CountryId, x.Name}).IsUnique();
            DisableCascadeDelete(modelBuilder);
        }
        //video 20 min 17
        //Deshabilitar el borrado en cascada
        private void DisableCascadeDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships) 
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
