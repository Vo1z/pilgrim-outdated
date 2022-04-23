using System;
using UnityEngine;

namespace Ingame.Cover
{
#if UNITY_EDITOR
    public class CoverPointGizmo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position,1);
            switch (transform.tag)
            {
                case "CoverPointRight":
                    Gizmos.DrawLine(transform.position,transform.position+transform.right*3);
                    break;
                case "CoverPointLeft":
                    Gizmos.DrawLine(transform.position,transform.position-transform.right*3);
                    break;
            }
        }
    }
#endif
}