using Matrices;
using NUnit.Framework;

namespace Homework.Tests.MatricesTests
{
    [TestFixture]
    public class DiagonalMatrixTests
    {
        [Test]
        public void DiagonalMatrixTest()
        {
            DiagonalMatrix<float> matrix;

            matrix = new DiagonalMatrix<float>(2);
            CollectionAssert.AreEqual(new float[4] { 0, 0, 0, 0 }, DiagonalMatrix<float>.GetMatrixElements(matrix));

            matrix = new DiagonalMatrix<float>(3, 5);
            CollectionAssert.AreEqual(new float[9] { 5, 0, 0, 0, 5, 0, 0, 0, 5 }, DiagonalMatrix<float>.GetMatrixElements(matrix));

            matrix = new DiagonalMatrix<float>(1, 2, 3);
            CollectionAssert.AreEqual(new float[9] { 1, 0, 0, 0, 2, 0, 0, 0, 3 }, DiagonalMatrix<float>.GetMatrixElements(matrix));

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[2, 2] = 9;
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<float>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == 3 && e.NewValue == 9 && e.Changed.Row == 2 && e.Changed.Column == 2);
        }
    }
}
