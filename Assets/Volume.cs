using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    Slider m_Slider;
    public bool SFX;
    void Awake()
    {
        //Fetch the Toggle GameObject
        m_Slider = GetComponent<Slider>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Slider.onValueChanged.AddListener((value) =>
        {
            MyListener(value);
        });//Do this in Start() for example
    }

    public void MyListener(float value)
    {
        if (SFX)
        {
            AudioManager.Si().VolumeSFX = value;
            AudioManager.Si().SetVolumeSFX();
        }
        else
        {
            AudioManager.Si().VolumeMusic = value;
            AudioManager.Si().SetVolumeMusic();
        }
    }

    void OnEnable()
    {
        if(SFX)
        {
            m_Slider.value = AudioManager.Si().VolumeSFX;
        }
        else
        {
            m_Slider.value = AudioManager.Si().VolumeMusic;
        }
    }

}
