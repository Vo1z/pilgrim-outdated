using Ingame.PlayerLegacy;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Player
{
    public sealed class PlayerModelProvider : MonoProvider<PlayerModel>
    {
        [Inject]
        private void Construct(PlayerData injectedPlayerData)
        {
            value = new PlayerModel
            {
                playerData = injectedPlayerData,
            };
        }
    }
}