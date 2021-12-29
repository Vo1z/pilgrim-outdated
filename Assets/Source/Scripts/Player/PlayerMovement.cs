using Support;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Ingame.Player
{ 
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        private StationaryInputSystem _inputSystem;
        private PlayerData _playerData;
        
        private Vector2 _inputMovementVector;
        private Vector2 _inputMouseDelta;
        private bool _inputIsJumping;
        
        [Inject]
        private void Construct(StationaryInputSystem inputSystem, PlayerData playerData)
        {
            _inputSystem = inputSystem;
            _playerData = playerData;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _inputSystem.FPS.MovementX.performed += UpdateInputMovementX;
            _inputSystem.FPS.MovementY.performed += UpdateInputMovementY;
            
            _inputSystem.FPS.RotationX.performed += UpdateInputMouseDeltaX;
            _inputSystem.FPS.RotationY.performed += UpdateInputMouseDeltaY;
            
            _inputSystem.FPS.Jump.performed += UpdateInputJump;
        }

        private void OnDestroy()
        {
            _inputSystem.FPS.MovementX.performed -= UpdateInputMovementX;
            _inputSystem.FPS.MovementY.performed -= UpdateInputMovementY;
            
            _inputSystem.FPS.RotationX.performed -= UpdateInputMouseDeltaX;
            _inputSystem.FPS.RotationY.performed -= UpdateInputMouseDeltaY;
            
            _inputSystem.FPS.Jump.performed -= UpdateInputJump;
        }

        private void Update()
        {
            Move();
            RotateBody();
            Jump();
            ApplyForces();
        }
        
        private void UpdateInputMovementX(InputAction.CallbackContext ctx) => _inputMovementVector.x = ctx.ReadValue<float>();
        
        private void UpdateInputMovementY(InputAction.CallbackContext ctx) => _inputMovementVector.y = ctx.ReadValue<float>();
        
        private void UpdateInputMouseDeltaX(InputAction.CallbackContext ctx) => _inputMouseDelta.x = ctx.ReadValue<float>();
        
        private void UpdateInputMouseDeltaY(InputAction.CallbackContext ctx) => _inputMouseDelta.y = ctx.ReadValue<float>();
        
        private void UpdateInputJump(InputAction.CallbackContext ctx) => _inputIsJumping = ctx.ReadValue<float>() > 0;

        private void Move()
        {
            if(_inputMovementVector.sqrMagnitude < .0001f)
                return;

            var movementVector = transform.forward * _inputMovementVector.y + transform.right * _inputMovementVector.x;
            movementVector *= _playerData.Speed;
            movementVector *= Time.deltaTime;

            _characterController.Move(movementVector);
        }

        private void RotateBody()
        {
            
        }

        private void ApplyForces()
        {
            var gravity = Physics.gravity * _playerData.Mass;
            gravity *= Time.deltaTime;

            _characterController.Move(gravity);
        }

        private void Jump()
        {
            if(!_inputIsJumping)
                return;
        }

    }
}