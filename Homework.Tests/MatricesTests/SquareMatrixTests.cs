using System.Linq;
using Matrices;
using NUnit.Framework;

namespace Homework.Tests.MatricesTests
{
    [TestFixture]
    public class SquareMatrixTests
    {
        [Test]
        public void SquareMatrixTest()
        {
            SquareMatrix<int> matrix;

            matrix = new SquareMatrix<int>(3u);
            CollectionAssert.AreEqual(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, matrix.GetElements());

            matrix = new SquareMatrix<int>(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            CollectionAssert.AreEqual(new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, matrix.GetElements());

            matrix = new SquareMatrix<int>(9, 8, 7, 6, 5, 4, 3, 2, 1);
            CollectionAssert.AreEqual(new int[9] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }, matrix.GetElements());

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[1, 1] = 0;

            var square = new SquareMatrix<int>(1, 2, 3, 4, 10, 6, 7, 8, 9);
            var sum1 = matrix.Add(square);
            var sum2 = square.Add(matrix);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum1);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(Enumerable.Repeat(10, 9), sum1.GetElements());

            var symmetric = new SymmetricMatrix<int>(new int[3, 3] { { 3, 3, 3 }, { 3, 2, 2, }, { 3, 2, 1 } });
            sum1 = sum1.Add(symmetric);
            sum2 = symmetric.Add(sum2);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum1);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new int[9] { 13, 13, 13, 13, 12, 12, 13, 12, 11 }, sum1.GetElements());

            var diagonal = new DiagonalMatrix<int>(-13, -12, -11);
            sum1 = sum1.Add(diagonal);
            sum2 = diagonal.Add(sum2);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum1);
            Assert.IsInstanceOf(typeof(SquareMatrix<int>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new int[9] { 0, 13, 13, 13, 0, 12, 13, 12, 0 }, sum1.GetElements());
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<int>.ElementChangedEventArgs e)
        {
            Assert.That(e.Row == 1 && e.Column == 1 && e.OldValue == 5 && e.NewValue == 0);
        }
    }
}
