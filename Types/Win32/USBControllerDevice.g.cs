﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class USBControllerDevice(ManagementObject ManagementObject) : CIM.ControlledBy(ManagementObject)
{
    /// <summary>
    /// A CIM_USBController representing the Universal Serial Bus (USB) controller associated with this device.
    /// </summary>
    public new CIM.USBController? Antecedent => (CIM.USBController)ManagementObject[nameof(Antecedent)];
    /// <summary>
    /// A CIM_LogicalDevice describing the logical device connected to the Universal Serial Bus (USB) controller.
    /// </summary>
    public new CIM.LogicalDevice? Dependent => (CIM.LogicalDevice)ManagementObject[nameof(Dependent)];
}
