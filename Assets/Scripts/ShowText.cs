using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowText : MySingleton<ShowText>
{
    [SerializeField]
    private GameObject DamageNumberPrefab;
    RectTransform m_RectTransform;

    public Camera cam;
    public GameObject canvas;
    
    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }


    public void ShowDamageNumber(string s, Vector3 worldPosition)
    {
        GameObject newDamageNumberObject = Instantiate<GameObject>(DamageNumberPrefab);
        newDamageNumberObject.GetComponent<Text>().text = s; 

        Vector3 screenPosition = cam.WorldToViewportPoint(worldPosition);

        RectTransform damageNumberTransform = newDamageNumberObject.GetComponent<RectTransform>();

        damageNumberTransform.SetParent(transform, true);
        damageNumberTransform.localScale = Vector3.one;
        damageNumberTransform.anchoredPosition = new Vector2(screenPosition.x * m_RectTransform.rect.width, screenPosition.y * m_RectTransform.rect.height);

        newDamageNumberObject.transform.DOScale(1.3f, 0.3f);
        newDamageNumberObject.transform.DOMove(newDamageNumberObject.transform.position + new Vector3(0, 1, 0)*(canvas.GetComponent<RectTransform>().rect.height/10f), 2f);

        newDamageNumberObject.GetComponent<Text>().DOColor(new Color(248f/255,95f/255,105f/255), 0.2f).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(DestroyShowedText(newDamageNumberObject));
    }

    IEnumerator DestroyShowedText(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        obj.transform.DOScale(0, 0.2f);

        yield return new WaitForSeconds(0.2f);

        obj.transform.DOKill();
        obj.GetComponent<Text>().DOKill();

        Destroy(obj);
    }
}
