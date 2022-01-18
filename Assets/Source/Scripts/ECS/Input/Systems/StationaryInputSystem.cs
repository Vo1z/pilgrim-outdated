using Ingame.PlayerLegacy;
using Leopotam.Ecs;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ingame
{
    public sealed class StationaryInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private StationaryInput _stationaryInputSystem;

        private InputAction _movementInputX;
        private InputAction _movementInputY;
        private InputAction _rotationInputX;
        private InputAction _rotationInputY;
        private InputAction _jumpInput;
        private InputAction _crouchInput;
        private InputAction _leanInput;

        public void Init()
        {
            _movementInputX = _stationaryInputSystem.FPS.MovementX;
            _movementInputY = _stationaryInputSystem.FPS.MovementY;
            
            _rotationInputX = _stationaryInputSystem.FPS.RotationX;
            _rotationInputY = _stationaryInputSystem.FPS.RotationY;
            
            _jumpInput = _stationaryInputSystem.FPS.Jump;
            _crouchInput = _stationaryInputSystem.FPS.Crouch;
            _leanInput = _stationaryInputSystem.FPS.Lean;
        }

        public void Run()
        {
            var movementInputVector = new Vector2(_movementInputX.ReadValue<float>(), _movementInputY.ReadValue<float>());
            var rotationInputVector = new Vector2(_rotationInputX.ReadValue<float>(), _rotationInputY.ReadValue<float>());
            bool jumpInput = _jumpInput.ReadValue<float>() > 0;
            bool crouchInput = _crouchInput.WasPressedThisFrame();
            var leanDirection = _leanInput.ReadValue<float>() switch
            {
                < 0 => LeanDirection.Left,
                > 0 => LeanDirection.Right,
                _ => LeanDirection.None
            };
            
            EcsEntity inputEntity = EcsEntity.Null;
            if (movementInputVector.sqrMagnitude > 0)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();
                
                inputEntity.Get<MoveInputRequest>().movementInput = movementInputVector;
            }
            
            if (rotationInputVector.sqrMagnitude > 0)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();
                
                inputEntity.Get<RotateInputRequest>().rotationInput = rotationInputVector;
            }

            if (jumpInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();
                
                inputEntity.Get<JumpInputEvent>();
            }

            if (crouchInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();
                
                inputEntity.Get<CrouchInputEvent>();
            }

            if (leanDirection != LeanDirection.None)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();
                inputEntity.Get<LeanDirection>();
            }
        }
    }
}