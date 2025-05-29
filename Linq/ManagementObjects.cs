namespace System.Management.Linq;
using Types;
using Types.Base;

public static class ManagementObjects
{
    public static IQueryable<T> Get<T>()
        => new ManagementObjectQueryable<T>(InstanceFactory.GetClassNameOf<T>() ?? throw new NotSupportedException($"Type {typeof(T).Name} is not supported by the {typeof(ManagementObjectQueryable<>)}."));

    public static IQueryable<_Object> Get(string className)
        => new ManagementObjectQueryable<_Object>(className);
}
