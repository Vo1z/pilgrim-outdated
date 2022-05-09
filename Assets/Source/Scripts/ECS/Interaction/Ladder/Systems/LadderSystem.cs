using System;
using System.Collections.Generic;
using Ingame.Enemy;
using Ingame.Enemy.System;
using Ingame.Input;
using Ingame.Interaction.Common;
using Ingame.Movement;
using Ingame.Player;
using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

namespace Ingame.Interaction.Ladder
{
    public sealed class LadderSystem : IEcsRunSystem
    {
        //Ladder
        private EcsFilter<InteractiveTag, PerformInteractionTag,TransformModel,LadderTag> _ladderFilter;
        private EcsFilter<InteractiveTag, PerformInteractionTag,TransformModel,FromTopLadderTag> _topLadderFilter;
        
        //Player starting/during interaction with ladder
        private EcsFilter<PlayerModel,CharacterControllerModel,TransformModel> _playerFilter;
        //Player finishing climbing
        private EcsFilter<PlayerModel,CharacterControllerModel,TransformModel,ActorLockOnLadderCallbackRequest> _playerLockOnLadder;
        //player up/down movement
        private EcsFilter<CharacterControllerModel, ActorOnLadderTag,TransformModel>.Exclude<ActorLockOnLadderCallbackRequest> _playerOnLadderFilter;
        
        //input
        private EcsFilter<MoveInputRequest> _inputFilter;
        private EcsFilter<OnTriggerEnterEvent> _enter;
        //Stop interaction with ladder
        private EcsFilter<ActorNotOnLadderCallbackRequest> _terminateTagCallbackFilter;
        
        private const int PLAYER_INDEX = 0;
        private const float STEP_RATE = 0.035f;
        private const float ENDING_= 1.25f;
        
        public void Run()
        {
            //Terminate Tag
            foreach (var i in _terminateTagCallbackFilter)
            {
                ref var terminateEntity = ref _terminateTagCallbackFilter.GetEntity(i);
                if (terminateEntity.Has<ActorOnLadderTag>())
                {
                    terminateEntity.Del<ActorOnLadderTag>();
                }
                else
                {
                    terminateEntity.Get<ActorOnLadderTag>();
                } 
                terminateEntity.Del<ActorNotOnLadderCallbackRequest>();
                terminateEntity.Del<ActorFromTopLadderTag>();
            }
            //Lock animations
            foreach (var i in _playerLockOnLadder)
            {
                ref var playerLockOnLadderEntity = ref _playerLockOnLadder.GetEntity(i);
                ref var playerModel = ref _playerLockOnLadder.Get1(i);
                ref var controller = ref _playerLockOnLadder.Get2(i);
                ref var transformModel = ref _playerLockOnLadder.Get3(i);
                ref var request = ref playerLockOnLadderEntity.Get<ActorLockOnLadderCallbackRequest>();

                if (request.Destinations.Count<=0)
                {
                    continue;
                }
                var waypoint = request.Destinations[request.Index];
                if (Math.Abs(waypoint.y - transformModel.transform.position.y) > 0.1)
                {
                    var dir = waypoint.y - transformModel.transform.position.y;
                    controller.characterController.Move((dir>0?1:-1) *Vector3.up*STEP_RATE*ENDING_);
                    continue;
                }
                if (Math.Abs(waypoint.x - transformModel.transform.position.x) > 0.1||Math.Abs(waypoint.z - transformModel.transform.position.z) > 0.1)
                {
                    var dir = waypoint - transformModel.transform.position;
                    dir = new Vector3(dir.x, 0, dir.z).normalized;
                    controller.characterController.Move(dir*STEP_RATE*ENDING_);
                    continue;
                }
                request.Index += 1;
                if (request.Destinations.Count > request.Index)
                {
                    continue;
                }

                playerLockOnLadderEntity.Get<ActorNotOnLadderCallbackRequest>();
                playerLockOnLadderEntity.Del<ActorLockOnLadderCallbackRequest>();
            }
            
            //player enter/exit 
            foreach (var i in _ladderFilter)
            {
                ref var ladderEntity = ref _ladderFilter.GetEntity(i);
                ref var playerEntity = ref _playerFilter.GetEntity(PLAYER_INDEX);
                if (playerEntity.Has<ActorOnLadderTag>())
                {
                    playerEntity.Del<ActorOnLadderTag>();
                }
                else
                {
                    playerEntity.Get<ActorOnLadderTag>();
                }
                
                ladderEntity.Del<PerformInteractionTag>();
            }
            //player from top to bottom
            foreach (var i in _topLadderFilter)
            {
                if (_playerFilter.IsEmpty()) continue;
                ref var playerController = ref _playerFilter.Get2(PLAYER_INDEX);
                ref var playerTransformModel = ref _playerFilter.Get3(PLAYER_INDEX);
                ref var transformModel = ref _topLadderFilter.Get3(i);
                //moving
                var waypoint1= transformModel.transform.GetChild(0).position;
                var waypoint2 = transformModel.transform.GetChild(1).position;
                var ladder = transformModel.transform.parent;

               ref var playerEntity =  ref _playerFilter.GetEntity(PLAYER_INDEX);
               playerEntity.Get<ActorFromTopLadderTag>();
               ref var actorLock = ref playerEntity.Get<ActorLockOnLadderCallbackRequest>();
               actorLock.Destinations = new List<Vector3>() {waypoint1, waypoint2};
               playerEntity.Get<ActorOnLadderTag>();
            }
            //player move up/down
            if (!_playerOnLadderFilter.IsEmpty()) {
                ref var controller = ref _playerOnLadderFilter.Get1(PLAYER_INDEX);
                ref var playerInput = ref _inputFilter.Get1(PLAYER_INDEX);
                var direction = (new Vector3(0, playerInput.movementInput.y, 0)).normalized; 
                controller.characterController.Move(direction*STEP_RATE);
            }
            
            // climb on the top
            foreach (var i in _enter)
            {
                ref var enter = ref _enter.Get1(i);
                if (!enter.senderGameObject.TryGetComponent(out EntityReference entityReference)) continue;
                //climbing from bottom to top
                if (entityReference.Entity.Has<LadderTag>()){
                    
                    //player
                    if (enter.collider.CompareTag("Player"))
                    {
                        if (!_playerOnLadderFilter.IsEmpty())
                        {
                            ref var playerEntity = ref _playerFilter.GetEntity(PLAYER_INDEX);
                            if (playerEntity.Has<ActorFromTopLadderTag>())
                            {
                                continue;
                            }
                            ref var request = ref _playerFilter.GetEntity(PLAYER_INDEX).Get<ActorLockOnLadderCallbackRequest>();
                            request.Destinations = new List<Vector3>()
                                {enter.senderGameObject.transform.GetChild(0).gameObject.transform.position};
                        }
                    }
                    //Enemy
                    //todo
                    //implement enemy climbing system
                    
                }
            }
        }
    }
}