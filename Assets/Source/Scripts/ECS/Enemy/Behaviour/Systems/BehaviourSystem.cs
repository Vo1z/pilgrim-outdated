using Leopotam.Ecs;

namespace Ingame.Behaviour
{
    public sealed class BehaviourSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BehaviourAgentModel,BehaviourCheckerTag> _behaviorAgentBeforeInitFilter;
        private readonly EcsFilter<BehaviourAgentModel,BehaviourAgentActiveTag>.Exclude<BehaviourCheckerTag> _behaviorAgentFilter;
       
        public void Run()
        {
            //Init 
            foreach (var i in _behaviorAgentBeforeInitFilter)
            { 
                ref var agent =  ref _behaviorAgentBeforeInitFilter.Get1(i);
                if (!agent.Tree.Init()) continue;
                ref var entity =  ref _behaviorAgentBeforeInitFilter.GetEntity(i);
                #if UNITY_EDITOR
                    UnityEngine.Debug.Log($"{agent.Tree.name} in {entity.ToString()} has been Initialized correctly");         
                #endif
                agent.Tree = agent.Tree.Clone();
                agent.Tree.InjectEntity(entity);
                entity.Get<BehaviourAgentActiveTag>();
                entity.Del<BehaviourCheckerTag>();
            }
            
            //Tick
            foreach (var i in _behaviorAgentFilter)
            {
                ref var agent =  ref _behaviorAgentBeforeInitFilter.Get1(i);
                ref var entity = ref _behaviorAgentBeforeInitFilter.GetEntity(i);
                
                if (agent.Tree.Tick() != Node.State.Running)
                {
                    entity.Del<BehaviourAgentActiveTag>();
                }
            }
        }
    }
}