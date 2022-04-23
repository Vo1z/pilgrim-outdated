using Leopotam.Ecs;
using Support;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ingame.Input
{
    public sealed class StationaryInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private StationaryInput _stationaryInputSystem;
        
        private float timePassedFromLastLeanInputRequest;

        private InputAction _movementInputX;
        private InputAction _movementInputY;
        private InputAction _rotationInputX;
        private InputAction _rotationInputY;
        private InputAction _jumpInput;
        private InputAction _crouchInput;
        private InputAction _leanInput;
        private InputAction _shootInput;
        private InputAction _aimInput;
        private InputAction _reloadInput;
        private InputAction _distortTheShutterInput;
        private InputAction _interactionInput;
        private InputAction _openInventoryInput;
        private InputAction _firstSlotInteraction;
        private InputAction _secondSlotInteraction;

        public void Init()
        {
            _movementInputX = _stationaryInputSystem.FPS.MovementX;
            _movementInputY = _stationaryInputSystem.FPS.MovementY;
            
            _rotationInputX = _stationaryInputSystem.FPS.RotationX;
            _rotationInputY = _stationaryInputSystem.FPS.RotationY;
            
            _jumpInput = _stationaryInputSystem.FPS.Jump;
            _crouchInput = _stationaryInputSystem.FPS.Crouch;
            _leanInput = _stationaryInputSystem.FPS.Lean;

            _shootInput = _stationaryInputSystem.FPS.Shoot;
            _aimInput = _stationaryInputSystem.FPS.Aim;

            _reloadInput = _stationaryInputSystem.FPS.Reload;
            _distortTheShutterInput = _stationaryInputSystem.FPS.DistortTheShutter;

            _interactionInput = _stationaryInputSystem.FPS.Interact;

            _openInventoryInput = _stationaryInputSystem.FPS.OpenInventory;

            _firstSlotInteraction = _stationaryInputSystem.FPS.FirstSlotInteraction;
            _secondSlotInteraction = _stationaryInputSystem.FPS.SecondSlotInteraction;
        }

        public void Run()
        {
            var movementInputVector = new Vector2(_movementInputX.ReadValue<float>(), _movementInputY.ReadValue<float>());
            var rotationInputVector = new Vector2(_rotationInputX.ReadValue<float>(), _rotationInputY.ReadValue<float>());
            bool jumpInput = _jumpInput.ReadValue<float>() > 0;
            bool crouchInput = _crouchInput.WasPressedThisFrame();
            bool shootInput = _shootInput.IsPressed();
            bool aimInput = _aimInput.WasPressedThisFrame();
            bool reloadInput = _reloadInput.WasPressedThisFrame();
            bool distortTheShutterInput = _distortTheShutterInput.WasPressedThisFrame();
            bool interactInput = _interactionInput.WasPressedThisFrame();
            bool openInventoryInput = _openInventoryInput.WasPressedThisFrame();
            bool interactWithFirstSlot = _firstSlotInteraction.WasPressedThisFrame();
            bool interactWithSecondSlot = _secondSlotInteraction.WasPressedThisFrame();

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

            if (leanDirection != LeanDirection.None && _leanInput.WasPressedThisFrame())
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<LeanInputRequest>().leanDirection = leanDirection;
            }

            if (shootInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<ShootInputEvent>();
            }

            if (aimInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<AimInputEvent>();
            }

            if (reloadInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<ReloadInputEvent>();
            }

            if (distortTheShutterInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<DistortTheShutterInputEvent>();
            }

            if (interactInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<InteractInputEvent>();
            }

            if (openInventoryInput)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<OpenInventoryInputEvent>();
            }

            if (interactWithFirstSlot)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<InteractWithFirstSlotInputEvent>();
            }
            
            if (interactWithSecondSlot)
            {
                if (inputEntity == EcsEntity.Null)
                    inputEntity = _world.NewEntity();

                inputEntity.Get<InteractWithSecondSlotInputEvent>();
            }
        }
    }
}