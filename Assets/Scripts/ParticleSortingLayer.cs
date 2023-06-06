using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleSortingLayer : MonoBehaviour
{
    private void Awake() {
        var particleRenderer = GetComponent<Renderer>();
        particleRenderer.sortingLayerName = "GUI";
    }
}
