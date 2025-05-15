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
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionShare> TransactionShares { get; set; } = null!;
    }
}