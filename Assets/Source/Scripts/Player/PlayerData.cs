using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerData", fileName = "Ingame/NewPlayerData")]
    public class PlayerData : ScriptableObject
    {
        [BoxGroup("Movement")][SerializeField][Min(0)] private float mass;
        [BoxGroup("Movement")][SerializeField][Min(0)] private float speed;

        public float Mass => mass;
        public float Speed => speed;
    }
}