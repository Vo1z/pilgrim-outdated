using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Ingame.Enemy.System
{
    public class HideOnCoolDownSystem : IEcsRunSystem
    {
        private EcsFilter<HideModel,HideBlockComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var hideModel = ref _filter.Get1(i);
                ref var hideBlock = ref _filter.Get2(i);

                if (hideBlock.RemainingTime>=hideModel.HideData.CoolDown)
                {
                    entity.Del<HideBlockComponent>();
                    continue;
                }

                hideBlock.RemainingTime += Time.deltaTime;
            }
        }
    }
}