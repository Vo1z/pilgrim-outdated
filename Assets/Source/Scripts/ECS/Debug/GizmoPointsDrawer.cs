using System;
using System.Collections.Generic;
using UnityEngine;


namespace Ingame.Debug
{
    #if UNITY_EDITOR
    public class GizmoPointsDrawer: MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private List<Transform> coverPoints;
        private void OnDrawGizmos()
        { 
            Gizmos.color = Color.blue;
            DrawSpheres(waypoints);
            Gizmos.color = Color.yellow;
            DrawSpheres(coverPoints);
        }

        private void DrawSpheres(List<Transform> list)
        {
            if (list == null)
            {
                return;
            }
            foreach (var i in list)
            {
                if (i==null)
                {
                    continue;
                }
                Gizmos.DrawSphere(i.position,1);   
            }
        }
    }
   #endif
}
