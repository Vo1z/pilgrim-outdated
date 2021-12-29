using Ingame.Player;
using UnityEngine;
using Zenject;

namespace Ingame.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CharacterController playerCharacterController;
        [SerializeField] private PlayerData playerData;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterController>()
                .FromInstance(playerCharacterController)
                .AsSingle();

            Container.Bind<PlayerData>()
                .FromInstance(playerData)
                .AsSingle();
        }
    }
}