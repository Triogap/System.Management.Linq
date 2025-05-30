﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class DeviceSettings(ManagementObject ManagementObject) : CIM.ElementSetting(ManagementObject)
{
    /// <summary>
    /// A CIM_Setting that represents settings that can be applied to the logical device.
    /// </summary>
    public new CIM.Setting? Setting => (CIM.Setting)ManagementObject[nameof(Setting)];
    /// <summary>
    /// A CIM_LogicalDevice that represents properties of the logical device on which the settings can be applied.
    /// </summary>
    public new CIM.LogicalDevice? Element => (CIM.LogicalDevice)ManagementObject[nameof(Element)];
}
