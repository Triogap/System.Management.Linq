﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class MappedLogicalDisk(ManagementObject ManagementObject) : CIM.LogicalDisk(ManagementObject)
{
    /// <summary>
    /// If True, the file is compressed.
    /// </summary>
    public bool? Compressed => (bool?)ManagementObject[nameof(Compressed)];
    /// <summary>
    /// File system on the logical disk.
    /// </summary>
    public string? FileSystem => (string)ManagementObject[nameof(FileSystem)];
    /// <summary>
    /// Contains the maximum length of a file-name component supported by the Windows drive.
    /// </summary>
    public uint? MaximumComponentLength => (uint?)ManagementObject[nameof(MaximumComponentLength)];
    /// <summary>
    /// Network path name to the logical device.
    /// </summary>
    public string? ProviderName => (string)ManagementObject[nameof(ProviderName)];
    /// <summary>
    /// If True, quota management is not enabled for this volume.
    /// </summary>
    public bool? QuotasDisabled => (bool?)ManagementObject[nameof(QuotasDisabled)];
    /// <summary>
    /// If True, quota management was used but has been disabled. Incomplete refers to the information left in the file system after quota management has been disabled.
    /// </summary>
    public bool? QuotasIncomplete => (bool?)ManagementObject[nameof(QuotasIncomplete)];
    /// <summary>
    /// If True, the file system is setting up for quota management.
    /// </summary>
    public bool? QuotasRebuilding => (bool?)ManagementObject[nameof(QuotasRebuilding)];
    /// <summary>
    /// ID of the user's session. The user may be connected using a local login or a terminal session.
    /// </summary>
    public string? SessionID => (string)ManagementObject[nameof(SessionID)];
    /// <summary>
    /// If True, then the file system on which this network drive is mapped supports disk quotas.
    /// </summary>
    public bool? SupportsDiskQuotas => (bool?)ManagementObject[nameof(SupportsDiskQuotas)];
    /// <summary>
    /// If True, the logical disk partition supports file-based compression, such as is the case with NTFS. This property is False, when the Compressed property is True.
    /// </summary>
    public bool? SupportsFileBasedCompression => (bool?)ManagementObject[nameof(SupportsFileBasedCompression)];
    /// <summary>
    /// Volume name of the logical disk. This property value can have a maximum of 32 characters.
    /// </summary>
    public string? VolumeName => (string)ManagementObject[nameof(VolumeName)];
}
