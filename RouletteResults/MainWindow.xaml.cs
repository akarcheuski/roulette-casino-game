using System.Windows;
using RouletteResults.Data;

namespace RouletteResults
{
    public partial class MainWindow : Window
    {
        static ResultsContext _ctx = new();
        //readonly string dbName = "RouletteResultsData";

        public MainWindow()
        {
            InitializeComponent();

           _ctx.Database.EnsureCreated();
        }
    }
}
