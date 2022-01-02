using UnityEngine;
using Zenject;

namespace Ingame.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
        [Inject]private PlayerData _playerData;
        [Inject]private PlayerInputReceiver _playerInputReceiver;
        
        private CharacterController _characterController;
        private float _initialCharacterHeight;

        private Vector3 _velocity;
        private float _lastTimeJumpWasPerformed;
        private bool _isGrounded = false;

        private bool IsAbleToJump => Time.time - _lastTimeJumpWasPerformed > _playerData.PauseBetweenJumps;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _initialCharacterHeight = _characterController.height;

            _playerInputReceiver.OnMovementInputReceived += Move;
            _playerInputReceiver.OnJumpInputReceived += Jump;
            _playerInputReceiver.OnCrouchInputReceived += Crouch;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnMovementInputReceived -= Move;
            _playerInputReceiver.OnJumpInputReceived -= Jump;
            _playerInputReceiver.OnCrouchInputReceived -= Crouch;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            var hitNormal = hit.normal;
            _isGrounded = Vector3.Angle(Vector3.up, hitNormal) <= _characterController.slopeLimit;

            if (!_isGrounded)
            {
                _velocity.x += (1f - hitNormal.y) * hitNormal.x * _playerData.SlidingForceModifier;
                _velocity.z += (1f - hitNormal.y) * hitNormal.z * _playerData.SlidingForceModifier;
            }
        }
        
        private void Update()
        {
            ApplyGravity();
            ApplyFriction();
            
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            float gravityY = 0;
            
            gravityY = _characterController.isGrounded ? 
                Mathf.Clamp(_velocity.y - _playerData.GravityAcceleration, 0, Mathf.Infinity) : 
                Mathf.Clamp(_velocity.y - _playerData.GravityAcceleration, -_playerData.MaximumGravitationForce, Mathf.Infinity);
            
            _velocity.y = gravityY;
        }
        
        
        private void ApplyFriction()
        {
            var velocityCopy = _velocity;
            var horizontalZeroVector = Vector3.zero;
            var friction = _playerData.MovementFriction * Time.deltaTime;
            
            _velocity = Vector3.Lerp(velocityCopy, horizontalZeroVector, friction);
            _velocity = new Vector3(_velocity.x, velocityCopy.y, _velocity.z);
        }

        private void Move(Vector2 direction)
        {
            var movingOffset = transform.forward * direction.y + transform.right * direction.x;
            movingOffset *= _playerData.MovementAcceleration;
            movingOffset *= Time.deltaTime;
            var initialVelocity = _velocity;
            var nextVelocity = initialVelocity + movingOffset;
            nextVelocity = Vector3.ClampMagnitude(nextVelocity, _playerData.Speed);
            nextVelocity.y = initialVelocity.y;
            
            _velocity = nextVelocity;
        }

        private void Crouch(bool isCrouching)
        {
            var characterHeightOffset = isCrouching ? 
                -_playerData.EnterCrouchStateSpeed * Time.fixedDeltaTime:
                _playerData.EnterCrouchStateSpeed * Time.fixedDeltaTime;

            _characterController.height += characterHeightOffset;
            _characterController.height = Mathf.Clamp(_characterController.height, _initialCharacterHeight / 2, _initialCharacterHeight);
        }

        private void Jump() 
        {
            if(!_isGrounded || !IsAbleToJump)
                return;
            
            var impulseVector = Vector3.up * _playerData.JumpForce;

            _lastTimeJumpWasPerformed = Time.time;
            _isGrounded = false;
            _velocity += impulseVector;
        }
    }
}