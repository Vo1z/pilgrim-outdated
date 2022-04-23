using LeoEcsPhysics;
using Leopotam.Ecs;


namespace Ingame.Enemy.System
{
    public sealed class DetectSystem : IEcsRunSystem
    {
        private EcsFilter<OnTriggerEnterEvent> _filterEnter;
        private EcsFilter<OnTriggerExitEvent> _filterExit;
        public void Run()
        {
            //On Trigger Enter
            foreach (var i in _filterEnter)
            {
                ref var enter = ref _filterEnter.Get1(i);
                if (!enter.senderGameObject.TryGetComponent(out EntityReference entityReference))
                {
                    continue;
                }

                if (!entityReference.Entity.Has<VisionModel>())
                {
                    continue;
                }
                //Add Item to list
                ref var vision =  ref entityReference.Entity.Get<VisionModel>();
                if (enter.collider.CompareTag("CoverPoint") || enter.collider.CompareTag("CoverPointLeft") || enter.collider.CompareTag("CoverPointRight"))
                {
                    vision.Covers.Add(enter.collider.transform);
                    
                }
                else if(enter.collider.CompareTag("Player")) 
                {
                    vision.Opponents.Add(enter.collider.transform);
                }
            }
            //On Trigger Exit
            foreach (var i in _filterExit)
            {
                ref var exit = ref _filterExit.Get1(i);
                var reference = exit.senderGameObject.GetComponent<EntityReference>();
                
                if (!reference.Entity.Has<VisionModel>())
                {
                    continue;
                }
                
                ref var vision =  ref reference.Entity.Get<VisionModel>();
                
                //Remove Item from the list
                if (exit.collider.CompareTag("CoverPoint")|| exit.collider.CompareTag("CoverPointLeft") || exit.collider.CompareTag("CoverPointRight"))
                {
                    vision.Covers.Remove(exit.collider.transform);
                }
                else if(exit.collider.CompareTag("Player"))
                {
                    vision.Opponents.Remove(exit.collider.transform);
                }
               
            }
        }
    }
}