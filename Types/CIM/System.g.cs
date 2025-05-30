﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class System(ManagementObject ManagementObject) : LogicalElement(ManagementObject)
{
    /// <summary>
    /// Name of the class or subclass used in the creation of an instance. When used with other key properties of the class, this property allows all instances of the class and its subclasses to be uniquely identified.
    /// </summary>
    public string? CreationClassName => (string)ManagementObject[nameof(CreationClassName)];
    /// <summary>
    /// Defines the label by which the object is known.
    /// </summary>
    public new string? Name => (string)ManagementObject[nameof(Name)];
    /// <summary>
    /// Identifies how the system name was generated, using the subclass heuristic.
    /// </summary>
    public string? NameFormat => (string)ManagementObject[nameof(NameFormat)];
    /// <summary>
    /// How the primary system owner can be reached (for example, phone number or email address).
    /// </summary>
    public string? PrimaryOwnerContact => (string)ManagementObject[nameof(PrimaryOwnerContact)];
    /// <summary>
    /// Name of the primary system owner.
    /// </summary>
    public string? PrimaryOwnerName => (string)ManagementObject[nameof(PrimaryOwnerName)];
    /// <summary>
    /// Roles the system plays in the information technology environment. Subclasses of the system can override this property to define explicit role values. Alternately, a working group can describe the heuristics, conventions, and guidelines for specifying roles. For example, for an instance of a networking system, this property might contain the string "Switch" or "Bridge".
    /// </summary>
    public string[]? Roles => (string[])ManagementObject[nameof(Roles)];
}
