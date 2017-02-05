// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparer{T}.cs" company="Leet">
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

    /// <summary>
    ///     Provides a mechanism for comparison of the two <see cref="IEnumerable{T}"/> sequences on by-element basis.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects contained in the <see cref="IEnumerable{T}"/> collection.
    /// </typeparam>
    public class SequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        /// <summary>
        ///     Holds a read-only reference to the collection item's equality comparer.
        /// </summary>
        private readonly IEqualityComparer<T> itemComparer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SequenceEqualityComparer{T}"/> class with the default item's equality comparer.
        /// </summary>
        public SequenceEqualityComparer()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SequenceEqualityComparer{T}"/> class with the specified item's equality comparer.
        /// </summary>
        /// <param name="itemComparer">
        ///     A collection item's comparer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="itemComparer"/> is <see langword="null"/>.
        /// </exception>
        public SequenceEqualityComparer(IEqualityComparer<T> itemComparer)
        {
            Contract.Requires(!object.ReferenceEquals(itemComparer, null));
            Contract.Ensures(object.ReferenceEquals(this.ItemComparer, itemComparer));

            if (object.ReferenceEquals(itemComparer, null))
            {
                throw new ArgumentNullException(nameof(itemComparer));
            }

            this.itemComparer = itemComparer;
        }

        /// <summary>
        ///     Gets the collection item's equality comparer.
        /// </summary>
        public IEqualityComparer<T> ItemComparer
        {
            get
            {
                return this.itemComparer;
            }
        }

        /// <summary>
        ///     Determines whether the specified <see cref="IEnumerable{T}"/> collections are equal.
        /// </summary>
        /// <param name="x">
        ///     The first collection to compare.
        /// </param>
        /// <param name="y">
        ///     The second collection to compare.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified collections are equal;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.ReferenceEquals(x, null) ||
                object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.SequenceEqual(y, this.itemComparer);
        }

        /// <summary>
        ///     Returns a hash code for the specified collection using its first 4 (or less if not available) elements.
        /// </summary>
        /// <param name="obj">
        ///     The collection for which a hash code is to be returned.
        /// </param>
        /// <returns>
        ///     A hash code for the specified collection.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        public int GetHashCode(IEnumerable<T> obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return obj.Take(4).Aggregate(0, (acc, v) => acc << 8 ^ this.itemComparer.GetHashCode(v));
        }
    }
}
