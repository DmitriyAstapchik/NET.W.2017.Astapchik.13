using System;
using System.Collections;
using System.Collections.Generic;

namespace Homework
{
    /// <summary>
    /// queue data structure of elements of any type
    /// </summary>
    /// <typeparam name="T">queue element type</typeparam>
    public class Queue<T> : IEnumerable<T>
    {
        /// <summary>
        /// start queue capacity
        /// </summary>
        private const uint StartCapacity = 10;

        /// <summary>
        /// inner array
        /// </summary>
        private T[] array;

        /// <summary>
        /// first element index
        /// </summary>
        private int start;

        /// <summary>
        /// index for next queue element
        /// </summary>
        private int end;

        /// <summary>
        /// queue elements number
        /// </summary>
        private int count;

        /// <summary>
        /// queue 'version'
        /// </summary>
        private int version;

        /// <summary>
        /// creates a queue with specified start capacity
        /// </summary>
        /// <param name="capacity"></param>
        public Queue(uint capacity = StartCapacity)
        {
            array = new T[capacity];
        }

        /// <summary>
        /// counts queue elements number
        /// </summary>
        public int Count => count;

        /// <summary>
        /// enqueues an element to the end
        /// </summary>
        /// <param name="item">an element to enqueue</param>
        public void Enqueue(T item)
        {
            if (count == array.Length)
            {
                var newArray = new T[array.Length * 2];
                if (count > 0)
                {
                    if (start < end)
                    {
                        Array.Copy(array, start, newArray, 0, count);
                    }
                    else
                    {
                        Array.Copy(array, start, newArray, 0, array.Length - start);
                        Array.Copy(array, 0, newArray, array.Length - start, end);
                    }
                }

                array = newArray;
                start = 0;
                end = count;
            }

            array[end] = item;
            end = (end + 1) % array.Length;
            count++;
            version++;
        }

        /// <summary>
        /// removes the first queue element and returns its value
        /// </summary>
        /// <returns>removed element</returns>
        /// <exception cref="InvalidOperationException">the queue is empty</exception>
        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("the queue is empty");
            }

            var item = array[start];
            start = (start + 1) % array.Length;
            count--;
            version++;

            return item;
        }

        /// <summary>
        /// returns the first queue element
        /// </summary>
        /// <returns>first queue element</returns>
        /// <exception cref="InvalidOperationException">the queue is empty</exception>
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("the queue is empty");
            }

            return array[start];
        }

        /// <summary>
        /// clears the queue
        /// </summary>
        public void Clear()
        {
            if (start < end)
            {
                Array.Clear(array, start, count);
            }
            else
            {
                Array.Clear(array, start, array.Length - start);
                Array.Clear(array, 0, end);
            }

            start = 0;
            end = 0;
            count = 0;
            version++;
        }

        /// <summary>
        /// gets the queue enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new QueueEnumerator(this);
        }

        /// <summary>
        /// returns enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// queue enumerator
        /// </summary>
        private class QueueEnumerator : IEnumerator<T>
        {
            /// <summary>
            /// queue to enumerate
            /// </summary>
            private Queue<T> queue;

            /// <summary>
            /// current index
            /// </summary>
            private int index;

            /// <summary>
            /// current value
            /// </summary>
            private T current;

            /// <summary>
            /// current version
            /// </summary>
            private int version;

            /// <summary>
            /// constructs a queue enumerator of a specified queue
            /// </summary>
            /// <param name="queue">queue to enumerate</param>
            public QueueEnumerator(Queue<T> queue)
            {
                this.queue = queue;
                index = -1;
                current = default(T);
                version = queue.version;
            }

            /// <summary>
            /// gets current element
            /// </summary>
            /// <exception cref="InvalidOperationException">enumerator is before the start or after the end</exception>
            public T Current
            {
                get
                {
                    if (index >= 0)
                    {
                        return current;
                    }
                    else
                    {
                        throw new InvalidOperationException(index == -1 ? "enumerator is before the start" : "enumerator is after the end");
                    }
                }
            }

            /// <summary>
            /// gets current object
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// disposes resources
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// moves enumerator to next element
            /// </summary>
            /// <returns>true if success; otherwise false</returns>
            /// <exception cref="InvalidOperationException">queue version was changed</exception>
            public bool MoveNext()
            {
                if (version != queue.version)
                {
                    throw new InvalidOperationException("queue was changed");
                }

                if (index == -2)
                {
                    return false;
                }

                if (++index == queue.count)
                {
                    index = -2;
                    current = default(T);
                    return false;
                }

                current = queue.array[(queue.start + index) % queue.array.Length];
                return true;
            }

            /// <summary>
            /// resets enumerator position
            /// </summary>
            /// <exception cref="InvalidOperationException">queue version was changed</exception>
            public void Reset()
            {
                if (version != queue.version)
                {
                    throw new InvalidOperationException("queue was changed");
                }

                index = -1;
                current = default(T);
            }
        }
    }
}
