using System.Data.Entity;
using Infrastructure.DTO;

namespace Server.Data
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext()
            : base("name=ServerDbConnectionString")
        { }

        public DbSet<DataPoint> DataPoints { get; set; }
    }
}
