//-----------------------------------------------------------------------
// <copyright file="IDisposableSpecification{TSut}.cs" company="Leet">
//     Copyright (c) 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IDisposable"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IDisposable"/> interface.
    /// </typeparam>
    public abstract class IDisposableSpecification<TSut>
        where TSut : IDisposable
    {
        /// <summary>
        ///     Checks whether <see cref="IDisposable.Dispose"/> method never throw exception upon multiple calls.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Theory]
        [AutoDomainData]
        public void Dispose_Never_Throws(TSut sut)
        {
            // Fixture setup

            // Exercise system
            sut.Dispose();
            sut.Dispose();
            sut.Dispose();

            // Verify outcome

            // Teardown
        }
    }
}
