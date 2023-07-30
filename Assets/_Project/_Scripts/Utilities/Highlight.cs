using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers;
    [SerializeField] private Material _highlightMaterial;
    private List<Material> _originalMaterials = new List<Material>();

    private void Start()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _originalMaterials.Add(_renderers[i].sharedMaterial);
        }
    }

    public void ToggleHighlight(bool value)
    {
        if (value)
        {
            for (int i = 0; i < _renderers.Count; i++)
            {
                _renderers[i].material = _highlightMaterial;
            }
        }
        else
        {
            for (int i = 0; i < _renderers.Count; i++)
            {
                _renderers[i].material = _originalMaterials[i];
            }
        }
    }
}