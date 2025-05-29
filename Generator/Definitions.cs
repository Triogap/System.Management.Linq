namespace System.Management.Generator; 

public record struct ClassDefinition(string ClassName, string? SuperClass)
{
    public required List<PropertyDefinition> Properties { get; init; }
    public required List<MethodDefinition> Methods { get; init; }
}

public record struct PropertyDefinition(CimType Type, bool IsArray, string Name, string? ReferencedClass = null)
{
    public bool ReadOnly { get; init; } = true;
    public string? Description { get; init; }
    public List<QualifierDefinition>? Qualifiers { get; init; }
}

public record struct QualifierDefinition(string Name, string Value);

public record struct MethodDefinition(CimType ReturnType, string Name)
{
    public required List<PropertyDefinition> Parameters { get; init; }
}

public enum CimType
{
    None = 0,
    SInt16 = 2,
    SInt32 = 3,
    Real32 = 4,
    Real64 = 5,
    String = 8,
    Boolean = 11,
    Object = 13,
    SInt8 = 16,
    UInt8 = 17,
    UInt16 = 18,
    UInt32 = 19,
    SInt64 = 20,
    UInt64 = 21,
    DateTime = 101,
    Reference = 102,
    Char16 = 103,
}