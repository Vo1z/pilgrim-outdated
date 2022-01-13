using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerMovementSystem : IEcsRunSystem
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
                ref var playerVelocityComponent = ref _playerInputFilter.Get2(i);
                ref var playerCharacterControllerModel = ref _playerInputFilter.Get3(i);

                var playerData = playerModel.playerData;
                var playerVelocity = playerVelocityComponent.velocity;
                var playerTransform = playerCharacterControllerModel.CharacterController.transform;
                var movementPower = playerData.MovementAcceleration * Time.fixedDeltaTime;
                var movementDirection = playerTransform.forward * inputVector.y + 
                                        playerTransform.right * inputVector.x;

                var targetVelocity = movementDirection.normalized * playerData.WalkSpeed;//todo replace with state speed
                targetVelocity.y = playerVelocity.y;


                playerVelocityComponent.velocity = Vector3.Lerp(playerVelocity, targetVelocity, movementPower);
            }
        }
    }
}