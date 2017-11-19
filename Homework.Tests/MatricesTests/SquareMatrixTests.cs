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
            CollectionAssert.AreEqual(new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, SquareMatrix<int>.GetMatrixElements(matrix));

            matrix = new SquareMatrix<int>(new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            CollectionAssert.AreEqual(new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, SquareMatrix<int>.GetMatrixElements(matrix));

            matrix = new SquareMatrix<int>(9, 8, 7, 6, 5, 4, 3, 2, 1);
            CollectionAssert.AreEqual(new int[9] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }, SquareMatrix<int>.GetMatrixElements(matrix));

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[1, 1] = 0;
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<int>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == 5 && e.NewValue == 0 && e.Changed.Row == 1 && e.Changed.Column == 1);
        }
    }
}
