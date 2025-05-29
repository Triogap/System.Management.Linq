using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Management.Types;

public record struct InstanceFactory(Type TargetType, string ClassName, Func<ManagementObject, object> Constructor)
{
    private static readonly Dictionary<Type, InstanceFactory?> _factoriesByType = [];
    private static readonly Dictionary<string, InstanceFactory?> _factoriesByClassName = [];

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

    public static bool CanCreateInstanceOf<T>()
        => CanCreateInstanceOf(typeof(T));

    public static bool CanCreateInstanceOf(Type type)
        => GetFactory(type) != null;

    public static string? GetClassNameOf<T>()
        => GetClassNameOf(typeof(T));

    public static string? GetClassNameOf(Type type)
        => GetFactory(type)?.ClassName;

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

    public static IEnumerable<T> CreateInstances<T>(ManagementObjectCollection managementObjectCollection)
        => managementObjectCollection.Cast<ManagementObject>().Select(CreateInstance<T>);

    private readonly T Create<T>(ManagementObject managementObject)
    {
        if (!TargetType.IsAssignableTo(typeof(T)))
        {
            throw new InvalidOperationException($"Unable to construct object of type {typeof(T)} with {nameof(ManagementObject)} of class {managementObject.ClassPath.ClassName}.");
        }

        return (T)Constructor(managementObject);
    }
}
