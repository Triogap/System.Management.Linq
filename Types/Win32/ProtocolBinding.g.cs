﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class ProtocolBinding(ManagementObject ManagementObject) : Base._Object(ManagementObject)
{
    /// <summary>
    /// Reference to the instance representing the protocol that is used with the system driver and on the network adapter.
    /// </summary>
    public NetworkProtocol? Antecedent => (NetworkProtocol)ManagementObject[nameof(Antecedent)];
    /// <summary>
    /// Reference to the instance representing the system driver that uses the network adapter through the network protocol of this class.
    /// </summary>
    public SystemDriver? Dependent => (SystemDriver)ManagementObject[nameof(Dependent)];
    /// <summary>
    /// Properties of the network adapter being used on the computer system.
    /// </summary>
    public NetworkAdapter? Device => (NetworkAdapter)ManagementObject[nameof(Device)];
}
