using Microsoft.EntityFrameworkCore;
using Storage.DataTables;

namespace Storage
{
    public class ResolutionDataContext : DbContext
    {
        public ResolutionDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ResolutionData> ResolutionData { get; set; }
    }
}
