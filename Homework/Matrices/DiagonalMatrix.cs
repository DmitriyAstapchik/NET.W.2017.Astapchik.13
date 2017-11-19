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
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        /// <summary>
        /// creates a diagonal matrix of default values
        /// </summary>
        /// <param name="order">matrix order</param>
        public DiagonalMatrix(uint order)
            : base(order)
        {
        }

        /// <summary>
        /// creates a diagonal matrix with specified order and main diagonal value
        /// </summary>
        /// <param name="order">matrix order</param>
        /// <param name="diagonalValue">main diagonal value</param>
        public DiagonalMatrix(uint order, T diagonalValue)
            : this(order)
        {
            for (int i = 0; i < order; i++)
            {
                this.Matrix[i, i] = diagonalValue;
            }
        }

        /// <summary>
        /// creates a diagonal matrix with specified main diagonal values
        /// </summary>
        /// <param name="diagonalValues">main diagonal values</param>
        public DiagonalMatrix(params T[] diagonalValues)
            : this((uint)diagonalValues.Length)
        {
            for (int i = 0; i < this.Order; i++)
            {
                this.Matrix[i, i] = diagonalValues[i];
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
                return base[row, column];
            }

            set
            {
                if (row != column)
                {
                    throw new ArgumentException("cannot change non-diagonal elements");
                }

                base[row, column] = value;
            }
        }
    }
}
