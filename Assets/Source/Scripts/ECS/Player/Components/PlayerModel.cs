using Ingame.PlayerLegacy;

namespace Ingame.Player
{
    public struct PlayerModel
    {
        public PlayerData playerData;
        
        public float currentSpeed;
        public bool isCrouching;
        public LeanDirection currentLeanDirection;
    }
}