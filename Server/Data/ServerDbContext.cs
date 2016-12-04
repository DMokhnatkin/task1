using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
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

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
