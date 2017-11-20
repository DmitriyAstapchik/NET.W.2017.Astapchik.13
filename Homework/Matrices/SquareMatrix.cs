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
    public class SquareMatrix<T>
    {
        /// <summary>
        /// matrix order
        /// </summary>
        private uint order;

        /// <summary>
        /// two-dimensional array of matrix elements
        /// </summary>
        private T[,] matrix;

        /// <summary>
        /// creates a square matrix of default values with specified order
        /// </summary>
        /// <param name="order">matrix order</param>
        public SquareMatrix(uint order)
        {
            this.order = order;
            matrix = new T[order, order];
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
                    this[i, j] = value;
                }
            }
        }

        /// <summary>
        /// creates a square matrix from a specified two-dimensional array
        /// </summary>
        /// <param name="array">array containing matrix elements</param>
        public SquareMatrix(T[,] array)
            : this((uint)array.GetLength(0))
        {
            if (array.GetLength(0) != array.GetLength(1))
            {
                throw new ArgumentException("array dimensions are not equal");
            }

            matrix = array;
        }

        /// <summary>
        /// creates a square matrix from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SquareMatrix(params T[] elements)
            : this((uint)Math.Sqrt(elements.Length))
        {
            if (elements.Length != order * order)
            {
                throw new ArgumentException("number of elements is not equal to number of matrix elements");
            }

            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    matrix[i, j] = elements[(i * order) + j];
                }
            }
        }

        /// <summary>
        /// occurs when matrix element is changed
        /// </summary>
        public event EventHandler<ElementChangedEventArgs> ElementChanged;

        /// <summary>
        /// gets matrix order
        /// </summary>
        public uint Order { get => order; protected set => order = value; }

        /// <summary>
        /// matrix element indexer
        /// </summary>
        /// <param name="row">element row</param>
        /// <param name="column">element column</param>
        /// <returns>element value</returns>
        public virtual T this[uint row, uint column]
        {
            get
            {
                if (row >= order)
                {
                    throw new ArgumentOutOfRangeException("row", "row number is too big");
                }

                if (column >= order)
                {
                    throw new ArgumentOutOfRangeException("column", "column number is too big");
                }

                return matrix[row, column];
            }

            set
            {
                var old = matrix[row, column];
                matrix[row, column] = value;
                ElementChanged?.Invoke(this, new ElementChangedEventArgs(row, column, old, value));
            }
        }

        /// <summary>
        /// gets an enumeration of matrix elements
        /// </summary>
        /// <returns>matrix elements</returns>
        public IEnumerable<T> GetElements()
        {
            for (uint i = 0; i < Order; i++)
            {
                for (uint j = 0; j < Order; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        /// <summary>
        /// two square matrices addition
        /// </summary>
        /// <param name="matrix">matrix to add</param>
        /// <returns>sum of matrices</returns>
        public SquareMatrix<T> Add(SquareMatrix<T> matrix)
        {
            if (order != matrix.order)
            {
                throw new InvalidOperationException("cannot add matrices with not equal orders");
            }

            var newMatrix = new SquareMatrix<T>(order);
            for (uint i = 0; i < newMatrix.order; i++)
            {
                for (uint j = 0; j < newMatrix.order; j++)
                {
                    newMatrix[i, j] = this[i, j] + (dynamic)matrix[i, j];
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// event handler invocation
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnElementChanged(ElementChangedEventArgs e)
        {
            ElementChanged?.Invoke(this, e);
        }

        /// <summary>
        /// arguments provided for a value changing event
        /// </summary>
        public class ElementChangedEventArgs : EventArgs
        {
            /// <summary>
            /// old element value
            /// </summary>
            public readonly T OldValue;

            /// <summary>
            /// new element value
            /// </summary>
            public readonly T NewValue;

            public readonly uint Row;
            public readonly uint Column;

            internal ElementChangedEventArgs(uint row, uint column, T old, T @new)
            {
                this.Row = row;
                this.Column = column;
                this.OldValue = old;
                this.NewValue = @new;
            }
        }
    }
}
