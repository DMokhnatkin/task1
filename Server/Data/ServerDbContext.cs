using System.Data.Entity;
using Infrastructure.DTO;
using Server.Data.DAO;

namespace Server.Data
{
    class ServerDbContext : DbContext
    {
        public ServerDbContext()
            : base("name=ServerDbConnectionString")
        { }

        public DbSet<MeteringDAO> Meterings { get; set; }
        public DbSet<MeteringSensorValueRelationDAO> MeteringSensorRelations { get; set; }
        public DbSet<SensorValueDAO> SensorValues { get; set; }
    }
}
