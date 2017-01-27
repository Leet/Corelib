﻿//-----------------------------------------------------------------------
// <copyright file="DisposableBaseTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Leet;
using Leet.Specifications;

/// <summary>
///     A class that tests <see cref="DisposableBase"/> class in a conformance to
///     behavior specified for <see cref="DisposableBase"/> class.
/// </summary>
public class DisposableBaseTests : DisposableBaseSpecification<DisposableBase>
{
    /// <summary>
    ///     A class that tests <see cref="DisposableBase"/> class in a conformance to
    ///     behavior specified for <see cref="IDisposable"/> interface.
    /// </summary>
    public class AsIDisposable : IDisposableSpecification<DisposableBase>
    {
    }
}
