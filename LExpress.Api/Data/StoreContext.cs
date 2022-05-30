using LExpress.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace LExpress.Api.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
