//-----------------------------------------------------------------------
// <copyright file="ObjectExtensionsTests.cs" company="Leet">
//     © 2016 Leet. Licensed under the MIT License.
//     See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Leet.Specifications;

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="object"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForObject : ObjectExtensionsSpecification<object>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="DateTime"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForDateTime : ObjectExtensionsSpecification<DateTime>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="DateTimeKind"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForDateTimeKind : ObjectExtensionsSpecification<DateTimeKind>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="Action"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForAction : ObjectExtensionsSpecification<Action>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="Nullable{decimal}"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForNullable : ObjectExtensionsSpecification<Nullable<decimal>>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="IDisposable"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForIDisposable : ObjectExtensionsSpecification<IDisposable>
{
}

/// <summary>
///     A class that tests <see cref="ObjectExtensions"/> class for <see cref="string"/>
///     generic type argument.
/// </summary>
public class ObjectExtensionsTestsForString : ObjectExtensionsSpecification<string>
{
}
