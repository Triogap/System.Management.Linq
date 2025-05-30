﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class PhysicalFrame(ManagementObject ManagementObject) : PhysicalPackage(ManagementObject)
{
    /// <summary>
    /// If TRUE, the frame is equipped with an audible alarm.
    /// </summary>
    public bool? AudibleAlarm => (bool?)ManagementObject[nameof(AudibleAlarm)];
    /// <summary>
    /// Free-form string that provides more information if the SecurityBreach property indicates that a breach or some other security-related event occurred.
    /// </summary>
    public string? BreachDescription => (string)ManagementObject[nameof(BreachDescription)];
    /// <summary>
    /// Free-form string that contains information on how the various cables are connected and bundled for the frame. With many networking, storage-related, and power cables, cable management can be a complex and challenging endeavor. This string property contains information to aid in assembly and service of the frame.
    /// </summary>
    public string? CableManagementStrategy => (string)ManagementObject[nameof(CableManagementStrategy)];
    /// <summary>
    /// If TRUE, the frame is protected with a lock.
    /// </summary>
    public bool? LockPresent => (bool?)ManagementObject[nameof(LockPresent)];
    /// <summary>
    /// Indicates whether a physical breach of the frame was attempted but unsuccessful, or attempted and successful.
    /// </summary>
    public ushort? SecurityBreach => (ushort?)ManagementObject[nameof(SecurityBreach)];
    /// <summary>
    /// Free-form strings that provide detailed explanations for entries in the ServicePhilosophy array.
    /// </summary>
    public string[]? ServiceDescriptions => (string[])ManagementObject[nameof(ServiceDescriptions)];
    /// <summary>
    /// Indicates whether the frame is serviced from the top, front, back, or side; and whether it has sliding trays or removable sides, and whether the frame is moveable (for example, having rollers).
    /// </summary>
    public ushort[]? ServicePhilosophy => (ushort[])ManagementObject[nameof(ServicePhilosophy)];
    /// <summary>
    /// If TRUE, the equipment includes a visible alarm.
    /// </summary>
    public bool? VisibleAlarm => (bool?)ManagementObject[nameof(VisibleAlarm)];
}
