//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Leet">
//     Copyright (c) 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using Properties;

    /// <summary>
    ///     A <see langword="static"/> class that contains extension methods for <see cref="IEnumerable{T}"/> class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Enumetrates all the elements in the collection.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection to iterate.
        /// </param>
        public static void Iterate<T>(this IEnumerable<T> source)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));

            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                }
            }
        }

        /// <summary>
        ///     Creates a new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection in which the new item shall be inserted.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item shall be inserted.
        /// </param>
        /// <param name="item">
        ///     A new item to insert at the specified location.
        /// </param>
        /// <returns>
        ///     A new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </returns>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));
            Contract.Requires(insertAt >= 0);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (insertAt < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(insertAt), insertAt, Resources.Exception_ArgumentOutOfRange_NegativeValue);
            }

            return InsertCore(source, insertAt, item);
        }

        /// <summary>
        ///     Creates a new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection in which the new item shall be inserted.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item shall be inserted.
        /// </param>
        /// <param name="item">
        ///     A new item to insert at the specified location.
        /// </param>
        /// <returns>
        ///     A new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </returns>
        private static IEnumerable<T> InsertCore<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));
            Contract.Requires(insertAt >= 0);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            
            int index = 0;

            foreach (T oldItem in source)
            {
                if (index++ == insertAt)
                {
                    yield return item;
                }

                yield return oldItem;
            }

            if (index == insertAt)
            {
                yield return item;
            }
            else if (index < insertAt)
            {
                throw new ArgumentOutOfRangeException(nameof(insertAt), insertAt, Resources.Exception_ArgumentOutOfRange_InsertionIndexGreaterThanCollectionSize);
            }
        }
    }
}
