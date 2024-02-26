using UnityEngine;

namespace LevelConstructor
{
    public class VoxelHit
    {
        public Vector3Int HitVoxelPosition;
        public Vector3Int HitDirection;
        public Side HitSide;

        public VoxelHit()
        {
            HitVoxelPosition = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            HitDirection = new Vector3Int();
            HitSide = null;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is VoxelHit other)
            {
                return HitVoxelPosition.Equals(other.HitVoxelPosition)
                       && HitDirection.Equals(other.HitDirection)
                       && HitSide == other.HitSide;
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + HitVoxelPosition.GetHashCode();
                hash = hash * 23 + HitDirection.GetHashCode();
                hash = hash * 23 + HitSide.GetHashCode();
                return hash;
            }
        }


        public static bool operator ==(VoxelHit left, VoxelHit right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(VoxelHit left, VoxelHit right)
        {
            return !(left == right);
        }
    }
}