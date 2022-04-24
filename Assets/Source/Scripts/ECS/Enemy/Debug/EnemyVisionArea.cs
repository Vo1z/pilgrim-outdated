 
using Ingame.Enemy.Data;
 
using UnityEngine;
 
#if UNITY_EDITOR
namespace Ingame.Enemy.Debug
{
    public sealed class EnemyVisionArea : MonoBehaviour
    {
        public float Angle;
        public float Distance;
        public float Height;
        public EnemyVisionData VisionData;
        public bool shouldWork =true;
        private Mesh CreateMesh()
        {
            
            Angle = VisionData.Angle;
            Distance = VisionData.Distance;
            Height = VisionData.Height;
            
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
 
            return mesh;
        }
        
        private void OnDrawGizmos()
        {
            if (!shouldWork)
            {
                return;
            }
            var mesh = CreateMesh();
            if (mesh.isReadable)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawMesh(mesh,transform.position,transform.rotation);
            }
        }
    }
}
#endif