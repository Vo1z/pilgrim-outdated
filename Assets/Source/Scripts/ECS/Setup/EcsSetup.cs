using System;
using LeoEcsPhysics;
using Leopotam.Ecs;
using Support;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame
{
    public sealed class EcsSetup : MonoBehaviour
    {
        [Inject] private StationaryInput _stationaryInput;
        [Inject] private EcsWorld _world;
        [Inject(Id = "UpdateSystems")] private EcsSystems _updateSystems;
        [Inject(Id = "FixedUpdateSystems")] private EcsSystems _fixedUpdateSystem;
#if UNITY_EDITOR
        private EcsProfiler _ecsProfiler;
#endif
        private void Awake()
        {
            EcsPhysicsEvents.ecsWorld = _world;
            
            _updateSystems.ConvertScene();
            _fixedUpdateSystem.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _updateSystems.Init();
            _fixedUpdateSystem.Init();

#if UNITY_EDITOR
            _ecsProfiler = new EcsProfiler(_world, new EcsWorldDebugListener(), _updateSystems);
#endif
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystem.Run();
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            _ecsProfiler.Dispose();
            _ecsProfiler = null;
#endif
            EcsPhysicsEvents.ecsWorld = null;
            
            _updateSystems.Destroy();
            _updateSystems = null;
            
            _fixedUpdateSystem.Destroy();
            _fixedUpdateSystem = null;
            
            _world.Destroy();
            _world = null;
        }

        private void AddInjections()
        {
            _updateSystems
                .Inject(_stationaryInput);
        }

        private void AddOneFrames()
        {
            _updateSystems
                .OneFrame<DebugRequest>()
                .OneFrame<JumpEvent>()
                .OneFrame<CrouchEvent>()
                .OneFrame<LeanRequest>()
                .OneFrame<MoveRequest>()
                .OneFrame<RotateRequest>();
        }

        private void AddSystems()
        {
            _updateSystems
                .Add(new PlayerInitSystem())
                .Add(new StationaryInputSystem())
                .Add(new PlayerRotationSystem())
                .Add(new TimeSystem())
                .Add(new DebugSystem());

            _fixedUpdateSystem
                .Add(new PlayerMovementSystem())
                .Add(new FrictionSystem())
                .Add(new SlidingSystem())
                .Add(new GravitationSystem())
                .Add(new PlayerJumpSystem())
                .Add(new MovementSystem());
        }
    }
}