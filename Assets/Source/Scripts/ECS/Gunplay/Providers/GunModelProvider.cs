using NaughtyAttributes;
using UnityEngine;
using Voody.UniLeo;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ingame.Gunplay
{
    public sealed class GunModelProvider : MonoProvider<GunModel>
    {
#if UNITY_EDITOR
        private const float BARREL_DIRECTION_LINE_LENGTH = 2f;
     
        private void OnDrawGizmos()
        {
            var barrelOrigin = value.barrelTransform; 
            
            if(barrelOrigin == null)
                return;
                
            var position = barrelOrigin.position;
            
            Handles.color = Color.green;
            Handles.DrawLine(position, position + barrelOrigin.forward * BARREL_DIRECTION_LINE_LENGTH);
        }
#endif
        [Button]
        private void BakeHudPosition()
        {
            value.localPositionInsideHud = transform.localPosition;
            value.localRotationInsideHud = transform.localRotation;
        }

        [Button]
        private void MoveBackedPosition()
        {
            transform.localPosition = value.localPositionInsideHud;
            transform.localRotation = value.localRotationInsideHud;
        }
    }
}