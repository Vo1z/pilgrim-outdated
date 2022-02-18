using DG.Tweening;
using Ingame.Enemy.State;
using Ingame.Health;
using Ingame.Movement;
using Leopotam.Ecs;
using Support;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Ingame.Enemy.System {
    sealed class AttackSystem : IEcsRunSystem {
        private readonly EcsFilter<LocateTargetComponent,ShootingModel,TransformModel,EnemyMovementComponent,ReactionDelayModel,AttackStateTag> _enemyFilter;
        
        void IEcsRunSystem.Run ()
        {
            foreach (var i in _enemyFilter)
            {
                ref var entity = ref _enemyFilter.GetEntity(i);
                ref var target = ref _enemyFilter.Get1(i);
                ref var shooting = ref _enemyFilter.Get2(i);
                ref var transformModel = ref _enemyFilter.Get3(i);
                ref var movement = ref entity.Get<EnemyMovementComponent>();
                ref var reactionDelay = ref entity.Get<ReactionDelayModel>();
                movement.NavMeshAgent.isStopped = true; 
                
                var delay = reactionDelay.ReactionTimeData.ReactionDelayFlat+Random.Range(-reactionDelay.ReactionTimeData.ReactionDelayRandom,reactionDelay.ReactionTimeData.ReactionDelayRandom);
               
                transformModel.transform.DOLookAt(target.Target.position,delay);
                if (entity.Has<ShootingBlockComponent>())
                {
                    //shooting cooldown
                    ref var block = ref entity.Get<ShootingBlockComponent>();
                    block.BlockTime += Time.deltaTime;
                    var res = block.BlockTime >= shooting.ShootingData.Timer;
                    if (!res)
                    {
                        return;
                    }
                    entity.Del<ShootingBlockComponent>();
                }
                else
                {
                    //random Vector
                    var randY =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.up;
                    var randX =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.forward;
                    var randZ =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.left;
                    var rand = randX + randY + randZ;
                    //shooting system
                    RaycastHit hit;
                    var position = transformModel.transform.position;
                    Debug.DrawLine(position, position+ transformModel.transform.forward*shooting.ShootingData.Distance+rand);
                    if (Physics.Linecast(transformModel.transform.position,transformModel.transform.position+ transformModel.transform.forward*shooting.ShootingData.Distance+rand,out hit))
                    {
                        var res = hit.collider.gameObject.TryGetComponent(out EntityReference entityReference);
                        if (!res)
                        {
                            hit.collider.gameObject.AddComponent<EntityReferenceRequestProvider>();
                            entityReference = hit.collider.gameObject.GetComponent<EntityReference>();
                        }

                        ref var entityOfTarget = ref entityReference.Entity;
                        if (entityOfTarget.Has<HealthComponent>())
                        {
                            ref var damageComponent =ref entityOfTarget.Get<DamageComponent>();
                            damageComponent.damageToDeal = shooting.ShootingData.Damage;
                        }
                    }
                    entity.Get<ShootingBlockComponent>();
                }
                
            }
        }
    }
}