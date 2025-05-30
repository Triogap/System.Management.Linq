﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.Win32;

public partial record class PrinterConfiguration(ManagementObject ManagementObject) : CIM.Setting(ManagementObject)
{
    /// <summary>
    /// Number of bits used to represent the color in this configuration (the bits per pixel). This property is obsolete. Instead, use properties in the Win32_VideoController, Win32_DesktopMonitor, or CIM_VideoControllerResolution classes to determine how color is represented.
    /// </summary>
    public uint? BitsPerPel => (uint?)ManagementObject[nameof(BitsPerPel)];
    /// <summary>
    /// If TRUE, the pages that are printed should be collated. To collate is to print out the entire document before printing the next copy, as opposed to printing out each page of the document the required number of times.
    /// </summary>
    public bool? Collate => (bool?)ManagementObject[nameof(Collate)];
    /// <summary>
    /// Color of the document. Some color printers have the capability to print using true black instead of a combination of cyan, magenta, and yellow (CMY). This usually creates darker and sharper text for documents. This option is only useful for color printers that support true black printing.
    /// </summary>
    public uint? Color => (uint?)ManagementObject[nameof(Color)];
    /// <summary>
    /// Number of copies to be printed. The printer driver must support printing multi-page copies.
    /// </summary>
    public uint? Copies => (uint?)ManagementObject[nameof(Copies)];
    /// <summary>
    /// Friendly name of the printer. This name is unique to the type of printer and may be truncated because of the limitations of the string from which it is derived.
    /// </summary>
    public string? DeviceName => (string)ManagementObject[nameof(DeviceName)];
    /// <summary>
    /// Indicates whether the display device is color or monochrome and whether the type of scanning is noninterlaced or interlaced. This property is obsolete. Instead, use display properties such as the DisplayType property of the Win32_DesktopMonitor class.
    /// </summary>
    public uint? DisplayFlags => (uint?)ManagementObject[nameof(DisplayFlags)];
    /// <summary>
    /// Displays the vertical refresh rate. The refresh rate for a monitor is the number of times the screen is redrawn per second (frequency). This property is obsolete. Instead, use properties in the Win32_VideoController, Win32_DesktopMonitor, or CIM_VideoControllerResolution class.
    /// </summary>
    public uint? DisplayFrequency => (uint?)ManagementObject[nameof(DisplayFrequency)];
    /// <summary>
    /// Dither type of the printer. This property can assume predefined values of 1 to 5, or driver-defined values from 6 to 256. Line art dithering is a special dithering method that produces well defined borders between black, white, and gray scalings. It is not suitable for images that include continuous graduations in intensity and hue, such as scanned photographs.
    /// </summary>
    public uint? DitherType => (uint?)ManagementObject[nameof(DitherType)];
    /// <summary>
    /// Version number of the Windows-based printer driver. The version numbers are created and maintained by the driver manufacturer.
    /// </summary>
    public uint? DriverVersion => (uint?)ManagementObject[nameof(DriverVersion)];
    /// <summary>
    /// If TRUE, printing is done on both sides. If FALSE, printing is done on only one side of the media.
    /// </summary>
    public bool? Duplex => (bool?)ManagementObject[nameof(Duplex)];
    /// <summary>
    /// Not supported.
    /// </summary>
    public string? FormName => (string)ManagementObject[nameof(FormName)];
    /// <summary>
    /// Print resolution in dots per inch along the x-axis (width) of the print job (similar to the obsolete XResolution property). This value is only set when the PrintQuality property of this class is positive.
    /// </summary>
    public uint? HorizontalResolution => (uint?)ManagementObject[nameof(HorizontalResolution)];
    /// <summary>
    /// Specific value of one of the three possible color matching methods (called intents) that should be used by default. ICM applications establish intents by using the ICM functions. This property can assume predefined values of 1 to 3, or driver-defined values from 4 to 256. Non-ICM applications can use this value to determine how the printer handles color printing jobs.
    /// </summary>
    public uint? ICMIntent => (uint?)ManagementObject[nameof(ICMIntent)];
    /// <summary>
    /// How ICM is handled. For a non-ICM application, this property determines if ICM is enabled or disabled. For ICM applications, the system examines this property to determine which part of the computer system handles ICM support.
    /// </summary>
    public uint? ICMMethod => (uint?)ManagementObject[nameof(ICMMethod)];
    /// <summary>
    /// Number of pixels per logical inch. This obsolete property is valid only with devices that work with pixels, which excludes devices such as printers. There is no replacement value that applies to printers.
    /// </summary>
    public uint? LogPixels => (uint?)ManagementObject[nameof(LogPixels)];
    /// <summary>
    /// Type of media on which the printer prints. The property can be set to a predefined value or a driver-defined value greater than or equal to 256.
    /// </summary>
    public uint? MediaType => (uint?)ManagementObject[nameof(MediaType)];
    /// <summary>
    /// Name of the printer with which this configuration is associated. This value matches the Name property of the associated Win32_Printer instance.
    /// </summary>
    public string? Name => (string)ManagementObject[nameof(Name)];
    /// <summary>
    /// Printing orientation of the paper.
    /// </summary>
    public uint? Orientation => (uint?)ManagementObject[nameof(Orientation)];
    /// <summary>
    /// Length of the paper. To determine the size of the paper in inches, divide this value by 254.
    /// </summary>
    public uint? PaperLength => (uint?)ManagementObject[nameof(PaperLength)];
    /// <summary>
    /// Size of the paper. The possible sizes are found in the PaperSizesSupported property of the associated Win32_Printer class.
    /// </summary>
    public string? PaperSize => (string)ManagementObject[nameof(PaperSize)];
    /// <summary>
    /// Width of the paper. To determine the size of the paper in inches, divide this value by 254.
    /// </summary>
    public uint? PaperWidth => (uint?)ManagementObject[nameof(PaperWidth)];
    /// <summary>
    /// This property is not supported.
    /// </summary>
    public uint? PelsHeight => (uint?)ManagementObject[nameof(PelsHeight)];
    /// <summary>
    /// This property is not supported.
    /// </summary>
    public uint? PelsWidth => (uint?)ManagementObject[nameof(PelsWidth)];
    /// <summary>
    /// One of four quality levels of the print job. If a positive value is specified, the quality is measured in dots per inch.
    /// </summary>
    public uint? PrintQuality => (uint?)ManagementObject[nameof(PrintQuality)];
    /// <summary>
    /// Factor by which the printed output is to be scaled. For example, a scale of 75 reduces the print output to 3/4 its original height and width.
    /// </summary>
    public uint? Scale => (uint?)ManagementObject[nameof(Scale)];
    /// <summary>
    /// Version number of the initialization data for the device associated with the Windows-based printer.
    /// </summary>
    public uint? SpecificationVersion => (uint?)ManagementObject[nameof(SpecificationVersion)];
    /// <summary>
    /// Indicates how TrueType fonts should be printed.
    /// </summary>
    public uint? TTOption => (uint?)ManagementObject[nameof(TTOption)];
    /// <summary>
    /// Print resolution along the y-axis (height) of the print job (similar to the obsolete YResolution property). This value is only set when the PrintQuality property of this class is positive.
    /// </summary>
    public uint? VerticalResolution => (uint?)ManagementObject[nameof(VerticalResolution)];
    /// <summary>
    /// This property is obsolete. Use the HorizontalResolution property instead.
    /// </summary>
    public uint? XResolution => (uint?)ManagementObject[nameof(XResolution)];
    /// <summary>
    /// This property is obsolete. Use the VerticalResolution property instead.
    /// </summary>
    public uint? YResolution => (uint?)ManagementObject[nameof(YResolution)];
}
