using Microsoft.EntityFrameworkCore;

namespace SuperHerosApi.Data
{
    public class DataContext: DbContext //from Microsoft.EntityFrameworkCore
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<SuperHeros> SuperHeros { get; set; }
    }
}
