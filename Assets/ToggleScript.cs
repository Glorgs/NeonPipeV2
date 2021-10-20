using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    Toggle m_Toggle;
    public Color c;
    public float intensity;
    public int numPlayer;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener((value) =>
        {
            MyListener(value);
        });//Do this in Start() for example
    }

    public void MyListener(bool value)
    {
     if(value)
        {
            VariableGlobale.Si().SetColor(c, intensity, numPlayer); ;
        }
    }
}
