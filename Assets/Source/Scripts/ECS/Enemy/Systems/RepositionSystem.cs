using System;
using Ingame.Enemy.State;
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;


namespace Ingame.Enemy.System
{
    public sealed class RepositionSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyMovementComponent,LocateTargetComponent,TransformModel, RepositionStateTag> _filter;
        private readonly float _treshholdDistance = 25;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movement = ref _filter.Get1(i);
                ref var target = ref _filter.Get2(i);
                ref var transformModel = ref _filter.Get3(i);
                var movementData = movement.EnemyMovementData;

                var position = transformModel.transform.position;
                var distance = Vector3.Distance(target.Target.position, position);
                var destination = new Vector3(position.x, position.y, position.z);
                float x = 0.1f;
                var moveRightOrLeft = Math.Floor((double) Random.Range(0, 2))==0 ? 1 : -1;
                destination += transformModel.transform.forward*Random.Range(movementData.MinRangeOfOneStep,movementData.MaxRangeOfOneStep)*(distance<_treshholdDistance?-1:1)
                               + transformModel.transform.right*moveRightOrLeft*Random.Range(movementData.MinRangeOfOneStep,movementData.MaxRangeOfOneStep);
                movement.NavMeshAgent.speed = movementData.SpeedOnReposition;
                movement.NavMeshAgent.destination = destination;
                movement.NavMeshAgent.isStopped = false;
            }   
        }
    }
}