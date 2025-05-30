﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class Thread(ManagementObject ManagementObject) : LogicalElement(ManagementObject)
{
    /// <summary>
    /// Name of the class or subclass used in the creation of an instance. When used with other key properties of the class, this property allow all instances of the class and its subclasses to be uniquely identified.
    /// </summary>
    public string? CreationClassName => (string)ManagementObject[nameof(CreationClassName)];
    /// <summary>
    /// Scoping computer system's creation class name.
    /// </summary>
    public string? CSCreationClassName => (string)ManagementObject[nameof(CSCreationClassName)];
    /// <summary>
    /// Scoping computer system's name.
    /// </summary>
    public string? CSName => (string)ManagementObject[nameof(CSName)];
    /// <summary>
    /// Indicates the current operating condition of the thread.
    /// </summary>
    public ushort? ExecutionState => (ushort?)ManagementObject[nameof(ExecutionState)];
    /// <summary>
    /// Identifier for the thread.
    /// </summary>
    public string? Handle => (string)ManagementObject[nameof(Handle)];
    /// <summary>
    /// Time, in kernel mode, in 100 nanosecond units. If this information is not available, a value of 0 (zero) should be used.
    /// </summary>
    public ulong? KernelModeTime => (ulong?)ManagementObject[nameof(KernelModeTime)];
    /// <summary>
    /// Scoping operating system's creation class name.
    /// </summary>
    public string? OSCreationClassName => (string)ManagementObject[nameof(OSCreationClassName)];
    /// <summary>
    /// Scoping operating system's name.
    /// </summary>
    public string? OSName => (string)ManagementObject[nameof(OSName)];
    /// <summary>
    /// Urgency for execution of a thread. A thread can have a different priority than its owning process. If this information is not available for a thread, a value of 0 (zero) should be used.
    /// </summary>
    public uint? Priority => (uint?)ManagementObject[nameof(Priority)];
    /// <summary>
    /// Scoping process's creation class name.
    /// </summary>
    public string? ProcessCreationClassName => (string)ManagementObject[nameof(ProcessCreationClassName)];
    /// <summary>
    /// Scoping process's handle.
    /// </summary>
    public string? ProcessHandle => (string)ManagementObject[nameof(ProcessHandle)];
    /// <summary>
    /// Time, in user mode, in 100 nanosecond units. If this information is not available, a value of 0 (zero) should be used.
    /// </summary>
    public ulong? UserModeTime => (ulong?)ManagementObject[nameof(UserModeTime)];
}
