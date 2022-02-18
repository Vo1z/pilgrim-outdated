using DG.Tweening;
using Ingame.Enemy.State;
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
                

                /*RaycastHit rayHit = new RaycastHit();
                Ray ray = new Ray(transformModel.transform.position, target.Target.position - transformModel.transform.position);
                Debug.DrawRay(transformModel.transform.position,target.Target.position - transformModel.transform.position);
                if (Physics.Raycast(ray,out rayHit,1000,1))
                {
                    Debug.Log("DOdge");
                }
                else
                {
                    Debug.Log("hit");
                }*/
                var delay = reactionDelay.ReactionTimeData.ReactionDelayFlat+Random.Range(-reactionDelay.ReactionTimeData.ReactionDelayRandom,reactionDelay.ReactionTimeData.ReactionDelayRandom);
               
                transformModel.transform.DOLookAt(target.Target.position,delay);
                //transformModel.transform.rotation = Quaternion.Euler(Mathf.Clamp(transformModel.transform.localRotation.x,-15,15),transformModel.transform.rotation.y,0);


                if (entity.Has<ShootingBlockComponent>())
                {
                    //shooting cooldown
                    ref var block = ref entity.Get<ShootingBlockComponent>();
                    block.BlockTime += Time.deltaTime;
                    var res = block.BlockTime >= shooting.Timer;
                    if (!res)
                    {
                        return;
                    }
                    entity.Del<ShootingBlockComponent>();
                }
                else
                {
                    //
                    //var randZ =  Random.Range(-shooting.Accuracy, shooting.Accuracy)*Vector3.right;
                    var randY =  Random.Range(-shooting.Accuracy, shooting.Accuracy)*Vector3.up;
                    var randX =  Random.Range(-shooting.Accuracy, shooting.Accuracy)*Vector3.forward;
                    var rand = randX + randY;
                    //shooting system
                    RaycastHit hit;
                    if (Physics.Raycast(transformModel.transform.position, transformModel.transform.forward, out hit, 1000.0F))
                    {
                        Debug.DrawRay(transformModel.transform.position, target.Target.position - transformModel.transform.position+rand);
                        if (hit.collider.tag == "Player")
                        {
                            TemplateUtils.SafeDebug("hit");
                        }
                        else
                        {
                            TemplateUtils.SafeDebug("Dodge");
                        }
                    }
                    entity.Get<ShootingBlockComponent>();
                }
                
            }
        }
    }
}