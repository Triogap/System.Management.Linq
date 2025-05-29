namespace System.Management.Types.Base;

/// <summary>
/// Base wrapper for all <see cref="Management.ManagementObject"/> instances implementing the <see cref="ISystemProperties"/> interface explicitly.
/// </summary>
public record class _Object(ManagementObject ManagementObject) : ISystemProperties
{
    string ISystemProperties.ClassName => (string)ManagementObject["__Class"];

    string[] ISystemProperties.Derivations => (string[])ManagementObject["__Derivation"];

    string ISystemProperties.Dynasty => (string)ManagementObject["__Dynasty"];

    bool ISystemProperties.IsClass => 1.Equals(ManagementObject["__Genus"]);

    bool ISystemProperties.IsInstance => 2.Equals(ManagementObject["__Genus"]);

    string ISystemProperties.Namespace => (string)ManagementObject["__Namespace"];

    string ISystemProperties.Path => (string)ManagementObject["__Path"];

    int ISystemProperties.PropertyCount => (int)ManagementObject["__Property_Count"];

    string ISystemProperties.RelativePath => (string)ManagementObject["__Relpath"];

    string ISystemProperties.Server => (string)ManagementObject["__Server"];

    string? ISystemProperties.Superclass => (string)ManagementObject["__Superclass"];

    public override int GetHashCode()
        => ManagementObject.GetHashCode();

    public override string ToString()
        => ManagementObject.ToString();
}
