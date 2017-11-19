using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrices
{
    /// <summary>
    /// class represents a symmetric matrix
    /// </summary>
    /// <typeparam name="T">type of matrix elements</typeparam>
    public class SymmetricMatrix<T> : SquareMatrix<T> where T : IEquatable<T>
    {
        /// <summary>
        /// creates symmetric matrix instance of default values
        /// </summary>
        /// <param name="order"></param>
        public SymmetricMatrix(uint order)
            : base(order)
        {
        }

        /// <summary>
        /// creates a symmetric matrix from a two-dimensional array of elements
        /// </summary>
        /// <param name="array">elements array</param>
        public SymmetricMatrix(T[,] array)
            : base(array)
        {
            if (!IsSymmetric(this))
            {
                throw new ArgumentException("given array cannot form a symmetric matrix", "array");
            }
        }

        /// <summary>
        /// creates a symmetric matrix instance from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SymmetricMatrix(params T[] elements)
            : base(elements)
        {
            if (!IsSymmetric(this))
            {
                throw new ArgumentException("given elemets cannot form a symmetric matrix", "elements");
            }
        }

        /// <summary>
        /// symmetric matrix element indexer
        /// </summary>
        /// <param name="row">element row</param>
        /// <param name="column">element column</param>
        /// <returns>element value</returns>
        public override T this[uint row, uint column]
        {
            get
            {
                return base[row, column];
            }

            set
            {
                base[row, column] = value;
                if (row != column)
                {
                    base[column, row] = value;
                }
            }
        }

        /// <summary>
        /// checks if a specified square matrix is symmetric
        /// </summary>
        /// <param name="matrix">square matrix to check</param>
        /// <returns>true if symmetric; otherwise false</returns>
        public static bool IsSymmetric(SquareMatrix<T> matrix)
        {
            for (uint i = 0; i < matrix.Order; i++)
            {
                for (uint j = 0; j < matrix.Order; j++)
                {
                    if (!matrix[i, j].Equals(matrix[j, i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
