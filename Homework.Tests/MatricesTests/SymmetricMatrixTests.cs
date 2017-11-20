using System.Linq;
using Matrices;
using NUnit.Framework;

namespace Homework.Tests.MatricesTests
{
    [TestFixture]
    public class SymmetricMatrixTests
    {
        [Test]
        public void SymmetricMatrixTest()
        {
            SymmetricMatrix<string> matrix;

            matrix = new SymmetricMatrix<string>(4);
            CollectionAssert.AreEqual(Enumerable.Repeat<string>(null, 16), matrix.GetElements());

            var elements = new string[4, 4] { { "a", "b", "c", "d" }, { "b", "f", "e", "g" }, { "c", "e", "h", "i" }, { "d", "g", "i", "j" } };
            matrix = new SymmetricMatrix<string>(elements);
            Assert.True(SymmetricMatrix<string>.IsSymmetric(matrix));
            CollectionAssert.AreEqual("abcdbfegcehidgij".ToCharArray().Select(c => c.ToString()), matrix.GetElements());
            Assert.AreEqual("j", matrix[3, 3]);
            Assert.AreEqual("f", matrix[1, 1]);
            Assert.AreEqual("c", matrix[2, 0]);
            Assert.AreEqual("e", matrix[1, 2]);

            var elements2 = new string[] { "sym1", "sym2", "sym2", "sym1" };
            matrix = new SymmetricMatrix<string>(elements2);
            Assert.True(SymmetricMatrix<string>.IsSymmetric(matrix));
            CollectionAssert.AreEqual(elements2, matrix.GetElements());

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[0, 1] = "sym3";

            var square = new SquareMatrix<string>(new string[2, 2] { { "sq1", "sq2" }, { "sq3", "sq4" } });
            var sq1 = matrix.Add(square);
            var sq2 = square.Add(matrix);
            Assert.IsInstanceOf(typeof(SquareMatrix<string>), sq1);
            Assert.IsInstanceOf(typeof(SquareMatrix<string>), sq2);
            CollectionAssert.AreEqual(new string[4] { "sym1sq1", "sym3sq2", "sym3sq3", "sym1sq4" }, sq1.GetElements());
            CollectionAssert.AreEqual(new string[4] { "sq1sym1", "sq2sym3", "sq3sym3", "sq4sym1" }, sq2.GetElements());

            var symmetric = new SymmetricMatrix<string>(new string[2, 2] { { "1sym", "3sym" }, { "3sym", "1sym" } });
            var sum1 = matrix.Add(symmetric);
            var sum2 = symmetric.Add(matrix);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<string>), sum1);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<string>), sum2);
            CollectionAssert.AreEqual(new string[4] { "sym11sym", "sym33sym", "sym33sym", "sym11sym" }, sum1.GetElements());
            CollectionAssert.AreEqual(new string[4] { "1symsym1", "3symsym3", "3symsym3", "1symsym1" }, sum2.GetElements());

            var diagonal = new DiagonalMatrix<string>(2, "DIAGONAL");
            sum1 = sum1.Add(diagonal);
            sum2 = diagonal.Add(sum2);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<string>), sum1);
            Assert.IsInstanceOf(typeof(SymmetricMatrix<string>), sum2);
            CollectionAssert.AreEqual(new string[4] { "sym11symDIAGONAL", "sym33sym", "sym33sym", "sym11symDIAGONAL" }, sum1.GetElements());
            CollectionAssert.AreEqual(new string[4] { "DIAGONAL1symsym1", "3symsym3", "3symsym3", "DIAGONAL1symsym1" }, sum2.GetElements());
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<string>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == "sym2" && e.NewValue == "sym3" && ((e.Row == 0 && e.Column == 1) || (e.Row == 1 && e.Column == 0)));
        }
    }
}
