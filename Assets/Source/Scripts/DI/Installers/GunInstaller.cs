using Ingame.Guns;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public sealed class GunInstaller : MonoInstaller
    {
        [BoxGroup("Data"), Required]
        [SerializeField] private GunData gunData;
        [BoxGroup("Components"), Required]
        [SerializeField] private SurfaceDetector surfaceDetector;
        
        public override void InstallBindings()
        {
            Container.Bind<GunData>()
                .FromInstance(gunData)
                .AsSingle();
            
            Container.Bind<SurfaceDetector>()
                .FromInstance(surfaceDetector)
                .AsSingle();
        }
    }
}