//-----------------------------------------------------------------------
// <copyright file="DomainFixture.cs" company="Leet">
//     Copyright (c) 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace Leet
{
    using Ploeh.AutoFixture;

    /// <summary>
    ///     Provides anonymous object creation services with an additional knowledge of the current problem domain.
    /// </summary>
    public class DomainFixture : Fixture
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainFixture"/> class.
        /// </summary>
        public DomainFixture()
        {
            this.Customize(new DomainCustomization());
        }
    }
}
