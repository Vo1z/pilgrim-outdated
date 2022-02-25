using System;
using System.Collections.Generic;
using System.Linq;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;
using System.Xml.Linq;
namespace Ingame.Enemy.System
{
    public class EnemySoldierBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<TransformModel,VisionModel,EnemySoldierBehaviourTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                
                ref var transformModel = ref entity.Get<TransformModel>();
                ref var vision = ref entity.Get<VisionModel>();
                ref var targetComponent = ref entity.Get<LocateTargetComponent>();
                ref var obstacle = ref entity.Get<HideBehindObstacleComponent>();
                PerformMainLogic(ref entity, ref transformModel, ref vision, ref targetComponent, ref obstacle);
            }
        }
        
        private void PerformMainLogic(ref EcsEntity entity, ref TransformModel transformModel, ref VisionModel vision,ref LocateTargetComponent target, ref HideBehindObstacleComponent obstacle)
            {
                PerformPatrolLogic(ref entity, ref transformModel, ref vision, ref target);
                PerformFollowLogic(ref entity, ref transformModel, ref vision, ref target);
                PerformAttackLogic(ref entity, ref transformModel,ref vision, ref target, ref obstacle);
                PerformHideLogic(ref entity);
                PerformFleeLogic(ref entity);
            }
                
        
            private void PerformPatrolLogic(ref EcsEntity entity,ref TransformModel transformModel,ref VisionModel vision, ref LocateTargetComponent targetComponent)
            {
                if (!entity.Has<PatrolStateTag>())
                {
                    return;
                }
                //Attack State
                foreach (var item in vision.Opponents)
                {
                    Vector3 direction = item.transform.position- transformModel.transform.position;
                    direction.y = 0;
                    float deltaAngle = Vector3.Angle(direction, transformModel.transform.forward);
                    if (deltaAngle>vision.Vision.Angle || deltaAngle<0)
                    {
                        continue;
                    }
                    var originY = transformModel.transform.position.y + vision.Vision.Height / 2;
                    var origin = new Vector3(transformModel.transform.position.x,originY,transformModel.transform.position.z);
                    var dest = new Vector3(item.transform.position.x,originY,item.transform.position.z);
            
                    if (!Physics.Linecast(origin,dest,vision.Vision.Mask))
                    {
                        targetComponent.Target = item;
                        entity.Get<AttackStateTag>();
                        entity.Del<PatrolStateTag>();
                        return;
                    }
                }
            }
            
            private void PerformFollowLogic(ref EcsEntity entity, ref TransformModel transformModel,ref VisionModel vision, ref LocateTargetComponent targetComponent)
            {
                if (!entity.Has<FollowStateTag>())
                    return;
                if (vision.Vision.MaxDistance> (transformModel.transform.position-targetComponent.Target.position).magnitude)
                {
                    entity.Get<AttackStateTag>();
                    entity.Del<FollowStateTag>();   
                }
                if (vision.Vision.MaxDistance<Vector3.Magnitude(targetComponent.Target.position-transformModel.transform.position))
                {
                    entity.Get<PatrolStateTag>();
                    entity.Del<FollowStateTag>();   
                }
            }
            private void PerformAttackLogic(ref EcsEntity entity, ref TransformModel transformModel,ref VisionModel vision, ref LocateTargetComponent targetComponent, ref HideBehindObstacleComponent obstacleComponent)
            {
                if (!entity.Has<AttackStateTag>())
                    return;

                if (vision.Vision.Distance<Vector3.Magnitude(targetComponent.Target.position-transformModel.transform.position))
                {
                    entity.Get<FollowStateTag>();
                    entity.Del<AttackStateTag>();
                    return;
                }

                if (vision.Covers.Count==0)
                {
                    return;
                }

                Transform trans = vision.Covers.ElementAt(0);
                float dist = vision.Covers.ElementAt(0).transform.position.magnitude;
                for (int i = 1; i < vision.Covers.Count; i++)
                {
                    if (dist>vision.Covers.ElementAt(i).transform.position.magnitude)
                    {
                        dist = vision.Covers.ElementAt(0).transform.position.magnitude;
                        trans = vision.Covers.ElementAt(0).transform;
                    }
                }

                obstacleComponent.Obstacle = trans;
                entity.Get<HideStateTag>();
                entity.Del<AttackStateTag>();   
            }
            
            private void PerformHideLogic(ref EcsEntity entity)
            {
                if (!entity.Has<HideStateTag>())
                    return;
                
            }

            private void PerformFleeLogic(ref EcsEntity entity)
            {
                if (!entity.Has<FleeStateTag>())
                    return;

            }
    }
}