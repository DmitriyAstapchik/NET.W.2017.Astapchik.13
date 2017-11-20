using System;
using System.Collections.Generic;

namespace Homework
{
    /// <summary>
    /// represents a binary search tree of elements of any type
    /// </summary>
    /// <typeparam name="T">type of tree elements</typeparam>
    public class BinarySearchTree<T>
    {
        /// <summary>
        /// tree node element
        /// </summary>
        private T data;

        /// <summary>
        /// left tree node
        /// </summary>
        private BinarySearchTree<T> left;

        /// <summary>
        /// right tree node
        /// </summary>
        private BinarySearchTree<T> right;

        /// <summary>
        /// parent tree node
        /// </summary>
        private BinarySearchTree<T> parent;

        /// <summary>
        /// indicates if a tree node has data
        /// </summary>
        private bool hasData = false;

        /// <summary>
        /// tree elements comparer
        /// </summary>
        private IComparer<T> comparer;

        /// <summary>
        /// creates a tree with specified comparer
        /// </summary>
        /// <param name="comparer"></param>
        public BinarySearchTree(IComparer<T> comparer = null)
        {
            this.comparer = comparer ?? ((typeof(T).GetInterface(nameof(IComparable)) != null || typeof(T).GetInterface(nameof(IComparable<T>)) != null) ? Comparer<T>.Default : throw new ApplicationException($"type {nameof(T)} does not contain a default comparer"));
        }

        /// <summary>
        /// creates a tree of specified elements with specified comparer
        /// </summary>
        /// <param name="items">items to place to the tree</param>
        /// <param name="comparer">items comparer</param>
        public BinarySearchTree(IEnumerable<T> items, IComparer<T> comparer = null) : this(comparer)
        {
            foreach (var item in items)
            {
                Insert(item);
            }
        }

        /// <summary>
        /// tree infix traverse
        /// </summary>
        /// <param name="tree">tree to traverse</param>
        /// <returns>tree elements</returns>
        public static IEnumerable<T> InfixTraverse(BinarySearchTree<T> tree)
        {
            if (tree == null || !tree.hasData)
            {
                yield break;
            }

            foreach (var item in InfixTraverse(tree.left))
            {
                yield return item;
            }

            yield return tree.data;
            foreach (var item in InfixTraverse(tree.right))
            {
                yield return item;
            }
        }

        /// <summary>
        /// tree prefix traverse
        /// </summary>
        /// <param name="tree">tree to traverse</param>
        /// <returns>tree elements</returns>
        public static IEnumerable<T> PrefixTraverse(BinarySearchTree<T> tree)
        {
            if (tree == null || !tree.hasData)
            {
                yield break;
            }

            yield return tree.data;
            foreach (var item in PrefixTraverse(tree.left))
            {
                yield return item;
            }

            foreach (var item in PrefixTraverse(tree.right))
            {
                yield return item;
            }
        }

        /// <summary>
        /// tree postfix traverse
        /// </summary>
        /// <param name="tree">tree to traverse</param>
        /// <returns>tree elements</returns>
        public static IEnumerable<T> PostfixTraverse(BinarySearchTree<T> tree)
        {
            if (tree == null || !tree.hasData)
            {
                yield break;
            }

            foreach (var item in PostfixTraverse(tree.left))
            {
                yield return item;
            }

            foreach (var item in PostfixTraverse(tree.right))
            {
                yield return item;
            }

            yield return tree.data;
        }

        /// <summary>
        /// finds an element in a tree
        /// </summary>
        /// <param name="item">item to find</param>
        /// <returns>tree element or null if not found</returns>
        public BinarySearchTree<T> Find(T item)
        {
            if (!hasData)
            {
                return null;
            }

            if (comparer.Compare(item, data) == 0)
            {
                return this;
            }
            else if (comparer.Compare(item, data) > 0)
            {
                return right?.Find(item);
            }
            else
            {
                return left?.Find(item);
            }
        }

        /// <summary>
        /// inserts an item to the tree
        /// </summary>
        /// <param name="item">item to insert</param>
        public void Insert(T item)
        {
            if (!hasData)
            {
                data = item;
                hasData = true;
            }
            else
            {
                if (comparer.Compare(item, data) > 0)
                {
                    if (right == null)
                    {
                        right = new BinarySearchTree<T>(comparer);
                        right.parent = this;
                    }

                    right.Insert(item);
                }
                else if (comparer.Compare(item, data) < 0)
                {
                    if (left == null)
                    {
                        left = new BinarySearchTree<T>(comparer);
                        left.parent = this;
                    }

                    left.Insert(item);
                }
            }
        }

        /// <summary>
        /// removes an item from the tree
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true if removed; otherwise false</returns>
        public bool Remove(T item)
        {
            if (!hasData)
            {
                return false;
            }
            else
            {
                if (comparer.Compare(item, data) > 0)
                {
                    return right?.Remove(item) ?? false;
                }
                else if (comparer.Compare(item, data) < 0)
                {
                    return left?.Remove(item) ?? false;
                }
                else
                {
                    if (left == null && right == null)
                    {
                        if (parent != null)
                        {
                            if (parent.left == this)
                            {
                                parent.left = null;
                            }
                            else if (parent.right == this)
                            {
                                parent.right = null;
                            }

                            parent = null;
                        }

                        hasData = false;
                    }
                    else if (left == null)
                    {
                        var temp = right;
                        data = right.data;
                        left = right.left;
                        right = right.right;
                        temp.parent = null;
                        temp = null;
                    }
                    else if (right == null)
                    {
                        var temp = left;
                        data = left.data;
                        right = left.right;
                        left = left.left;
                        temp.parent = null;
                        temp = null;
                    }
                    else if (right.left == null)
                    {
                        var temp = right.right;
                        data = right.data;
                        right = right.right;
                        if (temp != null)
                        {
                            temp.parent = null;
                            temp = null;
                        }
                    }
                    else
                    {
                        var temp = right.left.data;
                        Remove(right.left.data);
                        data = temp;
                    }
                }

                return true;
            }
        }
    }
}