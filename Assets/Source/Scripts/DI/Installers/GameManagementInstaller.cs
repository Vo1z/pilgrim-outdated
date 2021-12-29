using Support;
using Zenject;

namespace Ingame.Installers
{
    public class GameManagementInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var stationaryInputSystem = new StationaryInputSystem();
            stationaryInputSystem.Enable();
            
            Container.Bind<StationaryInputSystem>()
                .FromInstance(stationaryInputSystem)
                .AsSingle()
                .NonLazy();
        }
    }
}