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
            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();

#if UNITY_EDITOR
            _ecsProfiler = new EcsProfiler(_world, _systems);
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
                .OneFrame<JumpEvent>()
                .OneFrame<CrouchEvent>()
                .OneFrame<LeanRequest>();
        }

        private void AddSystems()
        {
            _systems
                .Add(new StationaryInputSystem());
        }
    }
}