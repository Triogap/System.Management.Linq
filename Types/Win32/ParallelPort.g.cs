﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class ParallelPort(ManagementObject ManagementObject) : CIM.ParallelController(ManagementObject)
{
    /// <summary>
    /// If TRUE, the parallel port was automatically detected by the operating system. If FALSE, the port was detected by other means (such as being manually added through the Control Panel).
    /// </summary>
    public bool? OSAutoDiscovered => (bool?)ManagementObject[nameof(OSAutoDiscovered)];
}
