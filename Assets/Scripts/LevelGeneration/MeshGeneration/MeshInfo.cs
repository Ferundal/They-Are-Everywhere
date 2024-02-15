using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LevelGeneration
{
    public class MeshInfo
    {
        public List<Point> Points = new();
        public List<int> TrianglesVertexIndexes = new();

        public Vector3[] PointsAsVector3s
        {
            get
            {
                var vector3Array = new Vector3[Points.Count];
                for (var index = 0; index < Points.Count; index++)
                {
                    var position = Points[index].ParentVoxel.ParentShape.ParentLevel.GetPointPosition(Points[index]);
                    vector3Array[index] = position;
                }

                return vector3Array;
            }
        }

        public override string ToString()
        {
            var points = PointsAsVector3s;
            
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
}