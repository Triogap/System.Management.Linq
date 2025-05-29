using System.Linq.Expressions;
using System.Reflection;

namespace System.Management.Types;

/// <summary>
/// Provides a factory for creating strongly-typed instances from WMI <see cref="ManagementObject"/> data.
/// <para>
/// The <c>InstanceFactory</c> caches and uses reflection to construct objects of a specified type
/// that expose a constructor accepting a <see cref="ManagementObject"/>. It supports mapping between
/// WMI class names and .NET types, and enables efficient instantiation of objects from WMI queries.
/// </para>
/// </summary>
public record struct InstanceFactory(Type TargetType, string ClassName, Func<ManagementObject, object> Constructor)
{
    private static readonly Dictionary<Type, InstanceFactory?> _factoriesByType = [];
    private static readonly Dictionary<string, InstanceFactory?> _factoriesByClassName = [];

    /// <summary>
    /// Determines whether an instance of the specified generic type <typeparamref name="T"/> can be created from a <see cref="ManagementObject"/>.
    /// </summary>
    /// <typeparam name="T">The target type to check for instantiation support.</typeparam>
    /// <returns><c>true</c> if an instance can be created; otherwise, <c>false</c>.</returns>
    public static bool CanCreateInstanceOf<T>()
        => CanCreateInstanceOf(typeof(T));

    /// <summary>
    /// Determines whether an instance of the specified <see cref="Type"/> can be created from a <see cref="ManagementObject"/>.
    /// </summary>
    /// <param name="type">The target type to check for instantiation support.</param>
    /// <returns><c>true</c> if an instance can be created; otherwise, <c>false</c>.</returns>
    public static bool CanCreateInstanceOf(Type type)
        => GetFactory(type) != null;

    /// <summary>
    /// Gets the WMI class name associated with the specified generic type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type whose WMI class name is to be retrieved.</typeparam>
    /// <returns>The WMI class name if available; otherwise, <c>null</c>.</returns>
    public static string? GetClassNameOf<T>()
        => GetClassNameOf(typeof(T));

    /// <summary>
    /// Gets the WMI class name associated with the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The type whose WMI class name is to be retrieved.</param>
    /// <returns>The WMI class name if available; otherwise, <c>null</c>.</returns>
    public static string? GetClassNameOf(Type type)
        => GetFactory(type)?.ClassName;

    /// <summary>
    /// Creates an instance of type <typeparamref name="T"/> from the specified <see cref="ManagementObject"/>.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <param name="managementObject">The <see cref="ManagementObject"/> to use for instantiation.</param>
    /// <returns>An instance of type <typeparamref name="T"/> created from the management object.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an instance cannot be created for the specified type and class.</exception>
    public static T CreateInstance<T>(ManagementObject managementObject)
    {
        var factory = GetFactory(managementObject.ClassPath.ClassName);
        if (factory == null)
        {
            if (managementObject.GetSystemProperties().Derivations is string[] derivations)
            {
                foreach (var className in derivations)
                {
                    factory = GetFactory(className);
                    if (factory != null)
                    {
                        foreach (var derivation in derivations)
                        {
                            if (derivation == className)
                            {
                                break;
                            }
                            _factoriesByClassName[derivation] = factory;
                        }
                        _factoriesByClassName[className] = factory;
                        break;
                    }
                }
            }

            if (factory == null)
            {
                throw new InvalidOperationException($"Unable to create instance of type {typeof(T)} with class {managementObject.ClassPath.ClassName}.");
            }
        }
        return factory.Value.Create<T>(managementObject);
    }

    /// <summary>
    /// Creates a sequence of instances of type <typeparamref name="T"/> from a <see cref="ManagementObjectCollection"/>.
    /// </summary>
    /// <typeparam name="T">The type of objects to create.</typeparam>
    /// <param name="managementObjectCollection">The collection of <see cref="ManagementObject"/> instances.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of created instances.</returns>
    public static IEnumerable<T> CreateInstances<T>(ManagementObjectCollection managementObjectCollection)
        => managementObjectCollection.Cast<ManagementObject>().Select(CreateInstance<T>);

    private static InstanceFactory? GetFactory(Type type)
    {
        if (!_factoriesByType.TryGetValue(type, out var factory))
        {
            factory = CreateFactory(type, GetClassName(type));
        }
        return factory;
        
        static string GetClassName(Type type)
            => $"{type.Namespace?.Split('.')[^1]}_{type.Name}";
    }

    private static InstanceFactory? GetFactory(string className)
    {
        if (!_factoriesByClassName.TryGetValue(className, out var factory))
        {
            factory = CreateFactory(GetType(className), className);
        }
        return factory;


        static Type? GetType(string className)
            => Assembly.GetAssembly(typeof(InstanceFactory))?.GetType($"{nameof(System)}.{nameof(Management)}.{nameof(Types)}.{className.Replace('_', '.')}");
    }

    private static InstanceFactory? CreateFactory(Type? type, string className)
    {
        if (type == null)
        {
            return _factoriesByClassName[className] = null;
        }


        var constructorInfo = type.GetConstructor([typeof(ManagementObject)]);
        if (constructorInfo == null)
        {
            return _factoriesByClassName[className] = _factoriesByType[type] = null;
        }

        var parameters = constructorInfo.GetParameters().Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
        var newExpression = Expression.New(constructorInfo, parameters);
        var lambda = Expression.Lambda<Func<ManagementObject, object>>(newExpression, parameters);
        return _factoriesByClassName[className] = _factoriesByType[type] = new(type, className, lambda.Compile());
    }

    private readonly T Create<T>(ManagementObject managementObject)
    {
        if (!TargetType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException($"Unable to construct object of type {typeof(T)} with {nameof(ManagementObject)} of class {managementObject.ClassPath.ClassName}.");
        }

        return (T)Constructor(managementObject);
    }
}
