﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class COMApplicationClasses(ManagementObject ManagementObject) : CIM.Component(ManagementObject)
{
    /// <summary>
    /// A Win32_COMClass that represents the COM component grouped under the COM application.
    /// </summary>
    public new COMClass? PartComponent => (COMClass)ManagementObject[nameof(PartComponent)];
    /// <summary>
    /// A Win32_COMApplication that represents the COM application containing the COM component.
    /// </summary>
    public new COMApplication? GroupComponent => (COMApplication)ManagementObject[nameof(GroupComponent)];
}
