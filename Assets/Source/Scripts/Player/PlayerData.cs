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
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float pauseBetweenJumps = .5f;
        [BoxGroup("Movement")]
        [SerializeField][Min(0)] private float enterCrouchStateSpeed = .5f;
        [BoxGroup("Gravitation")]
        [SerializeField][Min(0)] private float gravityAcceleration = 1;
        [BoxGroup("Gravitation")]
        [SerializeField][Min(0)] private float maximumGravitationForce = 10;
        [BoxGroup("Gravitation")] 
        [SerializeField] [Min(0)] private float sensitivity = 1;
        [BoxGroup("Gravitation")]
        [SerializeField] [Range(0, 1)] private float slidingForceModifier = .5f;
        
        public float Speed => speed;
        public float MovementAcceleration => movementAcceleration;
        public float MovementFriction => movementFriction;
        public float JumpForce => jumpForce;
        public float PauseBetweenJumps => pauseBetweenJumps;
        public float EnterCrouchStateSpeed => enterCrouchStateSpeed;
        
        public float GravityAcceleration => gravityAcceleration;
        public float MaximumGravitationForce => maximumGravitationForce;
        public float SlidingForceModifier => slidingForceModifier;

        public float Sensitivity => sensitivity;
    }
}