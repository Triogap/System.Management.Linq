﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class SystemServices(ManagementObject ManagementObject) : CIM.SystemComponent(ManagementObject)
{
    /// <summary>
    /// Reference to the instance representing the properties of the computer system on which the service exists.
    /// </summary>
    public new ComputerSystem? GroupComponent => (ComputerSystem)ManagementObject[nameof(GroupComponent)];
    /// <summary>
    /// Reference to the instance representing the service that exists on the computer system.
    /// </summary>
    public new Service? PartComponent => (Service)ManagementObject[nameof(PartComponent)];
}
