﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class ComputerSystem(ManagementObject ManagementObject) : System(ManagementObject)
{
    /// <summary>
    /// Identifies how the computer system name is generated, using a heuristic. The heuristic is outlined, in detail, in the CIM V2 Common Model specification. It assumes that the documented rules are traversed in order, to determine and assign a name. The NameFormat values list defines the precedence order for assigning the computer system name. Several rules do map to the same Value.
    /// </summary>
    public new string? NameFormat => (string)ManagementObject[nameof(NameFormat)];
}
