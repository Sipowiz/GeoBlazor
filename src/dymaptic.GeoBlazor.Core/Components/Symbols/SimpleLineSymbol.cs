﻿using dymaptic.GeoBlazor.Core.Extensions;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace dymaptic.GeoBlazor.Core.Components.Symbols;

/// <summary>
///     SimpleLineSymbol is used for rendering 2D polyline geometries in a 2D MapView. SimpleLineSymbol is also used for rendering outlines for marker symbols and fill symbols.
///     <a target="_blank" href="https://developers.arcgis.com/javascript/latest/api-reference/esri-symbols-SimpleLineSymbol.html">ArcGIS JS API</a>
/// </summary>
public class SimpleLineSymbol : LineSymbol
{
    /// <inheritdoc />
    public override string Type => "simple-line";
    
    /// <summary>
    ///     Specifies the line style.
    /// </summary>
    [JsonPropertyName("style")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Parameter]
    public LineStyle? LineStyle { get; set; }
}

/// <summary>
///     Possible line style values for <see cref="SimpleLineSymbol"/>
/// </summary>
[JsonConverter(typeof(LineStyleConverter))]
public enum LineStyle
{
#pragma warning disable CS1591
    Solid,
    ShortDot,
    Dash
#pragma warning restore CS1591
}

internal class LineStyleConverter : JsonConverter<LineStyle>
{
    public override LineStyle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, LineStyle value, JsonSerializerOptions options)
    {
        string? stringVal = Enum.GetName(typeof(LineStyle), value);
        string resultString = stringVal!.ToKebabCase();
        writer.WriteRawValue($"\"{resultString}\"");
    }
}