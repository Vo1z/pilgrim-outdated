using Ingame.Utilities;
using UnityEngine;
using Zenject;

namespace Ingame.Player
{ 
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PlayerMovementController : MonoBehaviour
    {
        [Inject]private PlayerData _playerData;
        [Inject]private GroundDetection _groundDetection;
        [Inject] private PlayerInputReceiver _playerInputReceiver;
        
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _playerInputReceiver.OnMovementInputReceived += Move;
            _playerInputReceiver.OnJumpInputReceived += Jump;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnMovementInputReceived -= Move;
            _playerInputReceiver.OnJumpInputReceived -= Jump;
        }

        private void Update()
        {
            var playerVelocity = _rigidbody.velocity;
            var horizontalZeroVector = Vector3.zero;
            var friction = _playerData.MovementFriction * Time.deltaTime;
            
            horizontalZeroVector.y = playerVelocity.y;
            _rigidbody.velocity = Vector3.Lerp(playerVelocity, horizontalZeroVector, friction);
        }

        private void Move(Vector2 direction)
        {
            var movingOffset = transform.forward * direction.y + transform.right * direction.x;
            movingOffset *= _playerData.MovementAcceleration;
            movingOffset *= Time.deltaTime;
            var nextVelocity = _rigidbody.velocity + movingOffset;
            nextVelocity = Vector3.ClampMagnitude(nextVelocity, _playerData.Speed);
            
            _rigidbody.velocity = nextVelocity;
        }

        private void Jump()
        {
            if(!_groundDetection.IsGrounded)
                return;
            
            var impulseVector = Vector3.up * _playerData.JumpForce;
            
            _rigidbody.AddForce(impulseVector, ForceMode.Impulse);
        }
    }
}