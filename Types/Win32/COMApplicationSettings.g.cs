﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class COMApplicationSettings(ManagementObject ManagementObject) : CIM.ElementSetting(ManagementObject)
{
    /// <summary>
    /// A Win32_DCOMApplicationSetting that represents the configuration settings associated with the DCOM application.
    /// </summary>
    public new DCOMApplicationSetting? Setting => (DCOMApplicationSetting)ManagementObject[nameof(Setting)];
    /// <summary>
    /// A Win32_DCOMApplication that represents the DCOM application where the settings are applied.
    /// </summary>
    public new DCOMApplication? Element => (DCOMApplication)ManagementObject[nameof(Element)];
}
