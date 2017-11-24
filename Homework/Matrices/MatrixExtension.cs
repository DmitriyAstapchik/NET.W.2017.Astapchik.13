using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public static class MatrixExtension
    {
        public static AbstractMatrix<T> Add<T>(this AbstractMatrix<T> matrix1, AbstractMatrix<T> matrix2)
        {
            if (matrix1.Order != matrix2.Order)
            {
                throw new InvalidOperationException("matrices orders are not equal");
            }

            AbstractMatrix<T> result;
            if (matrix1.GetType() == typeof(SquareMatrix<T>) || matrix2.GetType() == typeof(SquareMatrix<T>))
            {
                result = new SquareMatrix<T>(matrix1.Order);
            }
            else if (matrix1.GetType() == typeof(SymmetricMatrix<T>) || matrix2.GetType() == typeof(SymmetricMatrix<T>))
            {
                result = new SymmetricMatrix<T>(matrix1.Order);
            }
            else
            {
                result = new DiagonalMatrix<T>(matrix1.Order);
            }

            for (uint i = 0; i < result.Order; i++)
            {
                for (uint j = 0; j < result.Order; j++)
                {
                    if (result is DiagonalMatrix<T> && i != j)
                    {
                        continue;
                    }

                    try
                    {
                        result[i, j] = matrix1[i, j] + (dynamic)matrix2[i, j];
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                    {
                        throw new NotSupportedException(ex.Message);
                    }
                }
            }

            return result;
        }
    }
}
