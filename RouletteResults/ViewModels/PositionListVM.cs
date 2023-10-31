using System;
using System.Collections.Generic;
using RouletteResults.Data.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteResults.Data;
using static System.Int32;
using static System.Windows.MessageBox;
using Color = RouletteResults.Data.Color;

namespace RouletteResults.ViewModels
{
    public class PositionListVM : BaseModel
    {
        internal static IPositionsRepository _repo;

        const int port = 4948;
        const string ip = "127.0.0.1";

        public PositionListVM(IPositionsRepository repo)
        {
            _repo = repo;
        }

        private ObservableCollection<Dozen> _dozens;
        public ObservableCollection<Dozen> Dozens
        {
            get => _dozens;
            set => SetProperty(ref _dozens, value);
        }

        private ObservableCollection<Color> _colors;
        public ObservableCollection<Color> Colors
        {
            get => _colors;
            set => SetProperty(ref _colors, value);
        }

        private ObservableCollection<Position> _positions;
        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        private ObservableCollection<Position> _positionsColumnA;
        public ObservableCollection<Position> PositionsColumnA
        {
            get => _positionsColumnA;
            set => SetProperty(ref _positionsColumnA, value);
        }

        private ObservableCollection<Position> _positionsColumnB;
        public ObservableCollection<Position> PositionsColumnB
        {
            get => _positionsColumnB;
            set => SetProperty(ref _positionsColumnB, value);
        }

        private ObservableCollection<Position> _positionsColumnC;
        public ObservableCollection<Position> PositionsColumnC
        {
            get => _positionsColumnC;
            set => SetProperty(ref _positionsColumnC, value);
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set => SetProperty(ref _isOpen, value);
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private Position _pos0;
        public Position Pos0
        {
            get => _pos0;
            set => SetProperty(ref _pos0, value);
        }

        private List<Position> posList;
        private List<Dozen> dozList;
        private List<Color> colList;

        public async void Initialize()
        {
            try
            {
                posList = await _repo.GetPositionsAsync();
                dozList = await _repo.GetAllDozensAsync();
                colList = await _repo.GetAllColorsAsync();
            }
            catch (Exception ex)
            {
                _ = Show(ex.Message);
            }
            Dozens = new ObservableCollection<Dozen>(dozList);
            Positions = new ObservableCollection<Position>(posList);
            PositionsColumnA = GetPositionByColumn(Column.A);
            PositionsColumnB = GetPositionByColumn(Column.B);
            PositionsColumnC = GetPositionByColumn(Column.C);
            Pos0 = posList.First(p=>p.Number.Equals(0));
            Colors = new ObservableCollection<Color>(colList.Where(c => c.Name is "Red" or "Black"));

            await ListenAsync();
        }

        ObservableCollection<Position> GetPositionByColumn(Column c) => new(posList.Where(p => p.Column.Equals(c)));


        private async Task ListenAsync()
        {
            try
            {
                while (true)
                {
                    var listener = new TcpListener(IPAddress.Parse(ip), port);
                    listener.Start();
                    using var client = await listener.AcceptTcpClientAsync();
                    await using var stream = client.GetStream();
                    using var sr = new StreamReader(stream, new UTF8Encoding(), false);
                    using var jtr = new JsonTextReader(sr);
                    var data = new JsonSerializer().Deserialize(jtr)?.ToString();
                    var json = JObject.Parse(data ?? ThrowNullEx(nameof(data)));
                    var num = json.SelectToken("Data.WinningNumber")?.ToString();
                    var pos = Positions.Single(p => p.Number == Parse(num ?? ThrowNullEx(nameof(num))));
                    var prevColor = pos.ColorId;
                    var c = Colors.Single(c => c.Id == pos.ColorId);
                    pos.ColorId = c.Id  = 3;
                    
                    Position = pos;

                    IsOpen = true;
                    await Task.Delay(3000);
                    IsOpen = false;
                    pos.ColorId = c.Id = prevColor;
                    listener.Server.Dispose();
                }
            }
            catch (Exception ex)
            {
                _ = Show(ex.Message);
            }
        }
    }
}
