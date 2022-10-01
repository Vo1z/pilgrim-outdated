using NaughtyAttributes;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Gunplay
{
    public sealed class FirearmComponentProvider : MonoProvider<FirearmComponent>
    {
        [Required, SerializeField] private Transform barrelOrigin;
        [Required, SerializeField] private FirearmConfig firearmConfig;
        
        [Inject]
        private void Construct()
        {
            value = new FirearmComponent
            {
                barrelOrigin = barrelOrigin,
                firearmConfig = firearmConfig
            };
        }
        
        private void OnDrawGizmos()
        {
            if(barrelOrigin == null)
                return;

            const float BARREL_LINE_DISTANCE = 2f;
            var originPos = barrelOrigin.position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(originPos, originPos + barrelOrigin.forward * BARREL_LINE_DISTANCE);
        }
    }
}