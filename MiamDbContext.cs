using System.Data.Entity;

namespace MIAM
{
    // https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
    //public class InMemoryMiamDbContext
    //{
    //    public InMemoryMiamDbContext(IList<Repas> repas)
    //    {
    //        IQueryable<Repas> data = repas.AsQueryable();
    //        Mock<DbSet<Repas>> mockSet = new Mock<DbSet<Repas>>();
    //        mockSet.As<IQueryable<Repas>>().Setup(m => m.Provider).Returns(data.Provider);
    //        mockSet.As<IQueryable<Repas>>().Setup(m => m.Expression).Returns(data.Expression);
    //        mockSet.As<IQueryable<Repas>>().Setup(m => m.ElementType).Returns(data.ElementType);
    //        mockSet.As<IQueryable<Repas>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    //    }

    //    public DbSet<Repas> Repas { get; set; }
    //}


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
