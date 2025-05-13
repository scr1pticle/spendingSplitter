using Microsoft.EntityFrameworkCore;

namespace api.Models

{
    public class SplitterContext : DbContext
    {
        public SplitterContext(DbContextOptions<SplitterContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; } = null!;
    }
}