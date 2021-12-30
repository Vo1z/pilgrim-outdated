using Ingame.Player;
using Ingame.Utilities;
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
        [SerializeField] private GroundDetection groundDetection;
        [BoxGroup("Components"), Required]
        [SerializeField] private PlayerHUD playerHUD;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle();

            Container.Bind<PlayerInputReceiver>()
                .FromInstance(playerInputReceiver)
                .AsSingle()
                .NonLazy();

            Container.Bind<GroundDetection>()
                .FromInstance(groundDetection)
                .AsSingle();

            Container.Bind<PlayerHUD>()
                .FromInstance(playerHUD)
                .AsSingle();
        }
    }
}