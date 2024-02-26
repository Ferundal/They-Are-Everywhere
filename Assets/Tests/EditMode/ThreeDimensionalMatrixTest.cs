using LevelGeneration;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class ThreeDimensionalMatrixTest
    {
        [Test]
        public void SetZeroPosition()
        {
            var matrix = new ThreeDimensionalMatrix<Voxel>();

            var testObject = new Voxel();

            matrix[0, 0, 0] = testObject;
            
            Assert.NotNull(matrix[0, 0, 0]);
            Assert.AreEqual(testObject, matrix[0, 0, 0]);
        }
        
        [Test]
        public void SetMultiplyTimes()
        {
            var matrix = new ThreeDimensionalMatrix<Voxel>();

            var testObject = new Voxel();
            
            matrix[-5, -5, -5] = testObject;
            matrix[-5, -5, -10] = testObject;
            Assert.AreEqual(testObject, matrix[-5, -5, -5]);
            Assert.AreEqual(testObject, matrix[-5, -5, -10]);
        }
    }
}