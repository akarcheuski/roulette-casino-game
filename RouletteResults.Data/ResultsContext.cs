using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows;

namespace RouletteResults.Data
{
    public class ResultsContext : DbContext
    {
        const string DbPath = @"RouletteResults.Data\Sql\RouletteResultsData.db";
        public DbSet<Position> Positions { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Dozen> Dozens { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            var projPath = Path.GetDirectoryName(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName);
            ob.UseSqlite($"Data Source={Path.Combine(projPath, DbPath)}");
            // ob.UseSqlite("FileName=RouletteResultsData.db");
        }
    }
}
