//-----------------------------------------------------------------------
// <copyright file="ArraySegmentExtensionsTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Leet;
using Leet.Specifications;

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="object"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForObject : ArraySegmentExtensionsSpecification<object>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="DateTime"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForDateTime : ArraySegmentExtensionsSpecification<DateTime>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="DateTimeKind"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForDateTimeKind : ArraySegmentExtensionsSpecification<DateTimeKind>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="Action"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForAction : ArraySegmentExtensionsSpecification<Action>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="Nullable{T}">Nullable&lt;decimal></see>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForNullable : ArraySegmentExtensionsSpecification<Nullable<decimal>>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="IDisposable"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForIDisposable : ArraySegmentExtensionsSpecification<IDisposable>
{
}

/// <summary>
///     A class that tests <see cref="ArraySegmentExtensions"/> class for <see cref="string"/>
///     generic type argument.
/// </summary>
public class ArraySegmentExtensionsTestsForString : ArraySegmentExtensionsSpecification<string>
{
}
