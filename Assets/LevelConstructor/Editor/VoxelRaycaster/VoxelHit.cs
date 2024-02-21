using UnityEngine;

namespace LevelConstructor
{
    public class VoxelHit
    {
        public Vector3Int Position;
        public Vector3Int Direction;

        public VoxelHit()
        {
            Position = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            Direction = new Vector3Int();
        }
        
        public static bool operator ==(VoxelHit a, VoxelHit b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }


            return a.Position == b.Position && a.Direction == b.Direction;
        }
        
        public static bool operator !=(VoxelHit a, VoxelHit b)
        {
            return !(a == b);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            VoxelHit other = (VoxelHit)obj;
            return Position.Equals(other.Position) && Direction.Equals(other.Direction);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Position.GetHashCode();
                hash = hash * 23 + Direction.GetHashCode();
                return hash;
            }
        }
    }
}