using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace MIAM
{
    public interface IMiamDbContext : IDisposable
    {
        DbSet<Repas> Repas { get; set; }
    }

    class InMemoryDbContext : ContextFactory, IMiamDbContext
    {
        public InMemoryDbContext(List<Repas> unRepasPourChaqueJourDeLaSemaine)
        {
            IQueryable<Repas> data = unRepasPourChaqueJourDeLaSemaine.AsQueryable();
            Mock<DbSet<Repas>> mockSet = new Mock<DbSet<Repas>>();
            mockSet.As<IQueryable<Repas>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Repas>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Repas>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Repas>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            Repas = mockSet.Object;
        }

        public override IMiamDbContext CreateContext()
        {
            return this;
        }

        public DbSet<Repas> Repas { get; set; }

        public void Dispose()
        {
        }
    }

    public class MiamDbContext : DbContext, IMiamDbContext
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
