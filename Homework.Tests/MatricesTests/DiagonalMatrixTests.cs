using System;
using Matrices;
using NUnit.Framework;

namespace Homework.Tests.MatricesTests
{
    [TestFixture]
    public class DiagonalMatrixTests
    {
        private DiagonalMatrix<float> testMatrix;

        [Test]
        public void ConstructorsTest()
        {
            testMatrix = new DiagonalMatrix<float>(2);
            CollectionAssert.AreEqual(new float[4] { 0, 0, 0, 0 }, testMatrix.GetElements());

            testMatrix = new DiagonalMatrix<float>(3, 5);
            CollectionAssert.AreEqual(new float[9] { 5, 0, 0, 0, 5, 0, 0, 0, 5 }, testMatrix.GetElements());

            testMatrix = new DiagonalMatrix<float>(1, 2, 3);
            CollectionAssert.AreEqual(new float[9] { 1, 0, 0, 0, 2, 0, 0, 0, 3 }, testMatrix.GetElements());
        }

        [Test]
        public void EventTest()
        {
            testMatrix = new DiagonalMatrix<float>(1, 2, 3);

            testMatrix.ElementChanged += Matrix_ElementChanged;
            testMatrix[2, 2] = 9;
        }

        [Test]
        public void ExceptionsTest()
        {
            testMatrix = new DiagonalMatrix<float>(1, 2, 3, 4);

            Assert.Throws<NotSupportedException>(() => testMatrix[1, 2] = 2);
        }

        [Test]
        public void AdditionTest()
        {
            testMatrix = new DiagonalMatrix<float>(1, 2, 9);

            var square = new SquareMatrix<float>(3u, 5);
            var sum1 = testMatrix.Add(square);
            var sum2 = square.Add(testMatrix);
            Assert.IsInstanceOf(typeof(SquareMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(SquareMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 6, 5, 5, 5, 7, 5, 5, 5, 14 }, sum1.GetElements());

            var symmetric = new SymmetricMatrix<float>(new float[3, 3] { { 3, 3, 3 }, { 3, 2, 2, }, { 3, 2, 1 } });
            sum1 = testMatrix.Add(symmetric);
            sum2 = symmetric.Add(testMatrix);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 4, 3, 3, 3, 4, 2, 3, 2, 10 }, sum1.GetElements());

            var diagonal = new DiagonalMatrix<float>(3, 7);
            sum1 = testMatrix.Add(diagonal);
            sum2 = diagonal.Add(testMatrix);
            Assert.IsInstanceOf(typeof(DiagonalMatrix<float>), sum1);
            Assert.IsInstanceOf(typeof(DiagonalMatrix<float>), sum2);
            CollectionAssert.AreEqual(sum1.GetElements(), sum2.GetElements());
            CollectionAssert.AreEqual(new float[9] { 8, 0, 0, 0, 9, 0, 0, 0, 16 }, sum1.GetElements());

            Assert.Throws<InvalidOperationException>(() => diagonal.Add(new DiagonalMatrix<float>(2)));
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<float>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == 3 && e.NewValue == 9 && e.Row == 2 && e.Column == 2);
        }
    }
}
