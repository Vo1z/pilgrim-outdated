using Ingame.Movement;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Health
{
    public sealed class RagdollSystem : IEcsRunSystem
    {
        private EcsFilter<RagdollTag,DeathTag> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                if (entity.Has<TransformModel>())
                {
                    ref var tranform = ref entity.Get<TransformModel>();
                    if (tranform.transform.gameObject.TryGetComponent(out NavMeshAgent agent))
                    {
                        Object.Destroy(agent);
                    }
                    if (tranform.transform.gameObject.TryGetComponent(out EntityReference entityRef))
                    {
                        Object.Destroy(entityRef);
                    }
                    if (!tranform.transform.gameObject.TryGetComponent(out Rigidbody rb))
                    {
                       rb = tranform.transform.gameObject.AddComponent<Rigidbody>();
                    }
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.constraints = RigidbodyConstraints.None;
                    entity.Destroy();
                }
            }
        }
    }
}