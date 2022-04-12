using System;
using DG.Tweening;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
 
namespace Ingame.Enemy.System
{
    public sealed class HideSystem: IEcsRunSystem
    {
        //find cover
        private EcsFilter<EnemyMovementComponent,HideModel,FindCoverStateTag> _filter;
        private EcsFilter<EnemyMovementComponent,HideModel,HideStateTag> _hideFilter;
        private EcsFilter<EnemyMovementComponent,HideModel,PeekStateTag> _peekFilter;
        public void Run()
        {
            //find a cover
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var movement = ref _filter.Get1(i);
                ref var obstacle = ref _filter.Get2(i);

                movement.NavMeshAgent.destination = obstacle.Obstacle.position;
                if ((movement.NavMeshAgent.remainingDistance <= movement.NavMeshAgent.stoppingDistance && !movement.NavMeshAgent.pathPending))
                {
                    if (entity.Has<HideBlockTag>())
                    {
                        entity.Del<HideBlockTag>();
                    }
                }
                
                movement.NavMeshAgent.autoRepath = true;
                movement.NavMeshAgent.isStopped = false;
            }
            //hide/take cover
            foreach (var i in _hideFilter)
            {
                ref var entity = ref _hideFilter.GetEntity(i);
                ref var transformModel = ref entity.Get<TransformModel>();
                ref var target = ref entity.Get<LocateTargetComponent>();
                if (entity.Has<HitboxModel>())
                {
                    ref var hitbox = ref entity.Get<HitboxModel>();
                    hitbox.Hitbox.height = hitbox.HitboxData.CrouchHeight;
                }
                transformModel.transform.DOLookAt(target.Target.position, 0.1f);
            }
            //peek from cover
            foreach (var i in _peekFilter)
            {
                ref var entity = ref _peekFilter.GetEntity(i);
                if (entity.Has<HitboxModel>())
                {
                    ref var hitbox = ref entity.Get<HitboxModel>();
                    hitbox.Hitbox.height = hitbox.HitboxData.NormalHeight;
                }
            }
        }
    }
}