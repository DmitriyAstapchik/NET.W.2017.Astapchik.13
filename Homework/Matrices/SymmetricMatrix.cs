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
    public class SymmetricMatrix<T> : AbstractMatrix<T>
    {
        /// <summary>
        /// triangle matrix as a base
        /// </summary>
        private T[][] matrixBase;

        /// <summary>
        /// creates symmetric matrix instance of default values
        /// </summary>
        /// <param name="order"></param>
        public SymmetricMatrix(uint order) : base(order)
        {
            this.matrixBase = new T[order][];
            for (int i = 0; i < order; i++)
            {
                this.matrixBase[i] = new T[order - i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="value"></param>
        public SymmetricMatrix(uint order, T value) : this(order)
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
        /// creates a symmetric matrix from a two-dimensional array of elements
        /// </summary>
        /// <param name="array">elements array</param>
        public SymmetricMatrix(T[,] array) : base(array)
        {
            this.matrixBase = ToMatrixBase(array);
        }

        /// <summary>
        /// creates a symmetric matrix instance from specified elements
        /// </summary>
        /// <param name="elements">matrix elements left-to-right top-to-bottom</param>
        public SymmetricMatrix(params T[] elements) : base(elements)
        {
            var array = new T[Order, Order];
            for (uint i = 0; i < this.Order; i++)
            {
                for (uint j = 0; j < this.Order; j++)
                {
                    array[i, j] = elements[(i * this.Order) + j];
                }
            }

            this.matrixBase = ToMatrixBase(array);
        }

        /// <summary>
        /// symmetric matrix element indexer
        /// </summary>
        /// <param name="row">element row</param>
        /// <param name="column">element column</param>
        /// <returns>element value</returns>
        public override T this[uint row, uint column]
        {
            get => this.matrixBase[row > column ? column : row][row > column ? row - column : column - row];
            set
            {
                var old = this[row, column];
                this.matrixBase[row > column ? column : row][row > column ? row - column : column - row] = value;
                this.OnElementChanged(new ElementChangedEventArgs(row, column, old, value));
                this.OnElementChanged(new ElementChangedEventArgs(column, row, old, value));
            }
        }

        /// <summary>
        /// gets symmetric matrix base from a square array of matrix elements
        /// </summary>
        /// <param name="array">array of matrix elements</param>
        /// <returns>symmetric matrix base</returns>
        private static T[][] ToMatrixBase(T[,] array)
        {
            if (!IsSymmetric(array))
            {
                throw new ArgumentException("this array cannot form a symmetric matrix", "array");
            }

            var matrixBase = new T[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                matrixBase[i] = new T[array.GetLength(0) - i];
                for (int j = i; j < array.GetLength(0); j++)
                {
                    matrixBase[i][j - i] = array[i, j];
                }
            }

            return matrixBase;
        }

        /// <summary>
        /// checks if a specified array can form a symmetric matrix
        /// </summary>
        /// <param name="array">array to check</param>
        /// <returns>true if symmetric; otherwise false</returns>
        private static bool IsSymmetric(T[,] array)
        {
            if (array.GetLength(0) != array.GetLength(1))
            {
                return false;
            }

            for (uint i = 0; i < array.GetLength(0); i++)
            {
                for (uint j = 0; j < array.GetLength(1); j++)
                {
                    if (!array[i, j].Equals(array[j, i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}