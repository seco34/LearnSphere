using LearnSphere.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LearnSphere.Data
{
    public class LearningDbContextFactory
        : IDesignTimeDbContextFactory<LearningDbContext>
    {
        public LearningDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LearningDbContext>();
            // Migration'lar için aynı connection string'i burada da kullanıyoruz:
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=LearnSphereDb;Trusted_Connection=True;"
            );

            return new LearningDbContext(optionsBuilder.Options);
        }
    }
}
