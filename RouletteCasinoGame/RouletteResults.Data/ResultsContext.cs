using Microsoft.EntityFrameworkCore;

namespace RouletteResults.Data
{
    public class ResultsContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Dozen> Dozens { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder ob) =>
            ob.UseSqlite("FileName=Sql\\RouletteResultsData.db");
       
    }
}
