﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class CacheMemory(ManagementObject ManagementObject) : CIM.CacheMemory(ManagementObject)
{
    /// <summary>
    /// Speed of the cache.
    /// </summary>
    public uint? CacheSpeed => (uint?)ManagementObject[nameof(CacheSpeed)];
    /// <summary>
    /// Array of types of Static Random Access Memory (SRAM) being used for the cache memory.
    /// </summary>
    public ushort[]? CurrentSRAM => (ushort[])ManagementObject[nameof(CurrentSRAM)];
    /// <summary>
    /// Error correction method used by the cache memory.
    /// </summary>
    public ushort? ErrorCorrectType => (ushort?)ManagementObject[nameof(ErrorCorrectType)];
    /// <summary>
    /// Current size of the installed cache memory.
    /// </summary>
    public uint? InstalledSize => (uint?)ManagementObject[nameof(InstalledSize)];
    /// <summary>
    /// Physical location of the cache memory.
    /// </summary>
    public ushort? Location => (ushort?)ManagementObject[nameof(Location)];
    /// <summary>
    /// Maximum cache size installable to this particular cache memory.
    /// </summary>
    public uint? MaxCacheSize => (uint?)ManagementObject[nameof(MaxCacheSize)];
    /// <summary>
    /// Array of supported types of Static Random Access Memory (SRAM) that can be used for the cache memory.
    /// </summary>
    public ushort[]? SupportedSRAM => (ushort[])ManagementObject[nameof(SupportedSRAM)];
}
