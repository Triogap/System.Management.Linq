﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class MediaAccessDevice(ManagementObject ManagementObject) : LogicalDevice(ManagementObject)
{
    /// <summary>
    /// Capabilities of the media access device.
    /// </summary>
    public ushort[]? Capabilities => (ushort[])ManagementObject[nameof(Capabilities)];
    /// <summary>
    /// Array of free-form strings that provides detailed explanations for access device features indicated in the Capabilities array.
    /// </summary>
    public string[]? CapabilityDescriptions => (string[])ManagementObject[nameof(CapabilityDescriptions)];
    public string? CompressionMethod => (string)ManagementObject[nameof(CompressionMethod)];
    /// <summary>
    /// Default block size, in bytes, for the device.
    /// </summary>
    public ulong? DefaultBlockSize => (ulong?)ManagementObject[nameof(DefaultBlockSize)];
    /// <summary>
    /// Free-form string that describes the types of error detection and correction supported by the device.
    /// </summary>
    public string? ErrorMethodology => (string)ManagementObject[nameof(ErrorMethodology)];
    /// <summary>
    /// Maximum block size, in bytes, for media accessed by the device.
    /// </summary>
    public ulong? MaxBlockSize => (ulong?)ManagementObject[nameof(MaxBlockSize)];
    /// <summary>
    /// Maximum size, in kilobytes, of media supported by this device. Kilobytes are interpreted as the number of bytes multiplied by 1000 (not the number of bytes multiplied by 1024).
    /// </summary>
    public ulong? MaxMediaSize => (ulong?)ManagementObject[nameof(MaxMediaSize)];
    /// <summary>
    /// Minimum block size, in bytes, for media accessed by the device.
    /// </summary>
    public ulong? MinBlockSize => (ulong?)ManagementObject[nameof(MinBlockSize)];
    /// <summary>
    /// If TRUE, the media access device needs cleaning. Whether manual or automatic cleaning is possible is indicated in the Capabilities array property.
    /// </summary>
    public bool? NeedsCleaning => (bool?)ManagementObject[nameof(NeedsCleaning)];
    /// <summary>
    /// Maximum number of multiple individual media that can be supported or inserted.
    /// </summary>
    public uint? NumberOfMediaSupported => (uint?)ManagementObject[nameof(NumberOfMediaSupported)];
}
