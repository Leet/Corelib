//-----------------------------------------------------------------------
// <copyright file="EnumerableExtensionsTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Leet.Specifications;

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="object"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForObject : EnumerableExtensionsSpecification<object>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="DateTime"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForDateTime : EnumerableExtensionsSpecification<DateTime>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="DateTimeKind"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForDateTimeKind : EnumerableExtensionsSpecification<DateTimeKind>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="Action"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForAction : EnumerableExtensionsSpecification<Action>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="Nullable{decimal}"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForNullable : EnumerableExtensionsSpecification<Nullable<decimal>>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="IDisposable"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForIDisposable : EnumerableExtensionsSpecification<IDisposable>
{
}

/// <summary>
///     A class that tests <see cref="EnumerableExtensions"/> class for <see cref="Nullable{decimal}"/>
///     generic type argument.
/// </summary>
public class EnumerableExtensionsTestsForString : EnumerableExtensionsSpecification<string>
{
}
