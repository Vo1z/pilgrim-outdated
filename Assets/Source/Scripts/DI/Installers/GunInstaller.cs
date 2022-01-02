using Ingame.Guns;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public sealed class GunInstaller : MonoInstaller
    {
        [SerializeField] private GunStatsData gunStatsData;

        public override void InstallBindings()
        {
            Container.Bind<GunStatsData>()
                .FromInstance(gunStatsData)
                .AsSingle();
        }
    }
}