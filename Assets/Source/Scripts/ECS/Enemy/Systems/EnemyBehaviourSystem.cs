
using Ingame.Enemy.Debug;
using Ingame.Enemy.State;
using Ingame.Health;
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
                //UnityEngine.Debug.Log(state.State.name);
                ref var x = ref entity.Get<HealthComponent>();
                VisualiseBehaviour(ref entity);
            }
        }

        private void VisualiseBehaviour(ref EcsEntity entity)
        {
            if (entity.Has<DebugStateVisualiserModel>())
            {
                ref var state = ref entity.Get<StateModel>();
                ref var visual = ref entity.Get<DebugStateVisualiserModel>();
                var renderer = visual.MeshRenderer;
                if (renderer == null)
                {
                    return;
                }
                if (entity.Has<AttackStateTag>())
                {
                    if (state.State.name.Trim() == "FocusedAttack")
                    {
                        renderer.material = visual.FocusedAttack;
                    }
                    else
                    {
                        renderer.material = visual.Attack;
                    }
                        
                }else if (entity.Has<FollowStateTag>())
                {
                    renderer.material = visual.Follow;
                }else if (entity.Has<PatrolStateTag>())
                {
                    renderer.material = visual.Patrol;
                }else if (entity.Has<HideStateTag>())
                {
                    renderer.material = visual.Hide;
                }else if (entity.Has<FleeStateTag>())
                {
                    renderer.material = visual.Flee;
                }else if (entity.Has<FindCoverStateTag>())
                {
                    renderer.material = visual.FindCover;
                }
            }
        }
    }
}