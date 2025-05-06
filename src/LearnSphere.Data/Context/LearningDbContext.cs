using LearnSphere.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearnSphere.Data.Context
{
    public class LearningDbContext : DbContext
    {
        public LearningDbContext(DbContextOptions<LearningDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses { get; set; } = default!;
    }
}
