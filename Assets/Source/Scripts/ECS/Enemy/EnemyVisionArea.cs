using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ingame.Enemy
{
    public class EnemyVisionArea : MonoBehaviour
    {
        //'
        public float Angle;
        public float Distance;
        public float Height;

        public LayerMask Mask;
        //
        private string _tag = "Player";
        private bool _value;
        private readonly List<Collider> _colliders = new List<Collider>();
        private Transform _target;

        public void OnTriggerEnter(Collider other)
        {
            _colliders.Add(other);
            if (other.CompareTag(_tag))
            {
                _target = other.transform;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            _colliders.Remove(other);
            if (other.CompareTag(_tag))
            {
                _value = false;
                _target = null;
            }
        }
        

        private Mesh CreateMesh()
        {
            var segment = 12;
            Mesh mesh = new Mesh();
            var numberOfTringles = segment*4+4;
            var numberOfVertices = numberOfTringles*3;
            Vector3[] vertices = new Vector3[numberOfVertices];
            int[] triangles = new int[numberOfVertices];
            //
            Vector3 bottomCenter = Vector3.zero;
            Vector3 bottomRight = Quaternion.Euler(0,-Angle,0)*Vector3.forward*Distance;
            Vector3 bottomLeft = Quaternion.Euler(0,Angle,0)*Vector3.forward*Distance;
            //
            Vector3 topCenter = bottomCenter + Vector3.up * Height;
            Vector3 topRight = bottomRight+ Vector3.up * Height;
            Vector3 topLeft = bottomLeft + Vector3.up * Height;
            //
            int vert = 0;
            
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomLeft;
            vertices[vert++] = topLeft;
            
            vertices[vert++] = topLeft;
            vertices[vert++] = topCenter;
            vertices[vert++] = bottomCenter;
            //
            vertices[vert++] = bottomCenter;
            vertices[vert++] = topCenter;
            vertices[vert++] = topRight;
            
            vertices[vert++] = topRight;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomCenter;

            float currentAngle = -Angle;
            float deltaAngle = Angle * 2 / segment;
            for (int i = 0; i < segment; i++)
            {
                bottomRight = Quaternion.Euler(0,currentAngle,0)*Vector3.forward*Distance;
                bottomLeft = Quaternion.Euler(0,currentAngle+deltaAngle,0)*Vector3.forward*Distance;
                //

                topRight = bottomRight+ Vector3.up * Height;
                topLeft = bottomLeft + Vector3.up * Height;
                currentAngle += deltaAngle;
                 
                //far
                vertices[vert++] = bottomLeft;
                vertices[vert++] = bottomRight;
                vertices[vert++] = topRight;
            
                vertices[vert++] = topRight;
                vertices[vert++] = topLeft;
                vertices[vert++] = bottomLeft;
                //top
                vertices[vert++] = topCenter;
                vertices[vert++] = topLeft;
                vertices[vert++] = topRight;
                //bottom
                vertices[vert++] = bottomCenter;
                vertices[vert++] = bottomRight;
                vertices[vert++] = bottomLeft;
            }


            for (int i = 0; i < numberOfVertices; i++)
            {
                triangles[i] = i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            vert = 0;
            return mesh;
        }
        
        

        private void OnDrawGizmos()
        {
            var mesh = CreateMesh();
            if (mesh.isReadable)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawMesh(mesh,transform.position,transform.rotation);
            }
        }
    }
}