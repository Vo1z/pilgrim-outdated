using System;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Ingame.Player
{ 
    public class PlayerInputReceiver : MonoBehaviour
    {
        [Inject]private StationaryInputSystem _inputSystem;
        
        private InputAction _movementX;
        private InputAction _movementY;
        private InputAction _deltaRotationX;
        private InputAction _deltaRotationY;
        private InputAction _jump;
        private InputAction _crouch;

        public const float ANGLE_FOR_ONE_SCREEN_PIXEL = .1f;
        
        public event Action<Vector2> OnMovementInputReceived;
        public event Action<Vector2> OnRotationDeltaInputReceived;
        public event Action OnJumpInputReceived;
        public event Action<bool> OnCrouchInputReceived;

        private void Awake()
        {
            _movementX = _inputSystem.FPS.MovementX;
            _movementY = _inputSystem.FPS.MovementY;
            
            _deltaRotationX = _inputSystem.FPS.RotationX;
            _deltaRotationY = _inputSystem.FPS.RotationY;
            
            _jump = _inputSystem.FPS.Jump;
            _crouch = _inputSystem.FPS.Crouch;
        }
        

        private void Update()
        {
            Vector2 movementInput = Vector2.zero;
            Vector2 rotationDeltaInput = Vector2.zero;

            movementInput.x = _movementX.ReadValue<float>();
            movementInput.y = _movementY.ReadValue<float>();

            rotationDeltaInput.x = _deltaRotationX.ReadValue<float>();
            rotationDeltaInput.y = _deltaRotationY.ReadValue<float>();
            
            ReceiveMovementInput(movementInput);
            ReceiveDeltaRotationInput(rotationDeltaInput);
            
            if(_jump.WasPerformedThisFrame())
                ReceiveJumpInput();
            
            ReceiveCrouchInput(_crouch.ReadValue<float>() > 0);
        }

        private void ReceiveMovementInput(Vector2 movementInput)
        {
            OnMovementInputReceived?.Invoke(movementInput);
        }

        private void ReceiveDeltaRotationInput(Vector2 deltaRotationInput)
        {
            OnRotationDeltaInputReceived?.Invoke(deltaRotationInput);
        }

        private void ReceiveJumpInput()
        {
            OnJumpInputReceived?.Invoke();
        }

        private void ReceiveCrouchInput(bool isCrouching)
        {
            OnCrouchInputReceived?.Invoke(isCrouching);
        }
    }
}