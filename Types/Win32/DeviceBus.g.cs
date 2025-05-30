﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class DeviceBus(ManagementObject ManagementObject) : CIM.Dependency(ManagementObject)
{
    /// <summary>
    /// A CIM_LogicalDevice that describes the properties of the logical device that is using the system bus.
    /// </summary>
    public new CIM.LogicalDevice? Dependent => (CIM.LogicalDevice)ManagementObject[nameof(Dependent)];
    /// <summary>
    /// A Win32_Bus that describes the properties of the system bus that is used by the logical device.
    /// </summary>
    public new Bus? Antecedent => (Bus)ManagementObject[nameof(Antecedent)];
}
