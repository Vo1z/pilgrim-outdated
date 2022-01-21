using Ingame.PlayerLegacy;

namespace Ingame
{
    public struct PlayerModel
    {
        public PlayerData playerData;
        
        public float currentSpeed;
        public bool isCrouching;
        public LeanDirection currentLeanDirection;
    }
}