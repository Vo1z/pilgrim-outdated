using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;
 

namespace Ingame.Enemy.System
{
    public class DetectSystem : IEcsRunSystem
    {
        private EcsFilter<OnTriggerEnterEvent> _filterEnter;
        private EcsFilter<OnTriggerExitEvent> _filterExit;
        public void Run()
        {
            //On Trigger Enter
            foreach (var i in _filterEnter)
            {
                ref var enter = ref _filterEnter.Get1(i);
                ref var entity = ref _filterEnter.GetEntity(i);
                if (!enter.senderGameObject.TryGetComponent(out EntityReference entityReference))
                {
                    enter.senderGameObject.AddComponent<EntityReference>();
                    return;
                }
                var reference = enter.senderGameObject.GetComponent<EntityReference>();
                
                if (!reference.Entity.Has<VisionModel>())
                {
                    entity.Del<OnTriggerEnterEvent>();
                    return;
                }
                //Add Item to list
                ref var vision =  ref reference.Entity.Get<VisionModel>();
                if (enter.collider.CompareTag("Terrain"))
                {
                    vision.Covers.Add(enter.collider.transform);
                }
                else
                {
                    vision.Opponents.Add(enter.collider.transform);
                }
                
                entity.Del<OnTriggerEnterEvent>();
            }
            //On Trigger Exit
            foreach (var i in _filterExit)
            {
                ref var exit = ref _filterExit.Get1(i);
                ref var entity = ref _filterExit.GetEntity(i);
                var reference = exit.senderGameObject.GetComponent<EntityReference>();
                ref var vision =  ref reference.Entity.Get<VisionModel>();
                
                if (!reference.Entity.Has<VisionModel>())
                {
                    entity.Del<OnTriggerExitEvent>();
                    return;
                }
                //Remove Item from the list
                if (exit.collider.CompareTag("Terrain"))
                {
                    vision.Covers.Remove(exit.collider.transform);
                }
                else
                {
                    vision.Opponents.Remove(exit.collider.transform);
                }
                entity.Del<OnTriggerExitEvent>();
            }
        }
    }
}