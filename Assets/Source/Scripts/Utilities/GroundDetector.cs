using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ingame.Utilities
{
    public sealed class GroundDetector : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float groundDetectionDistance = .5f;
        [SerializeField] private Transform[] rayOrigins;

        public bool IsGrounded => CheckGroundWithRayCast();

        private bool CheckGroundWithRayCast()
        {
            if (rayOrigins == null || rayOrigins.Length < 1)
                return false;

            var layerMask = ~LayerMask.NameToLayer("PlayerStatic");

            foreach (var rayOrigin in rayOrigins)
            {
                if (rayOrigin == null)
                    continue;

                var ray = new Ray(rayOrigin.position, Vector3.down);

                if (Physics.Raycast(ray, groundDetectionDistance, layerMask, QueryTriggerInteraction.Ignore))
                    return true;
            }

            return false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (rayOrigins == null || rayOrigins.Length < 1)
                return;

            foreach (var rayOrigin in rayOrigins)
            {
                if (rayOrigin == null)
                    continue;

                var position = rayOrigin.position;
                
                Handles.color = Color.red;
                Handles.DrawLine(position, position + Vector3.down * groundDetectionDistance);
            }
        }
#endif
    }
}