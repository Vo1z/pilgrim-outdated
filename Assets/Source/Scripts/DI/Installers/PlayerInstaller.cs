using Ingame.PlayerLegacy;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Required]
        [SerializeField] private PlayerData playerData;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle()
                .NonLazy();
        }
    }
}