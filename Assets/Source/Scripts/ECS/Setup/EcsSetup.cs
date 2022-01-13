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
        [Inject] private EcsSystems _systems;
#if UNITY_EDITOR
        private EcsProfiler _ecsProfiler;
#endif
        private void Awake()
        {
            EcsPhysicsEvents.ecsWorld = _world;
            
            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();

#if UNITY_EDITOR
            _ecsProfiler = new EcsProfiler(_world, new EcsWorldDebugListener(), _systems);
#endif
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR
            _ecsProfiler.Dispose();
            _ecsProfiler = null;
#endif
            EcsPhysicsEvents.ecsWorld = null;
            
            _systems.Destroy();
            _systems = null;
            
            _world.Destroy();
            _world = null;
        }

        private void AddInjections()
        {
            _systems
                .Inject(_stationaryInput);
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<DebugRequest>()
                .OneFrame<JumpEvent>()
                .OneFrame<CrouchEvent>()
                .OneFrame<LeanRequest>()
                .OneFrame<MoveRequest>()
                .OneFrame<RotateRequest>();
        }

        private void AddSystems()
        {
            _systems
                .Add(new PlayerInitSystem())
                .Add(new StationaryInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
                .Add(new PlayerFrictionSystem())
                .Add(new SlidingSystem())
                .Add(new GravitationSystem())
                .Add(new PlayerJumpSystem())
                .Add(new MovementSystem())
                .Add(new TimeSystem())
                .Add(new DebugSystem());
        }
    }
}