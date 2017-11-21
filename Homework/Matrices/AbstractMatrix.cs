using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    /// <summary>
    /// represents square matrix abstraction
    /// </summary>
    /// <typeparam name="T">type of matrix elements</typeparam>
    public abstract class AbstractMatrix<T>
    {
        /// <summary>
        /// matrix order
        /// </summary>
        private uint order;

        /// <summary>
        /// initializes matrix order
        /// </summary>
        /// <param name="order">matrix order</param>
        protected AbstractMatrix(uint order)
        {
            this.Order = order;
        }

        /// <summary>
        /// matrix construction from a two-dimensional array
        /// </summary>
        /// <param name="array">array of matrix elements</param>
        /// <exception cref="ArgumentException">array dimensions are not equal</exception>
        protected AbstractMatrix(T[,] array) : this((uint)array.GetLength(0))
        {
            if (array.GetLength(0) != array.GetLength(1))
            {
                throw new ArgumentException("array dimensions are not equal");
            }
        }

        /// <summary>
        /// matrix construction from a list of elements
        /// </summary>
        /// <param name="elements"></param>
        /// <exception cref="ArgumentException">wrong number of elements</exception>
        protected AbstractMatrix(params T[] elements) : this((uint)Math.Sqrt(elements.Length))
        {
            if (elements.Length != Order * Order)
            {
                throw new ArgumentException("number of elements is not equal to number of matrix elements");
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
        public abstract T this[uint row, uint column] { get; set; }

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

            /// <summary>
            /// changed element row
            /// </summary>
            public readonly uint Row;

            /// <summary>
            /// changed element column
            /// </summary>
            public readonly uint Column;

            /// <summary>
            /// constructs an instance with specified event arguments
            /// </summary>
            /// <param name="row">changed element row</param>
            /// <param name="column">changed element column</param>
            /// <param name="old">old element value</param>
            /// <param name="new">new element value</param>
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
