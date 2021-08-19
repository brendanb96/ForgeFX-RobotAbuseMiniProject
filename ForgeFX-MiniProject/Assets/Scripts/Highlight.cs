using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class adds highlighting functionality to a grabbable object.
/// </summary>
public class Highlight
{
    public Highlight(Material material)
    {
        highlightMaterial = material;
    }

    private readonly Material highlightMaterial = default;
    private readonly Dictionary<Renderer, Material[]> highlights = new Dictionary<Renderer, Material[]>();

    /// <summary>
    /// Highlight any given IEnumberable collection of renderers.
    /// </summary>
    /// <param name="renderers">Renderers to iterate through.</param>
    public void HighlightRenderers(IEnumerable<Renderer> renderers)
    {
        ClearHighlights();
        foreach (var meshRenderer in renderers)
        {
            highlights[meshRenderer] = meshRenderer.materials;
            ReplaceRendererMaterials(meshRenderer, highlightMaterial);
        }
    }

    /// <summary>
    /// Replace a renderer's materials with a newly specified one.
    /// </summary>
    /// <param name="renderer">Renderer's whose materials are to be replaced.</param>
    /// <param name="material">Material to change to.</param>
    private static void ReplaceRendererMaterials(Renderer renderer, Material material)
    {
        var materials = renderer.materials;
        for (var i = 0; i < materials.Length; i++)
        {
            materials[i] = material;
        }

        renderer.materials = materials;
    }

    /// <summary>
    /// Remove highlights from currently stored renderers.
    /// </summary>
    public void ClearHighlights()
    {
        foreach (var highlight in highlights)
        {
            highlight.Key.materials = highlight.Value;
        }
        highlights.Clear();
    }
}
