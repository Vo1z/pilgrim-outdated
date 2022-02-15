using Ingame.PlayerLegacy;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public class PlayerLegacyInstaller : MonoInstaller
    {
        [BoxGroup("Data"), Required]
        [SerializeField] private PlayerData playerData;
        [BoxGroup("Transforms"), Required]
        [SerializeField] private Transform hands;
        [BoxGroup("Transforms"), Required]
        [SerializeField] private Transform hudParent;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle();

            Container.Bind<Transform>()
                .WithId("Hands")
                .FromInstance(hands)
                .AsCached();
            
            Container.Bind<Transform>()
                .WithId("HudParent")
                .FromInstance(hudParent)
                .AsCached();
        }
    }
}