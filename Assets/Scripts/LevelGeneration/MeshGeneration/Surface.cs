using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    public class Surface
    {
        public MeshInfo MeshInfo { get; private set; } = new();
        public Vector3Int Direction { get; private set; }
        
        public Surface(List<Side> sides, Vector3Int direction)
        {
            Direction = direction;
            foreach (var side in sides)
            {
                AddMeshToSurface(side.MeshInfo);
            }
        }

        private void AddMeshToSurface(MeshInfo meshInfo)
        {
            var indexOffset = MeshInfo.Points.Count;
            meshInfo.AddOffsetToIndexes(indexOffset);
                
            RemovePointDuplicates(meshInfo, indexOffset);
            
            MeshInfo.TrianglesVertexIndexes.AddRange(meshInfo.TrianglesVertexIndexes);
        }

        private void RemovePointDuplicates(MeshInfo meshInfo, int indexOffset)
        {
            for (var index = 0; index < meshInfo.Points.Count; index++)
            {
                var existingIndex = MeshInfo.Points.IndexOf(meshInfo.Points[index]);
                if (existingIndex  != -1)
                {
                    ReplaceVertexIndexes(meshInfo.TrianglesVertexIndexes, index + indexOffset, existingIndex);
                }
                else
                {
                    MeshInfo.Points.Add(meshInfo.Points[index]);
                }
            }
        }

        private void ReplaceVertexIndexes(List<int> indexesList, int oldIndex, int newIndex)
        {
            for (var index = 0; index < indexesList.Count; index++)
            {
                if (indexesList[index] == oldIndex)
                {
                    indexesList[index] = newIndex;
                }
                else if (indexesList[index] > oldIndex)
                {
                    --indexesList[index];
                }
            }
        }
    }
}