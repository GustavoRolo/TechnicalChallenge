using ApiChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Data
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> opts) : base(opts)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar a entidade Recycler
            modelBuilder.Entity<Recycler>().HasData(
                new Recycler
                {
                    id = Guid.NewGuid(),
                    run = false,
                    days = 0
                }
            );
        }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Recycler> Recyclers { get; set; }

    }
}
