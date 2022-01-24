using Ingame.Enemy.ECS;
using Ingame.Enemy.Logic;
using Leopotam.Ecs;

namespace Client {
    sealed class StateManagerSystem : IEcsRunSystem
    {
        private EcsFilter<StateModel> _statEcsFilter;
        void IEcsRunSystem.Run () {
            foreach (var i in _statEcsFilter)
            {
                ref var entity = ref _statEcsFilter.GetEntity(i);
                ref var state = ref _statEcsFilter.Get1(i);
                StateBase newState=  state.StateBase.Tick(ref entity);
                if (newState!=null)
                {
                    state.StateBase = newState;
                }
            }
        }

       
    }
}