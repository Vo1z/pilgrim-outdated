using DG.Tweening;
using Ingame.Enemy.State;
using Ingame.Health;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Ingame.Enemy.System {
    public sealed class AttackSystem : IEcsRunSystem {
        private readonly EcsFilter<LocateTargetComponent,ShootingModel,TransformModel,ReactionDelayModel,AttackStateTag> _enemyFilter;
        private readonly EcsFilter<ShootingModel,ContinuousAttackTimerRequest> _attackTimerFilter;
        
        void IEcsRunSystem.Run ()
        {
            foreach (var i in _enemyFilter)
            {
                ref var entity = ref _enemyFilter.GetEntity(i);
                ref var target = ref _enemyFilter.Get1(i);
                ref var shooting = ref _enemyFilter.Get2(i);
                ref var transformModel = ref _enemyFilter.Get3(i);
                ref var reactionDelay = ref entity.Get<ReactionDelayModel>();


                var delay = reactionDelay.ReactionTimeData.ReactionDelayFlat+Random.Range(-reactionDelay.ReactionTimeData.ReactionDelayRandom,reactionDelay.ReactionTimeData.ReactionDelayRandom);
                transformModel.transform.DOLookAt(target.Target.position,delay);
                
                if (shooting.CurrentAmountOfAmmunition<=0)
                {
                    continue;
                }
                if (entity.Has<ShootingBlockComponent>())
                {
                    //shooting cooldown
                    ref var block = ref entity.Get<ShootingBlockComponent>();
                    block.BlockTime += Time.deltaTime;
                    var res = block.BlockTime >= shooting.ShootingData.Timer;
                    if (!res)
                    {
                        continue;
                    }
                    entity.Del<ShootingBlockComponent>();
                }
                else
                {entity.Get<ShootingBlockComponent>();
                    //random Vector
                    var randY =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.up;
                    var randX =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.forward;
                    var randZ =  Random.Range(-shooting.ShootingData.Accuracy, shooting.ShootingData.Accuracy)*Vector3.left;
                    var rand = randX + randY + randZ;
                    //shooting system
                    RaycastHit hit;

                    shooting.CurrentAmountOfAmmunition -= 1;
                    //get head
                    ref var binder = ref entity.Get<VisionBinderComponent>();
                    binder.FarRange.TryGetComponent(out EntityReference farEntityReference);
                    ref var farRef = ref farEntityReference.Entity;
                    ref var vision = ref farRef.Get<VisionModel>();
                    var headPos = vision.Vision.HeadPosition;
                    
#if  UNITY_EDITOR
                    UnityEngine.Debug.DrawLine(transformModel.transform.position+headPos*Vector3.up,target.Target.position +rand);
#endif
                    //shooting
                    if (Physics.Linecast(transformModel.transform.position+headPos*Vector3.up,target.Target.position +rand,out hit))
                    {
                        if (Vector3.Distance(transformModel.transform.position,target.Target.position) > shooting.ShootingData.Distance)
                        {
                            continue;
                        }
                        var res = hit.collider.gameObject.TryGetComponent(out EntityReference entityReference);
                        
                        if (!res)
                        {
                            continue;
                        }

                        if (entityReference.transform == transformModel.transform)
                        {
                            continue;
                        }
                        ref var entityOfTarget = ref entityReference.Entity;
                        if (entityOfTarget.Has<HealthComponent>())
                        {
                            ref var damageComponent = ref entityOfTarget.Get<DamageComponent>();
                            damageComponent.damageToDeal = shooting.ShootingData.Damage;
                          
                        }
                    }
                }
            }

            foreach (var i in _attackTimerFilter)
            {
                ref var entity = ref _attackTimerFilter.GetEntity(i);
                ref var shooting = ref _attackTimerFilter.Get1(i);
                ref var timer = ref _attackTimerFilter.Get2(i);

                timer.TimeLeft += Time.deltaTime;
                if (timer.TimeLeft >= shooting.ShootingData.ContinuousAttackDuration )
                {
                    entity.Del<ContinuousAttackTimerRequest>();
                }
            }
        }
    }
}