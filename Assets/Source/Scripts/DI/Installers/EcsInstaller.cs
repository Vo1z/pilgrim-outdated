using Leopotam.Ecs;
using Zenject;

namespace Ingame.DI.Installers
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var world = new EcsWorld();
            var systems = new EcsSystems(world);
            
            Container.Bind<EcsWorld>()
                .FromInstance(world)
                .AsSingle()
                .NonLazy();

            Container.Bind<EcsSystems>()
                .FromInstance(systems)
                .AsSingle()
                .NonLazy();
        }
    }
}