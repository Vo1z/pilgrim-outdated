using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Behaviour
{
    public class BehaviourBinderSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BehaviourAgentModel,BehaviourBinderRequest> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                _filter.Get1(i).Tree.Entity = entity;
                entity.Get<BehaviourCheckerTag>();
                entity.Del<BehaviourBinderRequest>();
            }
        }
    }
}