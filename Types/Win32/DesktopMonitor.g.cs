﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class DesktopMonitor(ManagementObject ManagementObject) : CIM.DesktopMonitor(ManagementObject)
{
    /// <summary>
    /// Name of the monitor manufacturer.
    /// </summary>
    public string? MonitorManufacturer => (string)ManagementObject[nameof(MonitorManufacturer)];
    /// <summary>
    /// Type of monitor.
    /// </summary>
    public string? MonitorType => (string)ManagementObject[nameof(MonitorType)];
    /// <summary>
    /// Resolution along the x-axis (horizontal direction) of the monitor.
    /// </summary>
    public uint? PixelsPerXLogicalInch => (uint?)ManagementObject[nameof(PixelsPerXLogicalInch)];
    /// <summary>
    /// Resolution along the y-axis (vertical direction) of the monitor.
    /// </summary>
    public uint? PixelsPerYLogicalInch => (uint?)ManagementObject[nameof(PixelsPerYLogicalInch)];
}
