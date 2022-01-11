using Ingame.PlayerLegacy;
using Leopotam.Ecs;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ingame
{
    public sealed class StationaryInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private Support.StationaryInputSystem _stationaryInputSystem;
        private readonly EcsFilter<StationaryInputComponent> _playerInputFilter;

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
            if(_playerInputFilter.IsEmpty())
                return;

            var movementInput = new Vector2(_movementInputX.ReadValue<float>(), _movementInputY.ReadValue<float>());
            var rotationInput = new Vector2(_rotationInputX.ReadValue<float>(), _rotationInputY.ReadValue<float>());
            bool jumpInput = _jumpInput.ReadValue<float>() > 0;
            bool crouchInput = _crouchInput.ReadValue<float>() > 0;
            var leanPosition = _leanInput.ReadValue<float>() switch
            {
                < 0 => LeanDirection.Left,
                > 0 => LeanDirection.Right,
                _ => LeanDirection.None
            }; 
            
            foreach (var i in _playerInputFilter)
            {
                ref var playerEntity = ref _playerInputFilter.GetEntity(i);
                ref var playerInputComp = ref _playerInputFilter.Get1(i);

                playerInputComp.movementInput = movementInput;
                playerInputComp.rotationInput = rotationInput;
                
                if (jumpInput)
                    playerEntity.Get<JumpEvent>();

                if (crouchInput)
                    playerEntity.Get<CrouchEvent>();
            }
        }
    }
}