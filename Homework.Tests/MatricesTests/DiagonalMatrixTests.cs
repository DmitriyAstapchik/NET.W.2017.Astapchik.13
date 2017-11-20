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
            CollectionAssert.AreEqual(new float[4] { 0, 0, 0, 0 }, matrix.GetElements());

            matrix = new DiagonalMatrix<float>(3, 5);
            CollectionAssert.AreEqual(new float[9] { 5, 0, 0, 0, 5, 0, 0, 0, 5 }, matrix.GetElements());

            matrix = new DiagonalMatrix<float>(1, 2, 3);
            CollectionAssert.AreEqual(new float[9] { 1, 0, 0, 0, 2, 0, 0, 0, 3 }, matrix.GetElements());

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[2, 2] = 9;

            var square = new SquareMatrix<float>(3u, 5);
            var sum1 = matrix.Add(square);
            var sum2 = square.Add(matrix);
            Assert.IsInstanceOf(typeof(SquareMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(SquareMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 6, 5, 5, 5, 7, 5, 5, 5, 14 }, sum1.GetElements());

            var symmetric = new SymmetricMatrix<float>(new float[3, 3] { { 3, 3, 3 }, { 3, 2, 2, }, { 3, 2, 1 } });
            sum1 = matrix.Add(symmetric);
            sum2 = symmetric.Add(matrix);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 4, 3, 3, 3, 4, 2, 3, 2, 10 }, sum1.GetElements());

            var diagonal = new DiagonalMatrix<float>(3, 7);
            sum1 = matrix.Add(diagonal);
            sum2 = diagonal.Add(matrix);
            Assert.IsInstanceOf(typeof(DiagonalMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(DiagonalMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 8, 0, 0, 0, 9, 0, 0, 0, 16 }, sum1.GetElements());
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<float>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == 3 && e.NewValue == 9 && e.Row == 2 && e.Column == 2);
        }
    }
}
