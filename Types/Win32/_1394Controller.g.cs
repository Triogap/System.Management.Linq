﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class _1394Controller(ManagementObject ManagementObject) : CIM.Controller(ManagementObject)
{
    /// <summary>
    /// Manufacturer of the controller.
    /// </summary>
    public string? Manufacturer => (string)ManagementObject[nameof(Manufacturer)];
}
