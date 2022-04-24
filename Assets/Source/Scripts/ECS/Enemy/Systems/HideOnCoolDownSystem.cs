using System;
using Leopotam.Ecs;
using UnityEngine;
 

namespace Ingame.Enemy.System
{
    public sealed class HideOnCoolDownSystem : IEcsRunSystem
    {
        private EcsFilter<HideModel,HideBlockOnPeekComponent> _peekFilter;
        private EcsFilter<HideModel,HideBlockOnTakeCoverComponent> _takeCoverFilter;
        private EcsFilter<HideModel,DynamicHideCooldownComponent> _dynamicHideFilter;
        public void Run()
        {
            //Peek
            foreach (var i in _peekFilter)
            {
                ref var entity = ref _peekFilter.GetEntity(i);
                ref var hideModel = ref _peekFilter.Get1(i);
                ref var hideBlock = ref _peekFilter.Get2(i);

                if (hideBlock.RemainingTime>=hideModel.HideData.CoolDownOnPeek)
                {
                    entity.Del<HideBlockOnPeekComponent>();
                    continue;
                }

                hideBlock.RemainingTime += Time.deltaTime;
            }
            //Take a cover
            foreach (var i in _takeCoverFilter)
            {
                ref var entity = ref _takeCoverFilter.GetEntity(i);
                ref var hideModel = ref _takeCoverFilter.Get1(i);
                ref var hideBlock = ref _takeCoverFilter.Get2(i);

                if (hideBlock.RemainingTime>=hideModel.HideData.CoolDownOnTakeCover)
                {
                    entity.Del<HideBlockOnTakeCoverComponent>();
                    continue;
                }

                hideBlock.RemainingTime += Time.deltaTime;
            }
        }
    }
}