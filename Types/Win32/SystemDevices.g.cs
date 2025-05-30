﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class SystemDevices(ManagementObject ManagementObject) : CIM.SystemDevice(ManagementObject)
{
    /// <summary>
    /// Reference to the instance representing the properties of the computer system where the logical device exists.
    /// </summary>
    public new ComputerSystem? GroupComponent => (ComputerSystem)ManagementObject[nameof(GroupComponent)];
    /// <summary>
    /// Reference to the instance representing the properties of a logical device that exists on the computer system.
    /// </summary>
    public new CIM.LogicalDevice? PartComponent => (CIM.LogicalDevice)ManagementObject[nameof(PartComponent)];
}
