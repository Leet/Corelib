// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is greater than <paramref name="source"/> collection size.
        /// </exception>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));
            Contract.Requires(insertAt >= 0);
            Contract.Requires(insertAt <= source.Count());
            Contract.Ensures(!object.ReferenceEquals(Contract.Result<IEnumerable<T>>(), null));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Count() == source.Count() + 1);
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Take(insertAt).SequenceEqual(source.Take(insertAt)));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().ElementAt(insertAt).Equals(item));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Skip(insertAt + 1).SequenceEqual(source.Skip(insertAt)));

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
        ///     Creates a cartesian product of the specified power using a specified collection as a source.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection for the cartesian product.
        /// </param>
        /// <param name="power">
        ///     A power of the cartesian product to create.
        /// </param>
        /// <returns>
        ///     A cartesian product of the specified collection and specified power.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="power"/> is less than zero.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<T> source, int power)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));
            Contract.Requires(power >= 0);
            Contract.Ensures(!object.ReferenceEquals(Contract.Result<IEnumerable<IEnumerable<T>>>(), null));

            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power), power, Resources.Exception_ArgumentOutOfRange_NegativeValue);
            }

            return Enumerable.Repeat(source, power).CartesianProduct();
        }

        /// <summary>
        ///     Creates a cartesian product of the two collections.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="first">
        ///     A first enumerable collection for cartesian product.
        /// </param>
        /// <param name="second">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        /// <returns>
        ///     A cartesian product of the two collections.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="first"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="second"/> is <see langword="null"/>.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            Contract.Requires(!object.ReferenceEquals(first, null));
            Contract.Requires(!object.ReferenceEquals(second, null));
            Contract.Ensures(!object.ReferenceEquals(Contract.Result<IEnumerable<IEnumerable<T>>>(), null));

            if (object.ReferenceEquals(first, null))
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (object.ReferenceEquals(second, null))
            {
                throw new ArgumentNullException(nameof(second));
            }

            return from f in first
                   from s in second
                   select new[] { f, s };
        }

        /// <summary>
        ///     Creates a cartesian product of the collection of source sequences.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="collections">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        /// <returns>
        ///     A cartesian product of the collection of source sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="collections"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="collections"/> contains <see langword="null"/> element.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            Contract.Requires(!object.ReferenceEquals(collections, null));
            Contract.Requires(Contract.ForAll(collections, collection => !object.ReferenceEquals(collection, null)));
            Contract.Ensures(!object.ReferenceEquals(Contract.Result<IEnumerable<IEnumerable<T>>>(), null));

            if (object.ReferenceEquals(collections, null))
            {
                throw new ArgumentNullException(nameof(collections));
            }

            if (!collections.Any() || collections.Any(collection => !collection.Any()))
            {
                return Enumerable.Empty<IEnumerable<T>>();
            }

            IEnumerable<IEnumerable<T>> emptyProduct =
              new[] { Enumerable.Empty<T>() };

            return collections.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
              {
                  if (object.ReferenceEquals(sequence, null))
                  {
                      throw new ArgumentException(Resources.Exception_Argument_CollectionElementNull, nameof(collections));
                  }

                  return from accseq in accumulator
                         from item in sequence
                         select accseq.Concat(new[] { item });
              });
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
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is greater than <paramref name="source"/> collection size.
        /// </exception>
        private static IEnumerable<T> InsertCore<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            Contract.Requires(!object.ReferenceEquals(source, null));
            Contract.Requires(insertAt >= 0);
            Contract.Requires(insertAt <= source.Count());
            Contract.Ensures(!object.ReferenceEquals(Contract.Result<IEnumerable<T>>(), null));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Count() == source.Count() + 1);
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Take(insertAt).SequenceEqual(source.Take(insertAt)));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().ElementAt(insertAt).Equals(item));
            Contract.Ensures(Contract.Result<IEnumerable<T>>().Skip(insertAt + 1).SequenceEqual(source.Skip(insertAt)));

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
