﻿// See https://aka.ms/new-console-template for more information
using System.Management.Generator;
using System.Threading.Channels;

List<string> classes = 
    [ 
    // WMI System Classes
    "__AbsoluteTimerInstruction", "__ACE", "__AggregateEvent", "__ArbitratorConfiguration", "__CacheControl", 
    "__CIMOMIdentification", "__ClassCreationEvent", "__ClassDeletionEvent", "__ClassModificationEvent", 
    "__ClassOperationEvent", "__ClassProviderRegistration", "__ConsumerFailureEvent", "__Event", "__EventConsumer", 
    "__EventConsumerProviderCacheControl", "__EventConsumerProviderRegistration", "__EventDroppedEvent", "__EventFilter",
    "__EventGenerator", "__EventProviderCacheControl", "__EventProviderRegistration", "__EventQueueOverflowEvent",
    "__EventSinkCacheControl", "__ExtendedStatus", "__ExtrinsicEvent", "__FilterToConsumerBinding", "__IndicationRelated",
    "__InstanceCreationEvent", "__InstanceDeletionEvent", "__InstanceModificationEvent", "__InstanceOperationEvent",
    "__InstanceProviderRegistration", "__IntervalTimerInstruction", "__MethodInvocationEvent", "__MethodProviderRegistration",
    "__Namespace", "__NamespaceCreationEvent", "__NamespaceDeletionEvent", "__NamespaceModificationEvent",
    "__NamespaceOperationEvent", "__NotifyStatus", "__NTLMUser9X", "__ObjectProviderCacheControl", "__ObjectProviderRegistration",
    "__PARAMETERS", "__PropertyProviderCacheControl", "__PropertyProviderRegistration", "__Provider", 
    "__ProviderHostQuotaConfiguration", "__ProviderRegistration", "__SecurityDescriptor", "__SecurityRelatedClass", 
    "__SystemClass", "__SystemEvent", "__SystemSecurity", "__thisNAMESPACE", "__TimerEvent", "__TimerInstruction",
    "__TimerNextFiring", "__Trustee", "__Win32Provider",
    // Computer System Hardware Classes
    "Win32_1394Controller", "Win32_1394ControllerDevice", "Win32_Fan", "Win32_HeatPipe", "Win32_Refrigeration",
    "Win32_TemperatureProbe", "Win32_AssociatedProcessorMemory", "Win32_AutochkSetting", "Win32_BaseBoard",
    "Win32_Battery", "Win32_BIOS", "Win32_Bus", "Win32_CacheMemory", "Win32_CDROMDrive",
    "Win32_CIMLogicalDeviceCIMDataFile", "Win32_ComputerSystemProcessor", "Win32_CurrentProbe", "Win32_DesktopMonitor",
    "Win32_DeviceBus", "Win32_DeviceChangeEvent", "Win32_DeviceMemoryAddress", "Win32_DeviceSettings", "Win32_DiskDrive",
    "Win32_DiskPartition", "Win32_DiskDriveToDiskPartition", "Win32_DisplayControllerConfiguration", "Win32_DMAChannel",
    "Win32_DriverForDevice", "Win32_FloppyController", "Win32_FloppyDrive", "Win32_IDEController",
    "Win32_IDEControllerDevice", "Win32_InfraredDevice", "Win32_IRQResource", "Win32_Keyboard", "Win32_LogicalDisk",
    "Win32_LogicalDiskRootDirectory", "Win32_LogicalDiskToPartition", "Win32_LogicalProgramGroup",
    "Win32_LogicalProgramGroupDirectory", "Win32_LogicalProgramGroupItem", "Win32_LogicalProgramGroupItemDataFile",
    "Win32_MappedLogicalDisk", "Win32_MemoryArray", "Win32_MemoryArrayLocation", "Win32_MemoryDevice",
    "Win32_MemoryDeviceArray", "Win32_MemoryDeviceLocation", "Win32_MotherboardDevice", "Win32_NetworkAdapter",
    "Win32_NetworkAdapterConfiguration", "Win32_NetworkAdapterSetting", "Win32_NetworkClient", "Win32_NetworkConnection",
    "Win32_NetworkLoginProfile", "Win32_NetworkProtocol", "Win32_OnBoardDevice", "Win32_ParallelPort",
    "Win32_PCMCIAController", "Win32_PhysicalMemory", "Win32_PhysicalMemoryArray", "Win32_PhysicalMemoryLocation",
    "Win32_PnPAllocatedResource", "Win32_PnPDevice", "Win32_PnPDeviceProperty", "Win32_PnPDevicePropertyUint8",
    "Win32_PnPDevicePropertyUint16", "Win32_PnPDevicePropertyUint32", "Win32_PnPDevicePropertyUint64",
    "Win32_PnPDevicePropertySint8", "Win32_PnPDevicePropertySint16", "Win32_PnPDevicePropertySint32",
    "Win32_PnPDevicePropertySint64", "Win32_PnPDevicePropertyString", "Win32_PnPDevicePropertyBoolean",
    "Win32_PnPDevicePropertyReal32", "Win32_PnPDevicePropertyReal64", "Win32_PnPDevicePropertyDateTime",
    "Win32_PnPDevicePropertySecurityDescriptor", "Win32_PnPDevicePropertyBinary", "Win32_PnPDevicePropertyUint16Array",
    "Win32_PnPDevicePropertyUint32Array", "Win32_PnPDevicePropertyUint64Array", "Win32_PnPDevicePropertySint8Array",
    "Win32_PnPDevicePropertySint16Array", "Win32_PnPDevicePropertySint32Array", "Win32_PnPDevicePropertySint64Array",
    "Win32_PnPDevicePropertyStringArray", "Win32_PnPDevicePropertyBooleanArray", "Win32_PnPDevicePropertyReal32Array",
    "Win32_PnPDevicePropertyReal64Array", "Win32_PnPDevicePropertyDateTimeArray",
    "Win32_PnPDevicePropertySecurityDescriptorArray", "Win32_PnPEntity", "Win32_PointingDevice", "Win32_PortableBattery",
    "Win32_PortConnector", "Win32_PortResource", "Win32_POTSModem", "Win32_POTSModemToSerialPort", "Win32_Printer",
    "Win32_PrinterConfiguration", "Win32_PrinterController", "Win32_PrinterDriver", "Win32_PrinterDriverDll",
    "Win32_PrintJob", "Win32_PrinterSetting", "Win32_PrinterShare", "Win32_Processor", "Win32_SCSIController",
    "Win32_SCSIControllerDevice", "Win32_SerialPort", "Win32_SerialPortConfiguration", "Win32_SerialPortSetting",
    "Win32_SMBIOSMemory", "Win32_SoundDevice", "Win32_TapeDrive", "Win32_TCPIPPrinterPort", "Win32_USBController",
    "Win32_USBControllerDevice", "Win32_VideoController", "Win32_VideoSettings", "Win32_VoltageProbe", 
    // Operating System Classes
    "Win32_Account", "Win32_AllocatedResource", "Win32_BootConfiguration", "Win32_ClassicCOMApplicationClasses",
    "Win32_ClassicCOMClass", "Win32_ClassicCOMClassSetting", "Win32_ClassicCOMClassSettings", "Win32_ClusterShare",
    "Win32_BaseService", "Win32_ClientApplicationSetting", "Win32_CodecFile", "Win32_COMApplication",
    "Win32_COMApplicationClasses", "Win32_COMApplicationSettings", "Win32_COMClass", "Win32_ComClassAutoEmulator",
    "Win32_ComClassEmulator", "Win32_ComponentCategory", "Win32_COMSetting", "Win32_ComputerSystem",
    "Win32_DCOMApplication", "Win32_DCOMApplicationSetting", "Win32_DependentService", "Win32_Desktop",
    "Win32_ComputerSystemProduct", "Win32_Directory", "Win32_Environment", "Win32_Group", "Win32_GroupUser",
    "Win32_ImplementedCategory", "Win32_LoadOrderGroup", "Win32_LoadOrderGroupServiceDependencies",
    "Win32_LoadOrderGroupServiceMembers", "Win32_LogonSessionMappedDisk", "Win32_LoggedOnUser", "Win32_LogonSession",
    "Win32_OperatingSystem", "Win32_OperatingSystemAutochkSetting", "Win32_OperatingSystemQFE", "Win32_OptionalFeature",
    "Win32_OSRecoveryConfiguration", "Win32_PageFile", "Win32_PageFileElementSetting", "Win32_PageFileSetting",
    "Win32_PageFileUsage", "Win32_PrivilegesStatus", "Win32_Process", "Win32_ProcessStartup", "Win32_ProgramGroupContents",
    "Win32_ProgramGroupOrItem", "Win32_ProtocolBinding", "Win32_QuickFixEngineering", "Win32_Registry",
    "Win32_ScheduledJob", "Win32_Service", "Win32_Session", "Win32_SessionProcess", "Win32_SessionResource", "Win32_Share",
    "Win32_ShareToDirectory", "Win32_StartupCommand", "Win32_SubDirectory", "Win32_SubSession", "Win32_SystemAccount",
    "Win32_SystemBIOS", "Win32_SystemBootConfiguration", "Win32_SystemConfigurationChangeEvent", "Win32_SystemDesktop",
    "Win32_SystemDevices", "Win32_SystemDriver", "Win32_SystemDriverPNPEntity", "Win32_SystemEnclosure",
    "Win32_SystemLoadOrderGroups", "Win32_SystemMemoryResource", "Win32_SystemNetworkConnections",
    "Win32_SystemOperatingSystem", "Win32_SystemPartitions", "Win32_SystemProcesses", "Win32_SystemProgramGroups",
    "Win32_SystemResources", "Win32_SystemServices", "Win32_SystemSetting", "Win32_SystemSlot", "Win32_SystemSystemDriver",
    "Win32_SystemTimeZone", "Win32_SystemUsers", "Win32_Thread", "Win32_TimeZone", "Win32_UserAccount", "Win32_UserDesktop",
    "Win32_VideoConfiguration", "Win32_VolumeChangeEvent", "Win32_ShortcutFile",
    // Performance Counter Classes
    "Win32_Perf", "Win32_PerfFormattedData", "Win32_PerfRawData",
    // WMI Service Management Classes
    "Win32_MethodParameterClass", "Win32_WMIElementSetting", "Win32_WMISetting",
];

var channel = Channel.CreateUnbounded<ClassDefinition>();

var loader = new DefinitionLoader(channel.Writer);
var generator = new CodeGenerator(channel.Reader);
await Task.WhenAll(loader.Load(classes), generator.GenerateCode());
