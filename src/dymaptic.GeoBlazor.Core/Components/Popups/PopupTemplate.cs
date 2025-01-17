﻿using Microsoft.AspNetCore.Components;

namespace dymaptic.GeoBlazor.Core.Components.Popups;

/// <summary>
///     A PopupTemplate formats and defines the content of a Popup for a specific Layer or Graphic. The user can also use the PopupTemplate to access values from feature attributes and values returned from Arcade expressions when a feature in the view is selected.
///     <a target="_blank" href="https://developers.arcgis.com/javascript/latest/api-reference/esri-PopupTemplate.html">ArcGIS JS API</a>
/// </summary>
public class PopupTemplate : MapComponent
{
    /// <summary>
    ///     The template for defining and formatting a popup's content, provided as a simple string.
    /// </summary>
    /// <remarks>
    ///     Either <see cref="Content"/> or <see cref="StringContent"/> should be defined, but not both.
    /// </remarks>
    [Parameter]
    [RequiredProperty(nameof(Content))]
    public string? StringContent { get; set; }

    /// <summary>
    ///     The template for defining how to format the title used in a popup.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    ///     The template for defining and formatting a popup's content, provided as a collection of <see cref="PopupContent"/>s.
    /// </summary>
    /// <remarks>
    ///     Either <see cref="Content"/> or <see cref="StringContent"/> should be defined, but not both.
    /// </remarks>
    [RequiredProperty(nameof(StringContent))]
    public HashSet<PopupContent> Content { get; set; } = new();

    /// <inheritdoc />
    public override async Task RegisterChildComponent(MapComponent child)
    {
        switch (child)
        {
            case PopupContent popupContent:
                if (!Content.Contains(popupContent))
                {
                    Content.Add(popupContent);
                    await UpdateComponent();
                }

                break;
            default:
                await base.RegisterChildComponent(child);

                break;
        }
    }

    /// <inheritdoc />
    public override async Task UnregisterChildComponent(MapComponent child)
    {
        switch (child)
        {
            case PopupContent popupContent:
                if (Content.Contains(popupContent))
                {
                    Content.Remove(popupContent);
                }

                break;
            default:
                await base.UnregisterChildComponent(child);

                break;
        }
    }

    /// <inheritdoc />
    public override void ValidateRequiredChildren()
    {
        base.ValidateRequiredChildren();

        foreach (PopupContent item in Content)
        {
            item.ValidateRequiredChildren();
        }
    }
}