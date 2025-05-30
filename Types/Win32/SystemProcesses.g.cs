﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class SystemProcesses(ManagementObject ManagementObject) : CIM.SystemComponent(ManagementObject)
{
    /// <summary>
    /// Reference to the instance representing the computer system upon which the process is running.
    /// </summary>
    public new ComputerSystem? GroupComponent => (ComputerSystem)ManagementObject[nameof(GroupComponent)];
    /// <summary>
    /// Reference to the instance representing the process running on the computer system.
    /// </summary>
    public new Process? PartComponent => (Process)ManagementObject[nameof(PartComponent)];
}
