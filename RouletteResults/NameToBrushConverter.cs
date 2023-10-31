using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using RouletteResults.ViewModels;
using Color = RouletteResults.Data.Color;

namespace RouletteResults
{
    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = GetColorNameByIdAsync((int?)value).Result.Name;
            return input switch
            {
                "Red" => HexToBrush("#BC1318"),
                "Black" => Brushes.Black,
                "Blue" => HexToBrush("#00E6C9"),
                "Green" => HexToBrush("#009B01"),
                _ => DependencyProperty.UnsetValue
            };
        }

        Brush HexToBrush(string hex) => (Brush) new BrushConverter().ConvertFrom(hex);

        public async Task<Color> GetColorNameByIdAsync(int? id) => await PositionListVM._repo.GetColorByIdAsync(id);


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
