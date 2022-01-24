using Leopotam.Ecs;

namespace Ingame.Enemy.ECS{
    sealed class StateManagerInitSystem : IEcsInitSystem {
        private EcsFilter<StateModel> _statEcsFilter;
        
        public void Init()
        {
            foreach (var i in _statEcsFilter)
            {
                ref var entity = ref _statEcsFilter.GetEntity(i);
                ref var state = ref _statEcsFilter.Get1(i);
                state.StateBase.OnEnter(ref entity);
            }
        }
    }
}