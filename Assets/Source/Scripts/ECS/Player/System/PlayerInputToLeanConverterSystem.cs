using System;
using Ingame.PlayerLegacy;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class PlayerInputToLeanConverterSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerModel> _playerFilter;
        private EcsFilter<HudLeanOriginTag> _leanOriginFilter;
        private EcsFilter<LeanInputRequest> _leanInputRequestFilter;

        public void Run()
        {
            if(_leanInputRequestFilter.IsEmpty() || _playerFilter.IsEmpty())
                return;

            ref var playerModel = ref _playerFilter.Get1(0);
            var playerData = playerModel.playerData;
            var leanDirectionInput = _leanInputRequestFilter.Get1(0).leanDirection;
            
            playerModel.currentLeanDirection = playerModel.currentLeanDirection == leanDirectionInput ?
                LeanDirection.None : 
                leanDirectionInput;

            foreach (var i in _leanOriginFilter)
            {
                ref var leanOriginEntity = ref _leanOriginFilter.GetEntity(i);
                ref var leanReq = ref leanOriginEntity.Get<LeanRequest>();

                leanReq.rotationAxis = Vector3.forward;
                leanReq.angle = playerModel.currentLeanDirection switch
                {
                    LeanDirection.Left => playerData.LeanAngleOffset,
                    LeanDirection.Right => -playerData.LeanAngleOffset,
                    _ => 0
                };
                //todo remove hardcode
                leanReq.speed = 5f;
            }
        }
    }
}