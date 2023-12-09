using ApiChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Data
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> opts):base(opts) 
        {
           
        }

        public DbSet<Server> Servers { get; set; }
    }
}
