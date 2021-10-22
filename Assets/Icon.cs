using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Icon : MonoBehaviour
{
    public float duration;
    public float scale;

    public float angle;
    public float rotationduration;

    public Transform shadow;

    public Ease ease;


    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(scale * new Vector3(1, 1, 1), duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        shadow.DOScale(scale * new Vector3(-1, 1, 1), duration).SetLoops(-1, LoopType.Yoyo).SetEase(ease);

        transform.DORotate(Vector3.forward * angle, rotationduration).From(transform.eulerAngles).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        shadow.DORotate(Vector3.forward * angle, rotationduration).From(transform.eulerAngles).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
