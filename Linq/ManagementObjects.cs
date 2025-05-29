namespace System.Management.Linq;
using Types;
using Types.Base;

/// <summary>
/// Provides entry points for querying WMI (Windows Management Instrumentation) objects using strongly-typed LINQ queries.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="ManagementObjects"/> class enables LINQ-to-WQL queries over WMI classes, returning either strongly-typed objects or generic WMI objects.
/// </para>
/// <para>
/// Use <see cref="Get{T}"/> for strongly-typed queries (e.g., <c>ManagementObjects.Get&lt;Process&gt;()</c>), or <see cref="Get(string)"/> for dynamic access by class name.
/// </para>
/// <para>
/// Example usage:
/// <code>
/// using System.Management.Linq;
/// using System.Management.Types.Win32;
///
/// var processes = ManagementObjects.Get&lt;Process&gt;()
///     .Where(p => p.Name == "explorer.exe")
///     .ToList();
/// </code>
/// </para>
/// </remarks>
public static class ManagementObjects
{
    /// <summary>
    /// Returns a queryable sequence of strongly-typed WMI objects for the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The target type representing a WMI class (e.g., <c>System.Management.Types.Win32.Process</c>).
    /// </typeparam>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> for LINQ queries against the WMI class associated with <typeparamref name="T"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Thrown if <typeparamref name="T"/> is not mapped to a WMI class.
    /// </exception>
    /// <remarks>
    /// The mapping between <typeparamref name="T"/> and the WMI class name is resolved via <see cref="InstanceFactory.GetClassNameOf{T}"/>.
    /// </remarks>
    public static IQueryable<T> Get<T>()
        => new ManagementObjectQueryable<T>(InstanceFactory.GetClassNameOf<T>() ?? throw new NotSupportedException($"Type {typeof(T).Name} is not supported by the {typeof(ManagementObjectQueryable<>)}."));

    /// <summary>
    /// Returns a queryable sequence of generic WMI objects for the specified WMI class name.
    /// </summary>
    /// <param name="className">The WMI class name (e.g., <c>"Win32_Process"</c>).</param>
    /// <returns>
    /// An <see cref="IQueryable{_Object}"/> for LINQ queries against the specified WMI class.
    /// </returns>
    /// <remarks>
    /// Use this overload for dynamic or late-bound scenarios where a strongly-typed class is not available.
    /// </remarks>
    public static IQueryable<_Object> Get(string className)
        => new ManagementObjectQueryable<_Object>(className);
}
