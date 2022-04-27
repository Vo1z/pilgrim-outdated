using Ingame.Movement;
using Ingame.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Health
{
    public class DestroyDeadActorsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, TransformModel, DeathTag>.Exclude<PlayerModel> _deadActorsFilter;

        public void Run()
        {
            foreach (var i in _deadActorsFilter)
            {
                ref var entity = ref _deadActorsFilter.GetEntity(i);
                ref var transformModel = ref _deadActorsFilter.Get2(i);

                Object.Destroy(transformModel.transform.gameObject);
                entity.Destroy();
            }
        }
    }
}