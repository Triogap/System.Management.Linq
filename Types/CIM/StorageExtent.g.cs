﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class StorageExtent(ManagementObject ManagementObject) : LogicalDevice(ManagementObject)
{
    /// <summary>
    /// Describes the read/write properties of the media.
    /// </summary>
    public ushort? Access => (ushort?)ManagementObject[nameof(Access)];
    /// <summary>
    /// Size, in bytes, of the blocks that form the storage extent. If variable block size, then the maximum block size, in bytes, should be specified. If the block size is unknown, or if a block concept is not valid (for example, for aggregate extents, memory, or logical disks), enter a 1 (one).
    /// </summary>
    public ulong? BlockSize => (ulong?)ManagementObject[nameof(BlockSize)];
    /// <summary>
    /// Free-form string that describes the type of error detection and correction supported by the storage extent.
    /// </summary>
    public string? ErrorMethodology => (string)ManagementObject[nameof(ErrorMethodology)];
    /// <summary>
    /// Number of consecutive blocks, each block the size of the value contained in the BlockSize property, that form the storage extent. Total size of the storage extent can be calculated by multiplying the value of the BlockSize property by the value of this property. If the value of BlockSize is 1 (one), this property is the total size of the storage extent.
    /// </summary>
    public ulong? NumberOfBlocks => (ulong?)ManagementObject[nameof(NumberOfBlocks)];
    /// <summary>
    /// Free form string that describes the media and its use.
    /// </summary>
    public string? Purpose => (string)ManagementObject[nameof(Purpose)];
}
