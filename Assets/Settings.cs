using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Settings : MonoBehaviour
{

    public Ease rotationEase;
    public float rotationDuration;
    public float angle;

    public Transform gear1;
    public Transform gear2;

    public void RotateIn()
    {
        gear1.DOKill();
        gear2.DOKill();
        float duration = rotationDuration * (angle - gear1.eulerAngles.z) / angle;

        gear1.DORotate(Vector3.forward * angle, duration).SetEase(rotationEase);
        gear2.DORotate(Vector3.forward * -angle, duration).SetEase(rotationEase);
    }

    public void RotateOut()
    {
        gear1.DOKill();
        gear2.DOKill();

        float duration = (gear1.eulerAngles.z / angle) * rotationDuration;
        gear1.DORotate(Vector3.zero, duration).SetEase(rotationEase);
        gear2.DORotate(Vector3.zero, duration).SetEase(rotationEase);
    }
}
