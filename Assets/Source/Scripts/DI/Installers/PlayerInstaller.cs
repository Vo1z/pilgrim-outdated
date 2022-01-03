using Ingame.Player;
using Ingame.Player.HUD;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Ingame.DI.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [BoxGroup("Data"), Required]
        [SerializeField] private PlayerData playerData;
        [BoxGroup("Components"), Required]
        [SerializeField] private PlayerInputReceiver playerInputReceiver;
        [BoxGroup("Components"), Required]
        [SerializeField] private PlayerHUD playerHUD;
        [BoxGroup("Components"), Required] 
        [SerializeField] private PlayerObserver playerObserver;
        [BoxGroup("Components"), Required]
        [SerializeField] private PlayerRotator playerRotator;
        [BoxGroup("Components"), Required] 
        [SerializeField] private PlayerMovementController playerMovementController;
        [BoxGroup("Transforms"), Required]
        [SerializeField] private Transform hands;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle();

            Container.Bind<PlayerInputReceiver>()
                .FromInstance(playerInputReceiver)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerHUD>()
                .FromInstance(playerHUD)
                .AsSingle();

            Container.Bind<PlayerObserver>()
                .FromInstance(playerObserver)
                .AsSingle();
            
            Container.Bind<PlayerRotator>()
                .FromInstance(playerRotator)
                .AsSingle();
            
            Container.Bind<PlayerMovementController>()
                .FromInstance(playerMovementController)
                .AsSingle();
            
            Container.Bind<Transform>()
                .WithId("Hands")
                .FromInstance(hands)
                .AsSingle();
        }
    }
}