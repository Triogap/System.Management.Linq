namespace System.Management.Types;

/// <summary>
/// Defines a set of common WMI system properties exposed by WMI classes and instances,
/// such as class name, namespace, server, and inheritance information.
/// Implementations provide access to metadata describing WMI objects.
/// </summary>
public interface ISystemProperties
{
    /// <summary>
    /// The name of the class.
    /// </summary>
    public string ClassName { get; }

    /// <summary>
    /// Class hierarchy of the current class or instance. The first element is the immediate parent class, the next is its parent, and so on; the last element is the base class.
    /// </summary>
    public string[] Derivations { get; }

    /// <summary>
    /// Name of the top-level class from which the class or instance is derived. When this class or instance is the top-level class, the values of Dynasty and Class are the same.
    /// </summary>
    public string Dynasty { get; }

    /// <summary>
    /// Value that indicates if this object is a class definition
    /// </summary>
    public bool IsClass { get; }

    /// <summary>
    /// Value that indicates if this object is an instance
    /// </summary>
    public bool IsInstance { get; }

    /// <summary>
    /// Name of the namespace of the class or instance.
    /// </summary>
    public string Namespace { get; }

    /// <summary>
    /// Full path to the class or instance—including server and namespace.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Number of nonsystem properties defined for the class or instance.
    /// </summary>
    public int PropertyCount { get; }

    /// <summary>
    /// Relative path to the class or instance.
    /// </summary>
    public string RelativePath { get; }

    /// <summary>
    /// Name of the server supplying the class or instance.
    /// </summary>
    public string Server { get; }

    /// <summary>
    /// Name of the immediate parent class of the class or instance.
    /// </summary>
    public string? Superclass { get; }
}
