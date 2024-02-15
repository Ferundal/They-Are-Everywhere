using System.Collections.Generic;
using System.Linq;

namespace LevelGeneration
{
    public class Surface
    {
        private MeshInfo _meshInfo = new();
        public Surface(List<Side> sides)
        {
            foreach (var side in sides)
            {
                AddMeshToSurface(side.MeshInfo);
            }
        }

        private void AddMeshToSurface(MeshInfo meshInfo)
        {
            var indexOffset = _meshInfo.Points.Count;
            AddOffsetToIndexes(meshInfo.TrianglesVertexIndexes, indexOffset);
                
            RemovePointDuplicates(meshInfo, indexOffset);
            
            _meshInfo.TrianglesVertexIndexes.AddRange(meshInfo.TrianglesVertexIndexes);
        }

        private void AddOffsetToIndexes(List<int> indexesList, int indexOffset)
        {
            for (int index = 0; index < indexesList.Count; index++)
            {
                indexesList[index] += indexOffset;
            }
        }

        private void RemovePointDuplicates(MeshInfo meshInfo, int indexOffset)
        {
            for (var index = 0; index < meshInfo.Points.Count; index++)
            {
                var existingIndex = _meshInfo.Points.IndexOf(meshInfo.Points[index]);
                if (existingIndex  != -1)
                {
                    ReplaceVertexIndexes(meshInfo.TrianglesVertexIndexes, index + indexOffset, existingIndex);
                }
                else
                {
                    _meshInfo.Points.Add(meshInfo.Points[index]);
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