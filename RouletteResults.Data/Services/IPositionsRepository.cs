using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteResults.Data.Services
{
    public interface IPositionsRepository
    {
        Task<List<Position>> GetPositionsAsync();
        Task<Color> GetColorByIdAsync(int? id);
        Task<List<Dozen>> GetAllDozensAsync();
        Task<List<Color>> GetAllColorsAsync();
       // Task<Color> GetColorById(int id);
    }
}
