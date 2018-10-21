using System.Data.Entity;

namespace MIAM.Database
{
    public class MiamDbContext : DbContext
    {

        public MiamDbContext() : base("name=miamConnectionString")
        {
        }

        public DbSet<Repas> Repas { get; set; }

        protected override void OnModelCreating(DbModelBuilder ModelBuilder)
        {
        }
    }
}
