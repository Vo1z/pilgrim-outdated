﻿using NaughtyAttributes;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Ingame.Gunplay
{
    public sealed class FirearmComponentProvider : MonoProvider<FirearmComponent>
    {
        [Required, SerializeField] private Transform barrelOrigin;
        [Required, SerializeField] [Min(0)] private float damage;
        
        [Inject]
        private void Construct()
        {
            value = new FirearmComponent
            {
                barrelOrigin = barrelOrigin,
                damage = damage
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