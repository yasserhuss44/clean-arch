﻿//https://lioncoding.com/calculate-a-file-size-from-base64-string/

namespace Core.Utilities;

public sealed class FileSizeHelper
{
    /// <summary>
    /// Calculate a file size from base64 string.
    /// </summary>
    /// <param name="base64String">The base64 string.</param>
    /// <param name="applyPaddingsRules">Indicate if the padding management is required or not. Default is false</param>
    /// <param name="unitsOfMeasurement">The unit of measure of the file size returned by the method. The default unit of measure is Byte.</param>
    /// <returns>The size of the file represented by the base64 string.</returns>
    public static double GetFileSizeFromBase64String(
        string base64String ,
        bool applyPaddingsRules = false ,
        UnitsOfMeasurement unitsOfMeasurement = UnitsOfMeasurement.Byte)
    {
        if(string.IsNullOrEmpty(base64String))
            return 0;

        var base64Length = base64String.AsSpan()[(base64String.IndexOf(',') + 1)..].Length;

        var fileSizeInByte = Math.Ceiling((double)base64Length / 4) * 3;

        if(applyPaddingsRules && base64Length >= 2)
        {
            var paddings = base64String.AsSpan()[^2..];
            fileSizeInByte = paddings.EndsWith("==") ? fileSizeInByte - 2 :
                paddings[1].Equals('=') ? fileSizeInByte - 1 : fileSizeInByte;
        }

        return fileSizeInByte > 0 ? fileSizeInByte / unitsOfMeasurement.ToInt() : 0;
    }

    public static string GetBase64StringWithoutPadding(string base64String)
        => base64String.AsSpan()[(base64String.IndexOf(',') + 1)..].ToString();
}

/// <summary>
/// Unit of measurement.
/// </summary>
public enum UnitsOfMeasurement
{
    /// <summary>
    /// B.
    /// </summary>
    Byte = 1,
    /// <summary>
    /// KB.
    /// </summary>
    KiloByte = 1_024,
    /// <summary>
    /// MB.
    /// </summary>
    MegaByte = 1_048_576
}