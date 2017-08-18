using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmSharp.IO.Binary.Test
{
    /// <summary>
    /// A collection of extra asserts.
    /// </summary>
    public static class ExtraAssert
    {
        /// <summary>
        /// Compares to enumerables and their content(s).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            ExtraAssert.AreEqual(expected, actual, (e1, e2) =>
            {
                Assert.AreEqual(e1, e2);
            });
        }

        /// <summary>
        /// Compares to enumerables and their content(s).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual,
            Action<T, T> compare)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            if (expected == null)
            {
                Assert.IsNull(actual);
                return;
            }
            Assert.IsNotNull(actual);
            var enum1 = expected.GetEnumerator();
            var enum2 = actual.GetEnumerator();
            while (enum1.MoveNext())
            {
                Assert.IsTrue(enum2.MoveNext(), "# of elements different");
                compare(enum1.Current, enum2.Current);
            }
            Assert.IsFalse(enum2.MoveNext(), "# of elements different");
        }
    }
}
