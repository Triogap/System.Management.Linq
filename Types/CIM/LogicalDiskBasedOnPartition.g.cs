﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class LogicalDiskBasedOnPartition(ManagementObject ManagementObject) : BasedOn(ManagementObject)
{
    /// <summary>
    /// A CIM_LogicalDisk that describes the logical disk which is built on the partition.
    /// </summary>
    public new LogicalDisk? Dependent => (LogicalDisk)ManagementObject[nameof(Dependent)];
    /// <summary>
    /// A CIM_DiskPartition that describes the disk partition.
    /// </summary>
    public new DiskPartition? Antecedent => (DiskPartition)ManagementObject[nameof(Antecedent)];
}
