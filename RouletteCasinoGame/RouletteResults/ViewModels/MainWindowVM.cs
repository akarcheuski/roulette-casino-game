using RouletteResults.Data;
using Unity;

namespace RouletteResults.ViewModels
{
    internal class MainWindowVM : BaseModel
    {
        public MainWindowVM()
        {
            var positionListVm = IoCHelper.Container.Resolve<PositionListVM>();
            MainVM = positionListVm;
        }

        private BaseModel _MainVM;
        public BaseModel MainVM
        {
            get => _MainVM;
            set => SetProperty(ref _MainVM, value);
        }
    }
}
