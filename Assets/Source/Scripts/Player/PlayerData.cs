using NaughtyAttributes;
using UnityEngine;

namespace Ingame.Player
{
    [CreateAssetMenu(menuName = "Ingame/PlayerData", fileName = "Ingame/NewPlayerData")]
    public class PlayerData : ScriptableObject
    {
        
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float speed = 10;
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float movementAcceleration = 100;
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float movementFriction = 10;
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float jumpForce = 5;
        [BoxGroup("Rotation")] 
        [SerializeField] [Min(0)] private float sensitivity = 1;
        
        public float Speed => speed;
        public float JumpForce => jumpForce;
        public float MovementAcceleration => movementAcceleration;
        public float MovementFriction => movementFriction;

        public float Sensitivity => sensitivity;
    }
}