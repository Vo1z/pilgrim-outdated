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
        [BoxGroup("Components"), Required]
        [SerializeField] private PlayerInputReceiver playerInputReceiver;
        [BoxGroup("Components"), Required] 
        [SerializeField] private PlayerObserver playerObserver;
        [BoxGroup("Components"), Required] 
        [SerializeField] private PlayerMover playerMover;
        [BoxGroup("Transforms"), Required]
        [SerializeField] private Transform hands;
        [BoxGroup("Transforms"), Required]
        [SerializeField] private Transform hudParent;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle();

            Container.Bind<PlayerInputReceiver>()
                .FromInstance(playerInputReceiver)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerObserver>()
                .FromInstance(playerObserver)
                .AsSingle();

            Container.Bind<PlayerMover>()
                .FromInstance(playerMover)
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