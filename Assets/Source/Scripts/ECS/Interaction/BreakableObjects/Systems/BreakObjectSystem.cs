
using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEditor;
namespace Ingame.Interaction.Breakable{
    public class BreakObjectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BreakableObjectModel,TransformModel,BreakObjectTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var breakable = ref _filter.Get1(i);
                ref var transform = ref _filter.Get2(i);
                Object.Instantiate(breakable.BrokenObject, transform.transform.position,transform.transform.rotation);
                Object.Destroy(transform.transform.gameObject);
                entity.Destroy();
            }
        }
    }
}