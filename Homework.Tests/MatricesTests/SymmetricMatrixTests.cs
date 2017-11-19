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
            CollectionAssert.AreEqual(Enumerable.Repeat<string>(null, 16), SymmetricMatrix<string>.GetMatrixElements(matrix));

            var symmetric = new string[4, 4] { { "a", "b", "c", "d" }, { "b", "f", "e", "g" }, { "c", "e", "h", "i" }, { "d", "g", "i", "j" } };
            matrix = new SymmetricMatrix<string>(symmetric);
            Assert.True(SymmetricMatrix<string>.IsSymmetric(matrix));
            CollectionAssert.AreEqual("abcdbfegcehidgij".ToCharArray().Select(c => c.ToString()), SymmetricMatrix<string>.GetMatrixElements(matrix));

            var elements = new string[] { "string1", "string2", "string2", "string1" };
            matrix = new SymmetricMatrix<string>(elements);
            Assert.True(SymmetricMatrix<string>.IsSymmetric(matrix));
            CollectionAssert.AreEqual(elements, SymmetricMatrix<string>.GetMatrixElements(matrix));

            matrix.ElementChanged += Matrix_ElementChanged;
            matrix[0, 1] = "string3";
        }

        private void Matrix_ElementChanged(object sender, SquareMatrix<string>.ElementChangedEventArgs e)
        {
            Assert.That(e.OldValue == "string2" && e.NewValue == "string3" && ((e.Changed.Row == 0 && e.Changed.Column == 1) || (e.Changed.Row == 1 && e.Changed.Column == 0)));
        }
    }
}
