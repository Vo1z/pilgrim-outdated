using Leopotam.Ecs;
using UnityEngine;

namespace Ingame
{
    public sealed class GravitationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<VelocityComponent, GravityComponent, CharacterControllerModel> _gravityFilter;

        public void Run()
        {
            foreach (var i in _gravityFilter)
            {
                ref var velocityComp = ref _gravityFilter.Get1(i);
                ref var gravityComponent = ref _gravityFilter.Get2(i); 
                ref var characterControllerComp = ref _gravityFilter.Get3(i);

                if (characterControllerComp.CharacterController.isGrounded)
                    velocityComp.velocity.y = 0;
                else
                {
                    var maximalGravitationalForce = gravityComponent.maximalGravitationalForce;
                    var gravityOffsetY = -gravityComponent.gravityAcceleration * Time.deltaTime;
                    velocityComp.velocity.y = Mathf.Clamp
                    (
                        velocityComp.velocity.y + gravityOffsetY,
                        -maximalGravitationalForce,
                        maximalGravitationalForce
                    );
                }
            }
        }
    }
}