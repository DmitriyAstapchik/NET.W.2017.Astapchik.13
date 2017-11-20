using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrices
{
    /// <summary>
    /// represents a diagonal matrix
    /// </summary>
    /// <typeparam name="T">type of matrix elements</typeparam>
    public class DiagonalMatrix<T> : SymmetricMatrix<T>
    {
        /// <summary>
        /// main diagonal values
        /// </summary>
        private T[] mainDiagonal;

        /// <summary>
        /// creates a diagonal matrix of default values
        /// </summary>
        /// <param name="order">matrix order</param>
        public DiagonalMatrix(uint order)
        {
            this.Order = order;
            this.mainDiagonal = new T[order];
        }

        /// <summary>
        /// creates a diagonal matrix with specified order and main diagonal value
        /// </summary>
        /// <param name="order">matrix order</param>
        /// <param name="diagonalValue">main diagonal value</param>
        public DiagonalMatrix(uint order, T diagonalValue) : this(order)
        {
            for (int i = 0; i < this.mainDiagonal.Length; i++)
            {
                this.mainDiagonal[i] = diagonalValue;
            }
        }

        /// <summary>
        /// creates a diagonal matrix with specified main diagonal values
        /// </summary>
        /// <param name="diagonalValues">main diagonal values</param>
        public DiagonalMatrix(params T[] diagonalValues)
            : this((uint)diagonalValues.Length)
        {
            for (int i = 0; i < diagonalValues.Length; i++)
            {
                this.mainDiagonal[i] = diagonalValues[i];
            }
        }

        /// <summary>
        /// diagonal matrix element indexer
        /// </summary>
        /// <param name="row">element row</param>
        /// <param name="column">element column</param>
        /// <returns>element value</returns>
        public override T this[uint row, uint column]
        {
            get
            {
                return row != column ? default(T) : this.mainDiagonal[row];
            }

            set
            {
                if (row == column)
                {
                    var old = this.mainDiagonal[row];
                    this.mainDiagonal[row] = value;
                    this.OnElementChanged(new ElementChangedEventArgs(row, column, old, value));
                }
            }
        }

        /// <summary>
        /// two diagonal matrices addition
        /// </summary>
        /// <param name="matrix">matrix to add</param>
        /// <returns>sum of matrices</returns>
        public DiagonalMatrix<T> Add(DiagonalMatrix<T> matrix)
        {
            if (this.Order != matrix.Order)
            {
                throw new InvalidOperationException("cannot add matrices with not equal orders");
            }

            var newMatrix = new DiagonalMatrix<T>(Order);
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
