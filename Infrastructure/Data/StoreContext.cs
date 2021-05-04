using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //constucter create
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        //crate dbset
        public DbSet<Product> Products { get; set; }
    }
}