// -----------------------------------------------------------------------
// <copyright file="RandomMultipleRelay.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using Ploeh.AutoFixture.Kernel;
    using Properties;

    /// <summary>
    ///     Relays requests for multiple (an unspecified count) specimens to a request for a random number of specimens.
    /// </summary>
    public class RandomMultipleRelay : ISpecimenBuilder
    {
        /// <summary>
        ///     Holds a read-only reference to the random number generator
        /// </summary>
        private readonly Random random = new Random(DateTimeOffset.UtcNow.Millisecond);

        /// <summary>
        ///     Holds a minimum inclusive numbers of element to draw from.
        /// </summary>
        private readonly int minInclusiveCount;

        /// <summary>
        ///     Holds a maximum exclusive numbers of element to draw from.
        /// </summary>
        private readonly int maxExclusiveCount;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RandomMultipleRelay" /> class.
        /// </summary>
        /// <param name="minInclusiveCount">
        ///     A minimum inclusive numbers of element to draw from.
        /// </param>
        /// <param name="maxExclusiveCount">
        ///     A maximum exclusive numbers of element to draw from.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="minInclusiveCount"/> is less than zero
        ///     <para>-or-</para>
        ///     <paramref name="maxExclusiveCount"/> is less than or equals to <paramref name="minInclusiveCount"/>.
        /// </exception>
        public RandomMultipleRelay(int minInclusiveCount, int maxExclusiveCount)
        {
            if (minInclusiveCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minInclusiveCount), minInclusiveCount, TestResources.Exceptions_ArgumentOutOfRange_MinimumInclusiveCountRangeNegative);
            }

            if (maxExclusiveCount <= minInclusiveCount)
            {
                throw new ArgumentOutOfRangeException(nameof(maxExclusiveCount), maxExclusiveCount, TestResources.Exceptions_ArgumentOutOfRange_MaximumExclusiveCountRangeLessThanOrEqualToMinimum);
            }

            this.minInclusiveCount = minInclusiveCount;
            this.maxExclusiveCount = maxExclusiveCount;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RandomMultipleRelay" /> class with minimum inclusive count range set to 1 and
        ///     maximum exclusive count range set to 4.
        /// </summary>
        public RandomMultipleRelay()
            : this(1, 4)
        {
        }

        /// <summary>
        ///     Gets the minimum inclusive numbers of element to draw from.
        /// </summary>
        public int MinInclusiveCount
        {
            get
            {
                return this.minInclusiveCount;
            }
        }

        /// <summary>
        ///     Gets the maximum exclusive numbers of element to draw from.
        /// </summary>
        public int MaxExclusiveCount
        {
            get
            {
                return this.maxExclusiveCount;
            }
        }

        /// <summary>
        ///     Creates many new specimens based on a request.
        /// </summary>
        /// <param name="request">
        ///     The request that describes what to create.
        /// </param>
        /// <param name="context">
        ///     A context that can be used to create other specimens.
        /// </param>
        /// <returns>
        ///     The requested specimens if possible;
        ///     otherwise a <see cref="NoSpecimen" /> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            if (object.ReferenceEquals(context, null))
            {
                throw new ArgumentNullException(nameof(context));
            }

            MultipleRequest multipleRequest = request as MultipleRequest;
            if (object.ReferenceEquals(multipleRequest, null))
            {
                return new NoSpecimen();
            }

            return context.Resolve(new FiniteSequenceRequest(multipleRequest.Request, this.minInclusiveCount + (this.random.Next() % (this.maxExclusiveCount - this.minInclusiveCount))));
        }
    }
}
