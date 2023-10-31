namespace RouletteResults.Data
{
    public class Color : BaseModel
    {
        private int? _id;
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Name { get; set; }
    }
}
