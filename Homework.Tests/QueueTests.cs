using System;
using NUnit.Framework;

namespace Homework.Tests
{
    [TestFixture]
    public class QueueTests
    {
        private const uint TestCapacity = 5;
        private static Queue<int> queue = new Queue<int>(TestCapacity);

        [Test]
        public void QueueTest()
        {
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());

            for (int i = 0; i < TestCapacity; i++)
            {
                queue.Enqueue(i + 1);
            }

            Assert.AreEqual(1, queue.Peek());

            for (int i = 0; i < TestCapacity; i++)
            {
                Assert.AreEqual(i + 1, queue.Dequeue());
            }

            Assert.Throws<InvalidOperationException>(() => queue.Peek());

            var enumerator = queue.GetEnumerator();
            int current;
            Assert.Throws<InvalidOperationException>(() => { current = enumerator.Current; });
            Assert.False(enumerator.MoveNext());
            Assert.DoesNotThrow(() => enumerator.Reset());

            queue.Enqueue(6);
            Assert.Throws<InvalidOperationException>(() => enumerator.Reset());
            Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());

            enumerator = queue.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() => { current = enumerator.Current; });
            Assert.True(enumerator.MoveNext());
            Assert.AreEqual(6, enumerator.Current);
            Assert.False(enumerator.MoveNext());

            Assert.DoesNotThrow(() => enumerator.Reset());
            Assert.Throws<InvalidOperationException>(() => { current = enumerator.Current; });
            Assert.True(enumerator.MoveNext());
            Assert.AreEqual(6, enumerator.Current);
            Assert.False(enumerator.MoveNext());

            Assert.AreEqual(6, queue.Dequeue());
        }
    }
}
