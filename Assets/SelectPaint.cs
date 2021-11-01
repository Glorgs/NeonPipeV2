using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPaint : MySingleton<SelectPaint>
{
    public Toggle[] toggleP1;
    public Toggle[] toggleP2;

    public void DeselectP1(int pos)
    {
        for(int i = 0; i<toggleP1.Length;i++)
        {
            if (toggleP1[i].GetComponent<ToggleScript>().Unlocked)
            {
                toggleP1[i].interactable = true;
            }
        }

        toggleP1[pos].interactable = false;
    }

    public void DeselectP2(int pos)
    {
        for (int i = 0; i < toggleP2.Length; i++)
        {
            if (toggleP2[i].GetComponent<ToggleScript>().Unlocked)
            {
                toggleP2[i].interactable = true;
            }
        }

        toggleP2[pos].interactable = false;
    }
}
