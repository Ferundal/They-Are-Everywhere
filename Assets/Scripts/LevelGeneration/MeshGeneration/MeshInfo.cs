using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LevelGeneration
{
    public class MeshInfo
    {
        public List<Point> Points = new();
        public List<int> TrianglesVertexIndexes = new();

        public MeshInfo(MeshInfo meshInfo)
        {
            Points = meshInfo.Points;
            TrianglesVertexIndexes = new List<int>(meshInfo.TrianglesVertexIndexes);
        }

        public MeshInfo() { }

        public Vector3[] PointsAsVector3
        {
            get
            {
                var vector3Array = new Vector3[Points.Count];
                for (var index = 0; index < Points.Count; index++)
                {
                    var position = Points[index].Position;
                    vector3Array[index] = position;
                }

                return vector3Array;
            }
        }

        public Mesh Mesh
        {
            get
            {
                var mesh = new Mesh();
                
                mesh.vertices = PointsAsVector3;
                mesh.triangles = TrianglesVertexIndexes.ToArray();

                mesh.RecalculateNormals();
                
                return mesh;
            }
        }

        public void AddOffsetToIndexes(int indexOffset)
        {
            for (int index = 0; index < TrianglesVertexIndexes.Count; index++)
            {
                TrianglesVertexIndexes[index] += indexOffset;
            }
        }

#if UNITY_EDITOR
        public override string ToString()
        {
            var points = PointsAsVector3;
            
            var stringBuilder = new StringBuilder();

            for (var index = 0; index < TrianglesVertexIndexes.Count;)
            {
                stringBuilder.Append("Triangle: ");
                for (var indexInTriangle = 0; indexInTriangle < 3;)
                {
                    stringBuilder.Append($" {points[TrianglesVertexIndexes[index]]}");
                    ++index;
                    ++indexInTriangle;
                }
            }

            return stringBuilder.ToString();
        }
    }
#endif
}