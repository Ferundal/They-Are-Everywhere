using System.Text;
using UnityEngine;

namespace Utility
{
    public class Vector3IntUtility
    {
        public static Vector3Int MultiplyVectors(Vector3Int vector1, Vector3Int vector2)
        {
            return new Vector3Int(
                vector1.x * vector2.x,
                vector1.y * vector2.y,
                vector1.z * vector2.z
            );
        }
        
        public static Vector3Int RotateVector(Quaternion rotation, Vector3Int vector)
        {
            Vector3 rotatedVector = rotation * vector;
            return new Vector3Int(Mathf.RoundToInt(rotatedVector.x), Mathf.RoundToInt(rotatedVector.y), Mathf.RoundToInt(rotatedVector.z));
        }

        public string ToString(Vector3Int[] vectorArray)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Vector3Int vector in vectorArray)
            {
                stringBuilder.Append(vector.x).Append(", ").Append(vector.y).Append(", ").Append(vector.z).Append("; ");
            }
            
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Length -= 2;
            }

            return stringBuilder.ToString();
        }
    }
}