using Voody.UniLeo;
using Zenject;

namespace Ingame.Player
{
    public sealed class PlayerModelProvider : MonoProvider<PlayerModel>
    {
        [Inject]
        private void Construct(PlayerMovementData injectedPlayerMovementData, PlayerHudData playerHudData)
        {
            value = new PlayerModel
            {
                playerMovementData = injectedPlayerMovementData,
                playerHudData = playerHudData
            };
        }
    }
}