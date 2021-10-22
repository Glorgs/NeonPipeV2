using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleScript : MonoBehaviour
{
    Toggle m_Toggle;
    public Color c;
    public float intensity;
    public int numPlayer;

    public Transform spray;
    public float rotationDuration;
    public float angle;
    public Ease ease;

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

    public void Move()
    {
        spray.DOKill();
        float duration = rotationDuration * (angle - spray.eulerAngles.z) / angle;

        spray.DORotate(Vector3.forward * 20, duration).From(Vector3.zero).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        spray.DOScale(1.2f, 0.3f);
    }

    public void Out()
    {
        spray.DOKill();
        spray.DOScale(1f, 0.3f);
        spray.DORotate(Vector3.zero, 0.3f);
    }
}
