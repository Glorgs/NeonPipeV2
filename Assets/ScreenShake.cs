using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MySingleton<ScreenShake>
{
    public float timeShake;
    public float shakeForce;

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shake()
    {
        cam.DOShakePosition(timeShake, shakeForce);
    }
}
