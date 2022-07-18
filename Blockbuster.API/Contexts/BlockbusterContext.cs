using Blockbuster.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blockbuster.API.Contexts
{
    public class BlockbusterContext : DbContext
    {
        public BlockbusterContext(DbContextOptions<BlockbusterContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Movie> Movies { get; set; } = default!;
    }
}
