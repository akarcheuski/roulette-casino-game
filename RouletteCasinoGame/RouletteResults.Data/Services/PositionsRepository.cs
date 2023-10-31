using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RouletteResults.Data.Services
{
    public class PositionsRepository : IPositionsRepository
    {
        ResultsContext _ctx = new();
        public Task<List<Position>> GetPositionsAsync() => _ctx.Positions.ToListAsync();
        public Task<Color> GetColorByIdAsync(int? id) => _ctx.Colors.FirstOrDefaultAsync(c => c.Id == id);
        public Task<List<Dozen>> GetAllDozensAsync()=> _ctx.Dozens.ToListAsync();
        public Task<List<Color>> GetAllColorsAsync() => _ctx.Colors.ToListAsync();
    }
}
