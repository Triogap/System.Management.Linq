﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class OperatingSystemAutochkSetting(ManagementObject ManagementObject) : CIM.ElementSetting(ManagementObject)
{
    /// <summary>
    /// TBD
    /// </summary>
    public new OperatingSystem? Element => (OperatingSystem)ManagementObject[nameof(Element)];
    /// <summary>
    /// TBD
    /// </summary>
    public new AutochkSetting? Setting => (AutochkSetting)ManagementObject[nameof(Setting)];
}
