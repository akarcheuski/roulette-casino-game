using RouletteResults.Data.Services;
using Unity;
using Unity.Lifetime;

namespace RouletteResults
{
    public static class IoCHelper
    {
        public static IUnityContainer Container { get; }
        static IoCHelper()
        {
            Container = new UnityContainer();
            Container.RegisterType<IPositionsRepository, PositionsRepository>(
                new ContainerControlledLifetimeManager());
        }
    }
}
