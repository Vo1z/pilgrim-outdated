using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerInputToMovementCommunicationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, VelocityComponent, CharacterControllerModel> _playerInputFilter;
        private readonly EcsFilter<MoveRequest> _moveRequestFilter;
        
        public void Run()
        {
            if(_moveRequestFilter.IsEmpty())
                return;
            
            ref var moveRequest = ref _moveRequestFilter.Get1(0);
            var inputVector = moveRequest.movementInput;
            
            foreach (var i in _playerInputFilter)
            {
                ref var playerModel = ref _playerInputFilter.Get1(i);
                ref var playerVelocity = ref _playerInputFilter.Get2(i);
                ref var playerCharacterControllerModel = ref _playerInputFilter.Get3(i);

                var playerData = playerModel.playerData;
                var playerTransform = playerCharacterControllerModel.CharacterController.transform;
                var movingOffset = playerTransform.forward * inputVector.y + playerTransform.right * inputVector.x;
                movingOffset *= playerData.MovementAcceleration;
                movingOffset *= Time.fixedDeltaTime;
                
                var initialVelocity = playerVelocity.velocity;
                var nextVelocity = initialVelocity + movingOffset;
                nextVelocity = Vector3.ClampMagnitude(nextVelocity, playerData.WalkSpeed); //todo replace with player model
                nextVelocity.y = initialVelocity.y;
            
                playerVelocity.velocity = nextVelocity;
                
                // var playerData = playerModel.playerData;
                // var moveInputVector = moveRequest.movementInput;
                // var playerTransform = characterControllerModel.CharacterController.transform;
                //
                // var nextVelocity = playerTransform.forward * moveInputVector.y * playerData.MovementAcceleration +
                //                    playerTransform.right * moveInputVector.x * playerData.MovementAcceleration;
                //
                // nextVelocity *= Time.fixedDeltaTime;
                // nextVelocity.x += playerVelocity.velocity.x;
                // nextVelocity.z += playerVelocity.velocity.z;
                //
                // nextVelocity = Vector3.ClampMagnitude(nextVelocity, playerData.WalkSpeed);
                // // nextVelocity.x = Mathf.Clamp(nextVelocity.x, -playerData.WalkSpeed, playerData.WalkSpeed);
                // // nextVelocity.z = Mathf.Clamp(nextVelocity.z, -playerData.WalkSpeed, playerData.WalkSpeed);
                // nextVelocity.y = playerVelocity.velocity.y;
                //
                // playerVelocity.velocity = nextVelocity;
            }
        }
    }
}