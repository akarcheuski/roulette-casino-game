namespace RouletteResults.Data
{
    public class Position : BaseModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool? EvenOdd { get; set; }
        public int? DozenId { get; set; }
        public virtual Color Color { get; set; }
        public virtual Dozen Dozen { get; set; }
        public Column? Column { get; set; }
        
        private int? _colorId;
        public int? ColorId
        {
            get => _colorId;
            set => SetProperty(ref _colorId, value);
        }
    }
}
