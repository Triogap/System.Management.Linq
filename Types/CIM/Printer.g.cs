﻿/**************************************************************
 *                                                            *
 *   WARNING: This file is autogenerated by                   *
 *   System.Management.Generator.                             *
 *   Any changes made to this file will be overwritten.       *
 *                                                            *
 **************************************************************/
#nullable enable
namespace System.Management.Types.CIM;

public partial record class Printer(ManagementObject ManagementObject) : LogicalDevice(ManagementObject)
{
    /// <summary>
    /// Describes all of the job sheets that are available on the printer. This can also be used to describe the banner that a printer might provide at the beginning of each job, or can describe other user specified options.
    /// </summary>
    public string[]? AvailableJobSheets => (string[])ManagementObject[nameof(AvailableJobSheets)];
    /// <summary>
    /// Printer capabilities.
    /// </summary>
    public ushort[]? Capabilities => (ushort[])ManagementObject[nameof(Capabilities)];
    /// <summary>
    /// Free-form strings that provide detailed explanations for any of the printer features indicated in the Capabilities array.
    /// </summary>
    public string[]? CapabilityDescriptions => (string[])ManagementObject[nameof(CapabilityDescriptions)];
    /// <summary>
    /// Available character sets for the output of text related to managing the printer. Strings provided in this property should conform to the semantics and syntax specified by section 4.1.2 ("Charset parameter") in RFC 2046 (MIME Part 2), and contained in the IANA character-set registry. Examples include "utf-8", "us-ascii", and "iso-8859-1".
    /// </summary>
    public string[]? CharSetsSupported => (string[])ManagementObject[nameof(CharSetsSupported)];
    /// <summary>
    /// Finishings and other capabilities of the printer that are currently in use. Each entry in this property should also be listed in the Capabilities array.
    /// </summary>
    public ushort[]? CurrentCapabilities => (ushort[])ManagementObject[nameof(CurrentCapabilities)];
    /// <summary>
    /// Current character set used for the output of text relating to management of the printer. The character set described by this property should also be listed in the CharsetsSupported property. The string specified by this property should conform to the semantics and syntax specified by section 4.1.2 ("Charset parameter") in RFC 2046 (MIME Part 2), and contained in the IANA character-set registry. Examples include "utf-8", "us-ascii", and "iso-8859-1".
    /// </summary>
    public string? CurrentCharSet => (string)ManagementObject[nameof(CurrentCharSet)];
    /// <summary>
    /// Current printer language being used; the language should also be listed in the LanguagesSupported property.
    /// </summary>
    public ushort? CurrentLanguage => (ushort?)ManagementObject[nameof(CurrentLanguage)];
    /// <summary>
    /// Mime type currently in use by the printer when the CurrentLanguage property is set to indicate that a mime type is in use.
    /// </summary>
    public string? CurrentMimeType => (string)ManagementObject[nameof(CurrentMimeType)];
    /// <summary>
    /// Current language in use by the printer for management. The language listed here should also be listed in NaturalLanguagesSupported.
    /// </summary>
    public string? CurrentNaturalLanguage => (string)ManagementObject[nameof(CurrentNaturalLanguage)];
    /// <summary>
    /// Paper type currently in use by the printer. The string should be expressed in the form specified by ISO/IEC 10175 Document Printing Application (DPA), which is also summarized in Appendix C of RFC 1759 (Printer MIB).
    /// </summary>
    public string? CurrentPaperType => (string)ManagementObject[nameof(CurrentPaperType)];
    /// <summary>
    /// Default finishings and other capabilities of the printer. Each entry in this property should also be listed in the Capabilities array.
    /// </summary>
    public ushort[]? DefaultCapabilities => (ushort[])ManagementObject[nameof(DefaultCapabilities)];
    /// <summary>
    /// Number of copies that a single job will produce, unless otherwise specified.
    /// </summary>
    public uint? DefaultCopies => (uint?)ManagementObject[nameof(DefaultCopies)];
    /// <summary>
    /// Default printer language. The language should also be listed in the LanguagesSupported property.
    /// </summary>
    public ushort? DefaultLanguage => (ushort?)ManagementObject[nameof(DefaultLanguage)];
    /// <summary>
    /// Default mime type used by the printer when the DefaultLanguage property is set to indicate that a mime type is in use.
    /// </summary>
    public string? DefaultMimeType => (string)ManagementObject[nameof(DefaultMimeType)];
    /// <summary>
    /// Number of print-stream pages that the printer will render onto a single media sheet, unless a job specifies otherwise.
    /// </summary>
    public uint? DefaultNumberUp => (uint?)ManagementObject[nameof(DefaultNumberUp)];
    /// <summary>
    /// Paper type that the printer will use if PrintJob does not specify a particular type. The string should be expressed in the form specified by ISO/IEC 10175 Document Printing Application (DPA), which is also summarized in Appendix C of RFC 1759 (Printer MIB).
    /// </summary>
    public string? DefaultPaperType => (string)ManagementObject[nameof(DefaultPaperType)];
    /// <summary>
    /// Printer error information.
    /// </summary>
    public ushort? DetectedErrorState => (ushort?)ManagementObject[nameof(DetectedErrorState)];
    /// <summary>
    /// Array that provides supplemental information for the current error state, indicated in the DetectedErrorState property.
    /// </summary>
    public string[]? ErrorInformation => (string[])ManagementObject[nameof(ErrorInformation)];
    /// <summary>
    /// Horizontal resolution in pixels-per-inch.
    /// </summary>
    public uint? HorizontalResolution => (uint?)ManagementObject[nameof(HorizontalResolution)];
    /// <summary>
    /// Printer jobs processed since the last reset. These jobs can be processed from one or more print queues.
    /// </summary>
    public uint? JobCountSinceLastReset => (uint?)ManagementObject[nameof(JobCountSinceLastReset)];
    /// <summary>
    /// Print languages that are natively supported.
    /// </summary>
    public ushort[]? LanguagesSupported => (ushort[])ManagementObject[nameof(LanguagesSupported)];
    /// <summary>
    /// Marking technology used by the printer.
    /// </summary>
    public ushort? MarkingTechnology => (ushort?)ManagementObject[nameof(MarkingTechnology)];
    /// <summary>
    /// Maximum number of copies that can be produced by the printer from a single job.
    /// </summary>
    public uint? MaxCopies => (uint?)ManagementObject[nameof(MaxCopies)];
    /// <summary>
    /// Maximum number of print-stream pages that the printer can render onto a single media sheet.
    /// </summary>
    public uint? MaxNumberUp => (uint?)ManagementObject[nameof(MaxNumberUp)];
    /// <summary>
    /// Largest job (as a byte stream) that the printer will accept in units of kilobytes. A value of 0 (zero) indicates that no limit has been set.
    /// </summary>
    public uint? MaxSizeSupported => (uint?)ManagementObject[nameof(MaxSizeSupported)];
    /// <summary>
    /// Free-form strings that provide detailed explanations of mime types that are supported by the printer. If data is provided for this property, then the value 47 ("Mime"), should be included in the LanguagesSupported property.
    /// </summary>
    public string[]? MimeTypesSupported => (string[])ManagementObject[nameof(MimeTypesSupported)];
    /// <summary>
    /// Available languages for strings used by the printer for management information output. The strings should conform to RFC 1766. For example, "en" is used for English.
    /// </summary>
    public string[]? NaturalLanguagesSupported => (string[])ManagementObject[nameof(NaturalLanguagesSupported)];
    /// <summary>
    /// Types of paper supported.
    /// </summary>
    public ushort[]? PaperSizesSupported => (ushort[])ManagementObject[nameof(PaperSizesSupported)];
    /// <summary>
    /// Free-form strings that specify the types of paper that are currently available for the printer. Each string should be expressed in the form specified by ISO/IEC 10175 Document Printing Application (DPA), which is also summarized in Appendix C of RFC 1759 (Printer MIB). Examples of valid strings are "iso-a4-colored" and "na-10x14-envelope". By definition, a paper size that is available and listed in the PaperTypesAvailable property should also appear in the PaperSizesSupported property.
    /// </summary>
    public string[]? PaperTypesAvailable => (string[])ManagementObject[nameof(PaperTypesAvailable)];
    /// <summary>
    /// Status information, beyond that specified in the Availability property, for a printer.
    /// </summary>
    public ushort? PrinterStatus => (ushort?)ManagementObject[nameof(PrinterStatus)];
    /// <summary>
    /// Time of last printer reset.
    /// </summary>
    public DateTimeOffset? TimeOfLastReset => ManagementObject.GetDateTimePropertyValue(nameof(TimeOfLastReset));
    /// <summary>
    /// Vertical resolution in pixels-per-inch.
    /// </summary>
    public uint? VerticalResolution => (uint?)ManagementObject[nameof(VerticalResolution)];
}
