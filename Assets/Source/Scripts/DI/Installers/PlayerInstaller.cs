using Ingame.Player;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Required]
        [SerializeField] private PlayerMovementData playerMovementData;
        [Required]
        [SerializeField] private PlayerHudData playerHudData;
        [Required]
        [SerializeField] private PlayerInventoryData playerInventoryData;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerMovementData>()
                .FromInstance(playerMovementData)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<PlayerHudData>()
                .FromInstance(playerHudData)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<PlayerInventoryData>()
                .FromInstance(playerInventoryData)
                .AsSingle()
                .NonLazy();
        }
    }
}