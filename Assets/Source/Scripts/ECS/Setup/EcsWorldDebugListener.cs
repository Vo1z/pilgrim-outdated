using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public class EcsWorldDebugListener : IEcsWorldDebugListener
    {
        public void OnEntityCreated(EcsEntity entity)
        {
            Debug.Log("Entity was created");
        }

        public void OnEntityDestroyed(EcsEntity entity)
        {
            Debug.Log("Entity was destroyed");
        }

        public void OnFilterCreated(EcsFilter filter)
        {
            // Debug.Log("Filter was created");
        }

        public void OnComponentListChanged(EcsEntity entity)
        {
            // Debug.Log("Component list was changed");
        }

        public void OnWorldDestroyed(EcsWorld world)
        {
            // Debug.Log("World was destroyed");
        }
    }
}