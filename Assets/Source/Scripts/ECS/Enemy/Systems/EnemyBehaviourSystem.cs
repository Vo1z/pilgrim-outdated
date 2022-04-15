
using Leopotam.Ecs;
namespace Ingame.Enemy.System
{
    public sealed class EnemyBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<StateModel,EnemyBehaviourTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var state = ref _filter.Get1(i);
                state.State = state.State.GetPresentState(ref entity);
            }
        }
    }
}