using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Rainbow : MonoBehaviour
{
    private DecalProjector renderer;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<DecalProjector>();

        renderer.material.SetColor("_BaseColor", Random.ColorHSV());
        renderer.material.SetColor("_EmissiveColor", VariableGlobale.Si().rainbowColor);
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.SetColor("_EmissiveColor", VariableGlobale.Si().rainbowColor);
    }
}
