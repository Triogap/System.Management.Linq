﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class UserDevice(ManagementObject ManagementObject) : LogicalDevice(ManagementObject)
{
    /// <summary>
    /// If TRUE, the device is locked, preventing user input or output.
    /// </summary>
    public bool? IsLocked => (bool?)ManagementObject[nameof(IsLocked)];
}
