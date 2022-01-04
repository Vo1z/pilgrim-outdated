using Ingame.Guns;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public sealed class GunInstaller : MonoInstaller
    {
        [BoxGroup("Data"), Required]
        [SerializeField] private GunStatsData gunStatsData;
        [BoxGroup("Components"), Required]
        [SerializeField] private GunSurfaceDetector gunSurfaceDetector;
        
        public override void InstallBindings()
        {
            Container.Bind<GunStatsData>()
                .FromInstance(gunStatsData)
                .AsSingle();
            
            Container.Bind<GunSurfaceDetector>()
                .FromInstance(gunSurfaceDetector)
                .AsSingle();
        }
    }
}