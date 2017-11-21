using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrices
{
    /// <summary>
    /// represents a square matrix of elements of any type
    /// </summary>
    /// <typeparam name="T">type of matrix elements</typeparam>
    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        /// <summary>
        /// two-dimensional array of matrix elements
        /// </summary>
        private T[,] matrix;

        /// <summary>
        /// creates a square matrix of default values with specified order
        /// </summary>
        /// <param name="order">matrix order</param>
        public SquareMatrix(uint order) : base(order)
        {
            this.matrix = new T[order, order];
        }

        /// <summary>
        /// creates a square matrix of one value
        /// </summary>
        /// <param name="order">matrix order</param>
        /// <param name="value">value to fill the matrix</param>
        public SquareMatrix(uint order, T value) : this(order)
        {
            for (uint i = 0; i < order; i++)
            {
                for (uint j = 0; j < order; j++)
                {
                    this.matrix[i, j] = value;
                }
            }
        }

        /// <summary>
        /// creates a square matrix from a specified two-dimensional array
        /// </summary>
        /// <param name="array">array containing matrix elements</param>
        public SquareMatrix(T[,] array) : base(array)
        {
            this.matrix = array;
        }

        /// <summary>
        /// creates a square matrix from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SquareMatrix(params T[] elements) : base(elements)
        {
            this.matrix = new T[Order, Order];
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    this.matrix[i, j] = elements[(i * this.Order) + j];
                }
            }
        }

        /// <summary>
        /// square matrix indexer
        /// </summary>
        /// <param name="row">element row</param>
        /// <param name="column">element column</param>
        /// <returns>element at position</returns>
        public override T this[uint row, uint column]
        {
            get => this.matrix[row, column];
            set
            {
                var old = this.matrix[row, column];
                this.matrix[row, column] = value;
                this.OnElementChanged(new ElementChangedEventArgs(row, column, old, value));
            }
        }
    }
}
