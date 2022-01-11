using System;
using UnityEngine;
using Zenject;

namespace Ingame.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [Inject]private PlayerData _playerData;
        [Inject]private PlayerInputReceiver _playerInputReceiver;
        [Inject(Id="HudParent")] private Transform _hudParent;

        private const float GRAVITATIONAL_FORCE_WHEN_GROUNDED = 2f;
        
        private CharacterController _characterController;
        private float _initialCharacterHeight;

        private float _hudLocalRotationX = 0f;
        private float _currentSpeed;
        private Vector3 _velocity;
        private float _lastTimeJumpWasPerformed;
        private bool _isGrounded = false;

        private bool IsAbleToJump => Time.time - _lastTimeJumpWasPerformed > _playerData.PauseBetweenJumps;

        public event Action<Vector3> OnMovementPerformed;
        public event Action<Vector2> OnRotationPerformed;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _initialCharacterHeight = _characterController.height;
            _currentSpeed = _playerData.WalkSpeed;

            _playerInputReceiver.OnMovementInputReceived += Move;
            _playerInputReceiver.OnJumpInputReceived += Jump;
            _playerInputReceiver.OnCrouchInputReceived += Crouch;
            _playerInputReceiver.OnRotationDeltaInputReceived += Rotate;
            
            //todo move to the control settings
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDestroy()
        {
            _playerInputReceiver.OnMovementInputReceived -= Move;
            _playerInputReceiver.OnJumpInputReceived -= Jump;
            _playerInputReceiver.OnCrouchInputReceived -= Crouch;
            _playerInputReceiver.OnRotationDeltaInputReceived -= Rotate;
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
        
        private void FixedUpdate()
        {
            ApplyGravity();
            ApplyFriction();
            
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }

        private void ApplyGravity()
        {
            float gravityY = 0;
            
            gravityY = _characterController.isGrounded ? 
                Mathf.Clamp(_velocity.y - _playerData.GravityAcceleration, -GRAVITATIONAL_FORCE_WHEN_GROUNDED, Mathf.Infinity) : 
                Mathf.Clamp(_velocity.y - _playerData.GravityAcceleration, -_playerData.MaximumGravitationForce, Mathf.Infinity);
            
            _velocity.y = gravityY;
        }
        
        
        private void ApplyFriction()
        {
            var velocityCopy = _velocity;
            var horizontalZeroVector = Vector3.zero;
            var friction = _playerData.MovementFriction * Time.fixedDeltaTime;
            
            _velocity = Vector3.Lerp(velocityCopy, horizontalZeroVector, friction);
            _velocity = new Vector3(_velocity.x, velocityCopy.y, _velocity.z);
        }

        private void Move(Vector2 direction)
        {
            var movingOffset = transform.forward * direction.y + transform.right * direction.x;
            movingOffset *= _playerData.MovementAcceleration;
            movingOffset *= Time.fixedDeltaTime;
            var initialVelocity = _velocity;
            var nextVelocity = initialVelocity + movingOffset;
            nextVelocity = Vector3.ClampMagnitude(nextVelocity, _currentSpeed);
            nextVelocity.y = initialVelocity.y;
            
            _velocity = nextVelocity;

            OnMovementPerformed?.Invoke(new Vector3(direction.x * _currentSpeed, _velocity.y, direction.y * _currentSpeed));
        }

        private void Rotate(Vector2 direction)
        {
            var yRotation = direction.y * _playerData.Sensitivity * Time.fixedDeltaTime;
            var xRotation = direction.x * _playerData.Sensitivity * Time.fixedDeltaTime;

            _hudLocalRotationX -= yRotation;
            _hudLocalRotationX = Mathf.Clamp(_hudLocalRotationX, -90, 90);

            transform.Rotate(Vector3.up * xRotation);
            _hudParent.transform.localRotation = Quaternion.Euler(_hudLocalRotationX, 0, 0);
            
            OnRotationPerformed?.Invoke(direction);
        }
        
        private void Crouch(bool isCrouching)
        {
            var characterHeightOffset = isCrouching ? 
                -_playerData.EnterCrouchStateSpeed * Time.fixedDeltaTime:
                _playerData.EnterCrouchStateSpeed * Time.fixedDeltaTime;

            _currentSpeed = isCrouching ? _playerData.CrouchWalkSpeed : _playerData.WalkSpeed;

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