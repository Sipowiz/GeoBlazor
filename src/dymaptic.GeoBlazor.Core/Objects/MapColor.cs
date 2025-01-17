﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace dymaptic.GeoBlazor.Core.Objects;

/// <summary>
///     Creates a new color object by passing either a hex, rgb(a), hsl(a) or named color value. Hex, hsl(a) and named color values can be passed as a string:
///     <a target="_blank" href="https://developers.arcgis.com/javascript/latest/api-reference/esri-Color.html">ArcGIS JS API</a>
/// </summary>
[JsonConverter(typeof(MapColorConverter))]
public class MapColor
{
    /// <summary>
    ///     Creates a new color with a collection of numeric values in rgb or rgba format.
    /// </summary>
    /// <param name="values">
    ///     Requires 3 or 4 values, in the order R(0-255), G(0-255), B(0-255), A(0-1). A is optional.
    /// </param>
    public MapColor(params double[] values)
    {
        Values.AddRange(values);
    }

    /// <summary>
    ///     Creates a new color with a common color name, or a hex value starting with the # sign.
    /// </summary>
    /// <param name="hexOrNameValue">
    ///     The value of the hex or name.
    /// </param>
    public MapColor(string hexOrNameValue)
    {
        HexOrNameValue = hexOrNameValue;
    }

    /// <summary>
    ///     The numeric values for calculating a color (rgb/rgba).
    /// </summary>
    public List<double> Values { get; set; } = new();
    
    /// <summary>
    ///     The name or hex value of the color.
    /// </summary>
    public string? HexOrNameValue { get; set; }
}

internal class MapColorConverter : JsonConverter<MapColor>
{
    public override MapColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, MapColor value, JsonSerializerOptions options)
    {
        if (value.Values.Any())
        {
            writer.WriteRawValue(JsonSerializer.Serialize(value.Values, options));
        }
        else if (value.HexOrNameValue is not null)
        {
            writer.WriteRawValue($"\"{value.HexOrNameValue}\"");
        }
    }
}