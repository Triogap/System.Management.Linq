﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class LoadOrderGroupServiceMembers(ManagementObject ManagementObject) : CIM.Component(ManagementObject)
{
    /// <summary>
    /// Reference to the instance representing the load order group properties associated with the base service.
    /// </summary>
    public new LoadOrderGroup? GroupComponent => (LoadOrderGroup)ManagementObject[nameof(GroupComponent)];
    /// <summary>
    /// Reference to the instance representing the service that is a member of a load order group.
    /// </summary>
    public new BaseService? PartComponent => (BaseService)ManagementObject[nameof(PartComponent)];
}
