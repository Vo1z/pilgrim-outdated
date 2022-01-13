using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerModel, VelocityComponent, CharacterControllerModel, TimerComponent> _playerFilter;
        private readonly EcsFilter<JumpEvent> _jumpFilter;

        public void Run()
        {
            if(_jumpFilter.IsEmpty())
                return;
            
            foreach (var i in _playerFilter)
            {
                ref var playerModel = ref _playerFilter.Get1(i);
                ref var playerVelocityComp = ref _playerFilter.Get2(i);
                ref var characterControllerModel = ref _playerFilter.Get3(i);
                ref var playerJumpTimer = ref _playerFilter.Get4(i);

                var playerData = playerModel.playerData;
                
                if(!characterControllerModel.CharacterController.isGrounded || playerJumpTimer.timePassed < playerData.PauseBetweenJumps)
                    return;

                playerJumpTimer.timePassed = 0;
                playerVelocityComp.velocity += Vector3.up * playerModel.playerData.JumpForce;
            }
        }
    }
}