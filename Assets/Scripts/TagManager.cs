using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class TagManager : MySingleton<TagManager>
{
    public GameObject[] tags;
    public GameObject[] peintures;

    public Material rainbowMaterial;

    private bool inCombo = false;
    private float timeNoCombo = 1f;

    void Start()
    {
        tags = new GameObject[2];
        peintures = new GameObject[2];

        rainbowMaterial.SetColor("_BaseColor", Random.ColorHSV());
        rainbowMaterial.SetColor("_EmissiveColor", VariableGlobale.Si().rainbowColor);
    }
    
    public bool CheckSameTag()
    {
        if (tags[0] != null && tags[0] == tags[1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tags[0] != null && tags[0] == tags[1])
        {
            peintures[0].GetComponent<DecalProjector>().material = rainbowMaterial;
            peintures[1].GetComponent<DecalProjector>().material = rainbowMaterial;
            Debug.Log("Combo");

            if (timeNoCombo > 0.4f)
            {
                inCombo = true;
                UIManager.Si().Combo();
            }

            timeNoCombo = 0f;
        }
        else if(inCombo)
        {
            timeNoCombo += Time.deltaTime;
        }

        if (timeNoCombo > 0.4f && inCombo)
        {
            inCombo = false;
            UIManager.Si().StopCombo();
        }

        tags[0] = null;
        tags[1] = null;

    }

    // Update is called once per frame
    void Update()
    {
        rainbowMaterial.SetColor("_EmissiveColor", VariableGlobale.Si().rainbowColor);
    }
}
