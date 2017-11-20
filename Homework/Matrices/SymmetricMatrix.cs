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
    public class SymmetricMatrix<T> : SquareMatrix<T>
    {
        private T[][] matrixBase;

        /// <summary>
        /// creates symmetric matrix instance of default values
        /// </summary>
        /// <param name="order"></param>
        public SymmetricMatrix(uint order)
        {
            this.Order = order;
            this.matrixBase = new T[order][];
            for (int i = 0; i < order; i++)
            {
                this.matrixBase[i] = new T[order - i];
            }
        }

        /// <summary>
        /// creates a symmetric matrix from a two-dimensional array of elements
        /// </summary>
        /// <param name="array">elements array</param>
        public SymmetricMatrix(T[,] array)
        {
            this.Order = (uint)array.GetLength(0);
            this.matrixBase = new T[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                this.matrixBase[i] = new T[array.GetLength(1) - i];
                for (int j = i; j < array.GetLength(1); j++)
                {
                    this.matrixBase[i][j - i] = array[i, j];
                }
            }

            if (!IsSymmetric(this))
            {
                throw new ArgumentException("given array cannot form a symmetric matrix", "array");
            }
        }

        /// <summary>
        /// creates a symmetric matrix instance from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SymmetricMatrix(params T[] elements) : this((uint)Math.Sqrt(elements.Length))
        {
            for (uint i = 0; i < this.Order; i++)
            {
                for (uint j = 0; j < this.Order; j++)
                {
                    this[i, j] = elements[(i * this.Order) + j];
                }
            }

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
                return this.matrixBase[row > column ? column : row][row > column ? row - column : column - row];
            }

            set
            {
                var old = this[row, column];
                this.matrixBase[row > column ? column : row][row > column ? row - column : column - row] = value;
                this.OnElementChanged(new ElementChangedEventArgs(row, column, old, value));
                this.OnElementChanged(new ElementChangedEventArgs(column, row, old, value));
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

        /// <summary>
        ///  two symmetric matrices addition
        /// </summary>
        /// <param name="matrix">matrix to add</param>
        /// <returns>sum of matrices</returns>
        public SymmetricMatrix<T> Add(SymmetricMatrix<T> matrix)
        {
            if (this.Order != matrix.Order)
            {
                throw new InvalidOperationException("cannot add matrices with not equal orders");
            }

            var newMatrix = new SymmetricMatrix<T>(Order);
            for (uint i = 0; i < newMatrix.Order; i++)
            {
                for (uint j = 0; j < newMatrix.Order; j++)
                {
                    newMatrix[i, j] = this[i, j] + (dynamic)matrix[i, j];
                }
            }

            return newMatrix;
        }
    }
}