﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class FloppyController(ManagementObject ManagementObject) : CIM.Controller(ManagementObject)
{
    /// <summary>
    /// Name of the floppy controller manufacturer.
    /// </summary>
    public string? Manufacturer => (string)ManagementObject[nameof(Manufacturer)];
}
