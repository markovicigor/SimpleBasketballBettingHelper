using BBallByStats.Models;
using Microsoft.EntityFrameworkCore;

namespace BBallByStats.Repository
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
    }
}
