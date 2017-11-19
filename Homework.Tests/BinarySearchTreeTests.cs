using System;
using System.Collections.Generic;
using System.Windows;
using BookSystem;
using NUnit.Framework;

namespace Homework.Tests
{
    [TestFixture]
    public class BinarySearchTreeTests
    {
        [TestFixture]
        private class IntTreeTests // System.Int32 
        {
            private static int[] items = { 10, 11, 12, 13, 14, 4, 3, 2, 1 };
            private static object[] defaultTree = new[] { new BinarySearchTree<int>(items) };
            private static object[] defaultTree2 = new[] { new BinarySearchTree<int>(items) };
            private static object[] defaultTree3 = new[] { new BinarySearchTree<int>(items) };
            private static object[] comparerTree = new[] { new BinarySearchTree<int>(items, new IntComparer()) };
            private static object[] comparerTree2 = new[] { new BinarySearchTree<int>(items, new IntComparer()) };
            private static object[] comparerTree3 = new[] { new BinarySearchTree<int>(items, new IntComparer()) };

            [Test, TestCaseSource("defaultTree")]
            public void TraverseDefaultTest(BinarySearchTree<int> tree)
            {
                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 10, 11, 12, 13, 14 }, BinarySearchTree<int>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new int[] { 10, 4, 3, 2, 1, 11, 12, 13, 14 }, BinarySearchTree<int>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 14, 13, 12, 11, 10 }, BinarySearchTree<int>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree")]
            public void TraverseComparerTest(BinarySearchTree<int> tree)
            {
                CollectionAssert.AreEqual(new int[] { 14, 13, 12, 11, 10, 4, 3, 2, 1 }, BinarySearchTree<int>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new int[] { 10, 11, 12, 13, 14, 4, 3, 2, 1 }, BinarySearchTree<int>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new int[] { 14, 13, 12, 11, 1, 2, 3, 4, 10 }, BinarySearchTree<int>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree")]
            public void FindDefaultTest(BinarySearchTree<int> tree)
            {
                Assert.Null(tree.Find(20));
                Assert.Null(tree.Find(5));
                Assert.Null(tree.Find(-10));
                Assert.Null(tree.Find(15));

                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, BinarySearchTree<int>.InfixTraverse(tree.Find(4)));
                CollectionAssert.AreEqual(new int[] { 11, 12, 13, 14 }, BinarySearchTree<int>.PrefixTraverse(tree.Find(11)));
                CollectionAssert.AreEqual(new int[] { 14, 13 }, BinarySearchTree<int>.PostfixTraverse(tree.Find(13)));
            }

            [Test, TestCaseSource("comparerTree")]
            public void FindComparerTest(BinarySearchTree<int> tree)
            {
                Assert.Null(tree.Find(20));
                Assert.Null(tree.Find(5));
                Assert.Null(tree.Find(-10));
                Assert.Null(tree.Find(15));

                CollectionAssert.AreEqual(new int[] { 4, 3, 2, 1 }, BinarySearchTree<int>.InfixTraverse(tree.Find(4)));
                CollectionAssert.AreEqual(new int[] { 11, 12, 13, 14 }, BinarySearchTree<int>.PrefixTraverse(tree.Find(11)));
                CollectionAssert.AreEqual(new int[] { 14, 13 }, BinarySearchTree<int>.PostfixTraverse(tree.Find(13)));
            }

            [Test, TestCaseSource("defaultTree2")]
            public void InsertDefaultTest(BinarySearchTree<int> tree)
            {
                tree.Insert(0);
                CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4, 10, 11, 12, 13, 14 }, BinarySearchTree<int>.InfixTraverse(tree));
                tree.Insert(8);
                CollectionAssert.AreEqual(new int[] { 10, 4, 3, 2, 1, 0, 8, 11, 12, 13, 14 }, BinarySearchTree<int>.PrefixTraverse(tree));
                tree.Insert(20);
                CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 8, 4, 20, 14, 13, 12, 11, 10 }, BinarySearchTree<int>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree2")]
            public void InsertComparerTest(BinarySearchTree<int> tree)
            {
                tree.Insert(0);
                CollectionAssert.AreEqual(new int[] { 14, 13, 12, 11, 10, 4, 3, 2, 1, 0 }, BinarySearchTree<int>.InfixTraverse(tree));
                tree.Insert(8);
                CollectionAssert.AreEqual(new int[] { 10, 11, 12, 13, 14, 4, 8, 3, 2, 1, 0 }, BinarySearchTree<int>.PrefixTraverse(tree));
                tree.Insert(20);
                CollectionAssert.AreEqual(new int[] { 20, 14, 13, 12, 11, 8, 0, 1, 2, 3, 4, 10 }, BinarySearchTree<int>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree3")]
            public void RemoveDefaultTest(BinarySearchTree<int> tree)
            {
                Assert.False(tree.Remove(-5));
                Assert.False(tree.Remove(7));
                Assert.False(tree.Remove(15));
                Assert.False(tree.Remove(25));

                tree.Remove(4);
                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 10, 11, 12, 13, 14 }, BinarySearchTree<int>.InfixTraverse(tree));
                tree.Remove(12);
                CollectionAssert.AreEqual(new int[] { 10, 3, 2, 1, 11, 13, 14 }, BinarySearchTree<int>.PrefixTraverse(tree));
                tree.Remove(10);
                CollectionAssert.AreEqual(new int[] { 1, 2, 3, 14, 13, 11 }, BinarySearchTree<int>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree3")]
            public void RemoveComparerTest(BinarySearchTree<int> tree)
            {
                Assert.False(tree.Remove(-5));
                Assert.False(tree.Remove(7));
                Assert.False(tree.Remove(15));
                Assert.False(tree.Remove(25));

                tree.Remove(4);
                CollectionAssert.AreEqual(new int[] { 14, 13, 12, 11, 10, 3, 2, 1, }, BinarySearchTree<int>.InfixTraverse(tree));
                tree.Remove(12);
                CollectionAssert.AreEqual(new int[] { 10, 11, 13, 14, 3, 2, 1 }, BinarySearchTree<int>.PrefixTraverse(tree));
                tree.Remove(10);
                CollectionAssert.AreEqual(new int[] { 14, 13, 11, 1, 2, 3, }, BinarySearchTree<int>.PostfixTraverse(tree));
            }
        }

        [TestFixture]
        private class StringTreeTests // System.String 
        {
            private static string[] items = { "mmm", "aaa", "bbb", "ccc", "zzz", "yyy", "xxx" };
            private static object[] defaultTree = new[] { new BinarySearchTree<string>(items) };
            private static object[] defaultTree2 = new[] { new BinarySearchTree<string>(items) };
            private static object[] defaultTree3 = new[] { new BinarySearchTree<string>(items) };
            private static object[] comparerTree = new[] { new BinarySearchTree<string>(items, new StringComparer()) };
            private static object[] comparerTree2 = new[] { new BinarySearchTree<string>(items, new StringComparer()) };
            private static object[] comparerTree3 = new[] { new BinarySearchTree<string>(items, new StringComparer()) };

            [Test, TestCaseSource("defaultTree")]
            public void TraverseDefaultTest(BinarySearchTree<string> tree)
            {
                CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "ccc", "mmm", "xxx", "yyy", "zzz" }, BinarySearchTree<string>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new string[] { "mmm", "aaa", "bbb", "ccc", "zzz", "yyy", "xxx" }, BinarySearchTree<string>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new string[] { "ccc", "bbb", "aaa", "xxx", "yyy", "zzz", "mmm" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree")]
            public void TraverseComparerTest(BinarySearchTree<string> tree)
            {
                CollectionAssert.AreEqual(new string[] { "zzz", "yyy", "xxx", "mmm", "ccc", "bbb", "aaa" }, BinarySearchTree<string>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new string[] { "mmm", "zzz", "yyy", "xxx", "aaa", "bbb", "ccc" }, BinarySearchTree<string>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new string[] { "xxx", "yyy", "zzz", "ccc", "bbb", "aaa", "mmm" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree")]
            public void FindDefaultTest(BinarySearchTree<string> tree)
            {
                Assert.Null(tree.Find("zzzzz"));
                Assert.Null(tree.Find("ppp"));
                Assert.Null(tree.Find("fff"));
                Assert.Null(tree.Find("aa"));

                CollectionAssert.AreEqual(new string[] { "xxx", "yyy", "zzz" }, BinarySearchTree<string>.InfixTraverse(tree.Find("zzz")));
                CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "ccc" }, BinarySearchTree<string>.PrefixTraverse(tree.Find("aaa")));
                CollectionAssert.AreEqual(new string[] { "ccc", "bbb" }, BinarySearchTree<string>.PostfixTraverse(tree.Find("bbb")));
            }

            [Test, TestCaseSource("comparerTree")]
            public void FindComparerTest(BinarySearchTree<string> tree)
            {
                Assert.Null(tree.Find("zzzzz"));
                Assert.Null(tree.Find("ppp"));
                Assert.Null(tree.Find("fff"));
                Assert.Null(tree.Find("aa"));

                CollectionAssert.AreEqual(new string[] { "zzz", "yyy", "xxx" }, BinarySearchTree<string>.InfixTraverse(tree.Find("zzz")));
                CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "ccc" }, BinarySearchTree<string>.PrefixTraverse(tree.Find("aaa")));
                CollectionAssert.AreEqual(new string[] { "ccc", "bbb" }, BinarySearchTree<string>.PostfixTraverse(tree.Find("bbb")));
            }

            [Test, TestCaseSource("defaultTree2")]
            public void InsertDefaultTest(BinarySearchTree<string> tree)
            {
                tree.Insert("ddd");
                CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "ccc", "ddd", "mmm", "xxx", "yyy", "zzz" }, BinarySearchTree<string>.InfixTraverse(tree));
                tree.Insert("sss");
                CollectionAssert.AreEqual(new string[] { "mmm", "aaa", "bbb", "ccc", "ddd", "zzz", "yyy", "xxx", "sss" }, BinarySearchTree<string>.PrefixTraverse(tree));
                tree.Insert("AAA");
                CollectionAssert.AreEqual(new string[] { "AAA", "ddd", "ccc", "bbb", "aaa", "sss", "xxx", "yyy", "zzz", "mmm" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree2")]
            public void InsertComparerTest(BinarySearchTree<string> tree)
            {
                tree.Insert("ddd");
                CollectionAssert.AreEqual(new string[] { "zzz", "yyy", "xxx", "mmm", "ddd", "ccc", "bbb", "aaa" }, BinarySearchTree<string>.InfixTraverse(tree));
                tree.Insert("sss");
                CollectionAssert.AreEqual(new string[] { "mmm", "zzz", "yyy", "xxx", "sss", "aaa", "bbb", "ccc", "ddd" }, BinarySearchTree<string>.PrefixTraverse(tree));
                tree.Insert("AAA");
                CollectionAssert.AreEqual(new string[] { "sss", "xxx", "yyy", "zzz", "ddd", "ccc", "AAA", "bbb", "aaa", "mmm" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree3")]
            public void RemoveDefaultTest(BinarySearchTree<string> tree)
            {
                Assert.False(tree.Remove("AAA"));
                Assert.False(tree.Remove("ddd"));
                Assert.False(tree.Remove("ppp"));
                Assert.False(tree.Remove("zzzz"));

                tree.Remove("ccc");
                CollectionAssert.AreEqual(new string[] { "aaa", "bbb", "mmm", "xxx", "yyy", "zzz" }, BinarySearchTree<string>.InfixTraverse(tree));
                tree.Remove("zzz");
                CollectionAssert.AreEqual(new string[] { "mmm", "aaa", "bbb", "yyy", "xxx" }, BinarySearchTree<string>.PrefixTraverse(tree));
                tree.Remove("mmm");
                CollectionAssert.AreEqual(new string[] { "bbb", "aaa", "yyy", "xxx" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree3")]
            public void RemoveComparerTest(BinarySearchTree<string> tree)
            {
                Assert.False(tree.Remove("AAA"));
                Assert.False(tree.Remove("ddd"));
                Assert.False(tree.Remove("ppp"));
                Assert.False(tree.Remove("zzzz"));

                tree.Remove("ccc");
                CollectionAssert.AreEqual(new string[] { "zzz", "yyy", "xxx", "mmm", "bbb", "aaa" }, BinarySearchTree<string>.InfixTraverse(tree));
                tree.Remove("zzz");
                CollectionAssert.AreEqual(new string[] { "mmm", "yyy", "xxx", "aaa", "bbb" }, BinarySearchTree<string>.PrefixTraverse(tree));
                tree.Remove("mmm");
                CollectionAssert.AreEqual(new string[] { "xxx", "yyy", "aaa", "bbb" }, BinarySearchTree<string>.PostfixTraverse(tree));
            }
        }

        [TestFixture]
        private class BookTreeTests // BookSystem.Book 
        {
            private static Book book1 = new Book("111", "author", "title", "publisher", DateTime.Now, 200, 111);
            private static Book book2 = new Book("222", "author", "title", "publisher", DateTime.Now, 155, 222);
            private static Book book3 = new Book("333", "author", "title", "publisher", DateTime.Now, 241, 333);
            private static Book book4 = new Book("444", "author", "title", "publisher", DateTime.Now, 198, 444);
            private static Book book5 = new Book("555", "author", "title", "publisher", DateTime.Now, 110, 555);
            private static Book[] books = new Book[] { book2, book4, book1, book3, book5 };
            private static object[] defaultTree = new[] { new BinarySearchTree<Book>(books) };
            private static object[] defaultTree2 = new[] { new BinarySearchTree<Book>(books) };
            private static object[] defaultTree3 = new[] { new BinarySearchTree<Book>(books) };
            private static object[] comparerTree = new[] { new BinarySearchTree<Book>(books, new BookComparer()) };
            private static object[] comparerTree2 = new[] { new BinarySearchTree<Book>(books, new BookComparer()) };
            private static object[] comparerTree3 = new[] { new BinarySearchTree<Book>(books, new BookComparer()) };

            [Test, TestCaseSource("defaultTree")]
            public void TraverseDefaultTest(BinarySearchTree<Book> tree)
            {
                CollectionAssert.AreEqual(new Book[] { book1, book2, book3, book4, book5 }, BinarySearchTree<Book>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new Book[] { book2, book1, book4, book3, book5 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new Book[] { book1, book3, book5, book4, book2 }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree")]
            public void TraverseComparerTest(BinarySearchTree<Book> tree)
            {
                CollectionAssert.AreEqual(new Book[] { book5, book4, book3, book2, book1 }, BinarySearchTree<Book>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new Book[] { book2, book4, book5, book3, book1 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new Book[] { book5, book3, book4, book1, book2 }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree")]
            public void FindDefaultTest(BinarySearchTree<Book> tree)
            {
                Assert.Null(tree.Find(new Book("011", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("211", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("334", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("556", "author", "title", "publisher", DateTime.Now, 200, 15)));

                CollectionAssert.AreEqual(new Book[] { book1 }, BinarySearchTree<Book>.InfixTraverse(tree.Find(book1)));
                CollectionAssert.AreEqual(new Book[] { book4, book3, book5 }, BinarySearchTree<Book>.PrefixTraverse(tree.Find(book4)));
                CollectionAssert.AreEqual(new Book[] { book3 }, BinarySearchTree<Book>.PostfixTraverse(tree.Find(book3)));
            }

            [Test, TestCaseSource("comparerTree")]
            public void FindComparerTest(BinarySearchTree<Book> tree)
            {
                Assert.Null(tree.Find(new Book("011", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("211", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("334", "author", "title", "publisher", DateTime.Now, 200, 15)));
                Assert.Null(tree.Find(new Book("556", "author", "title", "publisher", DateTime.Now, 200, 15)));

                CollectionAssert.AreEqual(new Book[] { book1 }, BinarySearchTree<Book>.InfixTraverse(tree.Find(book1)));
                CollectionAssert.AreEqual(new Book[] { book4, book5, book3 }, BinarySearchTree<Book>.PrefixTraverse(tree.Find(book4)));
                CollectionAssert.AreEqual(new Book[] { book3 }, BinarySearchTree<Book>.PostfixTraverse(tree.Find(book3)));
            }

            [Test, TestCaseSource("defaultTree2")]
            public void InsertDefaultTest(BinarySearchTree<Book> tree)
            {
                var book001 = new Book("011", "author", "title", "publisher", DateTime.Now, 200, 1);
                var book155 = new Book("155", "author", "title", "publisher", DateTime.Now, 200, 155);
                var book399 = new Book("399", "author", "title", "publisher", DateTime.Now, 200, 399);

                tree.Insert(book001);
                CollectionAssert.AreEqual(new Book[] { book001, book1, book2, book3, book4, book5 }, BinarySearchTree<Book>.InfixTraverse(tree));
                tree.Insert(book155);
                CollectionAssert.AreEqual(new Book[] { book2, book1, book001, book155, book4, book3, book5 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                tree.Insert(book399);
                CollectionAssert.AreEqual(new Book[] { book001, book155, book1, book399, book3, book5, book4, book2 }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree2")]
            public void InsertComparerTest(BinarySearchTree<Book> tree)
            {
                var book001 = new Book("011", "author", "title", "publisher", DateTime.Now, 200, 1);
                var book155 = new Book("155", "author", "title", "publisher", DateTime.Now, 200, 155);
                var book399 = new Book("399", "author", "title", "publisher", DateTime.Now, 200, 399);

                tree.Insert(book001);
                CollectionAssert.AreEqual(new Book[] { book5, book4, book3, book2, book1, book001 }, BinarySearchTree<Book>.InfixTraverse(tree));
                tree.Insert(book155);
                CollectionAssert.AreEqual(new Book[] { book2, book4, book5, book3, book1, book155, book001 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                tree.Insert(book399);
                CollectionAssert.AreEqual(new Book[] { book5, book399, book3, book4, book155, book001, book1, book2 }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("defaultTree3")]
            public void RemoveDefaultTest(BinarySearchTree<Book> tree)
            {
                Assert.False(tree.Remove(new Book("001", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("122", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("335", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("599", "author", "title", "pub", DateTime.Now, 22, 22)));

                tree.Remove(book2);
                CollectionAssert.AreEqual(new Book[] { book1, book3, book4, book5 }, BinarySearchTree<Book>.InfixTraverse(tree));
                tree.Remove(book4);
                CollectionAssert.AreEqual(new Book[] { book3, book1, book5 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                tree.Remove(book5);
                CollectionAssert.AreEqual(new Book[] { book1, book3, }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree3")]
            public void RemoveComparerTest(BinarySearchTree<Book> tree)
            {
                Assert.False(tree.Remove(new Book("001", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("122", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("335", "author", "title", "pub", DateTime.Now, 22, 22)));
                Assert.False(tree.Remove(new Book("599", "author", "title", "pub", DateTime.Now, 22, 22)));

                tree.Remove(book2);
                CollectionAssert.AreEqual(new Book[] { book5, book4, book3, book1 }, BinarySearchTree<Book>.InfixTraverse(tree));
                tree.Remove(book4);
                CollectionAssert.AreEqual(new Book[] { book1, book3, book5 }, BinarySearchTree<Book>.PrefixTraverse(tree));
                tree.Remove(book5);
                CollectionAssert.AreEqual(new Book[] { book3, book1 }, BinarySearchTree<Book>.PostfixTraverse(tree));
            }
        }

        [TestFixture]
        private class PointTreeTests // System.Windows.Point 
        {
            private static Point point1 = new Point(10, 10);
            private static Point point2 = new Point(20, 20);
            private static Point point3 = new Point(30, 30);
            private static Point point4 = new Point(40, 40);
            private static Point point5 = new Point(50, 50);
            private static Point[] points = new Point[] { point3, point2, point5, point4, point1 };
            private static object[] comparerTree = new[] { new BinarySearchTree<Point>(points, new PointComparer()) };
            private static object[] comparerTree2 = new[] { new BinarySearchTree<Point>(points, new PointComparer()) };
            private static object[] comparerTree3 = new[] { new BinarySearchTree<Point>(points, new PointComparer()) };

            [Test, TestCaseSource("comparerTree")]
            public void TraverseComparerTest(BinarySearchTree<Point> tree)
            {
                CollectionAssert.AreEqual(new Point[] { point1, point2, point3, point4, point5 }, BinarySearchTree<Point>.InfixTraverse(tree));
                CollectionAssert.AreEqual(new Point[] { point3, point2, point1, point5, point4 }, BinarySearchTree<Point>.PrefixTraverse(tree));
                CollectionAssert.AreEqual(new Point[] { point1, point2, point4, point5, point3 }, BinarySearchTree<Point>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree")]
            public void FindComparerTest(BinarySearchTree<Point> tree)
            {
                Assert.Null(tree.Find(new Point(5, 10)));
                Assert.Null(tree.Find(new Point(10, 15)));
                Assert.Null(tree.Find(new Point(40, 10)));
                Assert.Null(tree.Find(new Point(50, 60)));

                CollectionAssert.AreEqual(new Point[] { point4 }, BinarySearchTree<Point>.InfixTraverse(tree.Find(point4)));
                CollectionAssert.AreEqual(new Point[] { point5, point4 }, BinarySearchTree<Point>.PrefixTraverse(tree.Find(point5)));
                CollectionAssert.AreEqual(new Point[] { point1, point2 }, BinarySearchTree<Point>.PostfixTraverse(tree.Find(point2)));
                Assert.AreEqual(tree, tree.Find(point3));
            }

            [Test, TestCaseSource("comparerTree2")]
            public void InsertComparerTest(BinarySearchTree<Point> tree)
            {
                var point15 = new Point(10, 15);
                var point35 = new Point(30, 35);
                var point55 = new Point(55, 55);

                tree.Insert(point15);
                CollectionAssert.AreEqual(new Point[] { point1, point15, point2, point3, point4, point5 }, BinarySearchTree<Point>.InfixTraverse(tree));
                tree.Insert(point35);
                CollectionAssert.AreEqual(new Point[] { point3, point2, point1, point15, point5, point4, point35 }, BinarySearchTree<Point>.PrefixTraverse(tree));
                tree.Insert(point55);
                CollectionAssert.AreEqual(new Point[] { point15, point1, point2, point35, point4, point55, point5, point3 }, BinarySearchTree<Point>.PostfixTraverse(tree));
            }

            [Test, TestCaseSource("comparerTree3")]
            public void RemoveComparerTest(BinarySearchTree<Point> tree)
            {
                Assert.False(tree.Remove(new Point(5, 10)));
                Assert.False(tree.Remove(new Point(10, 15)));
                Assert.False(tree.Remove(new Point(40, 10)));
                Assert.False(tree.Remove(new Point(50, 60)));

                tree.Remove(point3);
                CollectionAssert.AreEqual(new Point[] { point1, point2, point4, point5 }, BinarySearchTree<Point>.InfixTraverse(tree));
                tree.Remove(point1);
                CollectionAssert.AreEqual(new Point[] { point4, point2, point5 }, BinarySearchTree<Point>.PrefixTraverse(tree));
                tree.Remove(point2);
                CollectionAssert.AreEqual(new Point[] { point5, point4 }, BinarySearchTree<Point>.PostfixTraverse(tree));
            }
        }

        private class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y.CompareTo(x);
            }
        }

        private class StringComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return y.CompareTo(x);
            }
        }

        private class BookComparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                return y.ISBN.CompareTo(x.ISBN);
            }
        }

        private class PointComparer : IComparer<Point>
        {
            // compare distances from origin
            public int Compare(Point x, Point y)
            {
                return Math.Sqrt(Math.Pow(x.X, 2) + Math.Pow(x.Y, 2)).CompareTo(Math.Sqrt(Math.Pow(y.X, 2) + Math.Pow(y.Y, 2)));
            }
        }
    }
}
