using UnityEngine;

namespace Ingame.Utilities
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class GroundDetection : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            IsGrounded = true;
        }

        private void OnTriggerExit(Collider other)
        {
            IsGrounded = false;
        }
    }
}