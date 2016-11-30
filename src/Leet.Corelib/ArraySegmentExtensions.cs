//-----------------------------------------------------------------------
// <copyright file="ArraySegmentExtensions.cs" company="Leet">
//     Copyright (c) 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    ///     A <see langword="static"/> class that contains extension methods for <see cref="ArraySegment{T}"/> class.
    /// </summary>
    public static class ArraySegmentExtensions
    {
        /// <summary>
        ///     Creates an enumerable collection that represents a sequence of the array segment elements.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the array segment elements.
        /// </typeparam>
        /// <param name="segment">
        ///     Array segment to enumerate.
        /// </param>
        /// <returns>
        ///     Enumerable collection that represents a specified array segment.
        /// </returns>
        public static IEnumerable<T> ToEnumerable<T>(this ArraySegment<T> segment)
        {
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            if (object.ReferenceEquals(segment.Array, null))
            {
                yield break;
            }
            int limit = segment.Offset + segment.Count;
            for (int i = segment.Offset; i < limit; ++i)
            {
                yield return segment.Array[i];
            }
        }
    }
}
