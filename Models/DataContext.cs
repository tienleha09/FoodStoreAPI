using Microsoft.EntityFrameworkCore;

namespace FoodStoreAPI.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt): base(opt) { }
        
        public DbSet<Product> Products { get; set; }
    }
}
