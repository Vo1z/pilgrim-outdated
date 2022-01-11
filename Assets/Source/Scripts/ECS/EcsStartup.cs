using Leopotam.Ecs;
using Support;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [Inject] private Support.StationaryInputSystem _stationaryInputSystem;
        
        private EcsWorld _world;
        private EcsSystems _updateSystems;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);

            _updateSystems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _updateSystems.Init();
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void OnDestroy()
        {
            _updateSystems.Destroy();
            _updateSystems = null;
            
            _world.Destroy();
            _world = null;
        }

        private void AddInjections()
        {
            _updateSystems
                .Inject(_stationaryInputSystem);
        }

        private void AddOneFrames()
        {
            _updateSystems
                .OneFrame<JumpEvent>()
                .OneFrame<CrouchEvent>();
        }

        private void AddSystems()
        {
            _updateSystems
                .Add(new StationaryInputSystem())
                .Add(new MovementSystem());
        }
    }
}