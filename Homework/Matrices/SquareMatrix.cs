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
        public readonly uint Order;

        /// <summary>
        /// creates a square matrix of default values with specified order
        /// </summary>
        /// <param name="order">matrix order</param>
        public SquareMatrix(uint order)
        {
            this.Order = order;
            Matrix = new T[order, order];
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

            Matrix = array;
        }

        /// <summary>
        /// creates a square matrix from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SquareMatrix(params T[] elements)
            : this((uint)Math.Sqrt(elements.Length))
        {
            if (elements.Length != Order * Order)
            {
                throw new ArgumentException("number of elements is not equal to number of matrix elements");
            }

            for (int i = 0; i < Order; i++)
            {
                for (int j = 0; j < Order; j++)
                {
                    Matrix[i, j] = elements[(i * Order) + j];
                }
            }
        }

        /// <summary>
        /// occurs when matrix element is changed
        /// </summary>
        public event EventHandler<ElementChangedEventArgs> ElementChanged;

        /// <summary>
        /// two-dimensional array of matrix elements
        /// </summary>
        protected T[,] Matrix { get; set; }

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
                if (row >= Order)
                {
                    throw new ArgumentOutOfRangeException("row", "row number is too big");
                }

                if (column >= Order)
                {
                    throw new ArgumentOutOfRangeException("column", "column number is too big");
                }

                return Matrix[row, column];
            }

            set
            {
                var old = Matrix[row, column];
                Matrix[row, column] = value;
                ElementChanged(this, new ElementChangedEventArgs(old, value, new ElementChangedEventArgs.ElementLocation(row, column)));
            }
        }

        public static IEnumerable<T> GetMatrixElements(SquareMatrix<T> matrix)
        {
            for (uint i = 0; i < matrix.Order; i++)
            {
                for (uint j = 0; j < matrix.Order; j++)
                {
                    yield return matrix[i, j];
                }
            }
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

            /// <summary>
            /// changed element indices
            /// </summary>
            public readonly ElementLocation Changed;

            /// <summary>
            /// constructs event arguments instance with specified info
            /// </summary>
            /// <param name="old">old element value</param>
            /// <param name="new">new element value</param>
            /// <param name="changed">changed element indices</param>
            internal ElementChangedEventArgs(T old, T @new, ElementLocation changed)
            {
                this.OldValue = old;
                this.NewValue = @new;
                this.Changed = changed;
            }

            /// <summary>
            /// contains matrix element indices
            /// </summary>
            public struct ElementLocation
            {
                /// <summary>
                /// element row
                /// </summary>
                public readonly uint Row;

                /// <summary>
                /// element column
                /// </summary>
                public readonly uint Column;

                /// <summary>
                /// constructs an instance with specified row and column
                /// </summary>
                /// <param name="row">element row</param>
                /// <param name="column">element column</param>
                internal ElementLocation(uint row, uint column)
                {
                    this.Row = row;
                    this.Column = column;
                }
            }
        }
    }
}
