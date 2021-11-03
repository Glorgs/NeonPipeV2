using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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


    public void ShowDamageNumber(string s, Vector3 worldPosition, Color first, Color second)
    {
        GameObject newDamageNumberObject = Instantiate<GameObject>(DamageNumberPrefab);
        newDamageNumberObject.GetComponent<TextMeshProUGUI>().text = s; 

        Vector3 screenPosition = cam.WorldToViewportPoint(worldPosition);

        RectTransform damageNumberTransform = newDamageNumberObject.GetComponent<RectTransform>();

        damageNumberTransform.SetParent(transform, true);
        damageNumberTransform.localScale = Vector3.one;
        damageNumberTransform.anchoredPosition = new Vector2(screenPosition.x * m_RectTransform.rect.width, screenPosition.y * m_RectTransform.rect.height);

        newDamageNumberObject.transform.DOScale(1.3f, 0.3f);
        newDamageNumberObject.transform.DOMove(newDamageNumberObject.transform.position + new Vector3(0, 1, 0)*(canvas.GetComponent<RectTransform>().rect.height/10f), 2f);

        newDamageNumberObject.GetComponent<TextMeshProUGUI>().DOColor(second, 0.2f).From(first).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(DestroyShowedText(newDamageNumberObject));
    }

    public void ShowDamageNumberColor(string s, Vector3 worldPosition, Color first)
    {
        GameObject newDamageNumberObject = Instantiate<GameObject>(DamageNumberPrefab);
        newDamageNumberObject.GetComponent<TextMeshProUGUI>().text = s;
        newDamageNumberObject.GetComponent<TextMeshProUGUI>().color = first;

        Vector3 screenPosition = cam.WorldToViewportPoint(worldPosition);

        RectTransform damageNumberTransform = newDamageNumberObject.GetComponent<RectTransform>();

        damageNumberTransform.SetParent(transform, true);
        damageNumberTransform.localScale = Vector3.one;
        damageNumberTransform.anchoredPosition = new Vector2(screenPosition.x * m_RectTransform.rect.width, screenPosition.y * m_RectTransform.rect.height);

        newDamageNumberObject.transform.DOScale(1.3f, 0.3f);
        newDamageNumberObject.transform.DOMove(newDamageNumberObject.transform.position + new Vector3(0, 1, 0) * (canvas.GetComponent<RectTransform>().rect.height / 10f), 2f);

        StartCoroutine(DestroyShowedText(newDamageNumberObject));
    }

    IEnumerator DestroyShowedText(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        obj.transform.DOScale(0, 0.2f);

        yield return new WaitForSeconds(0.2f);

        obj.transform.DOKill();
        obj.GetComponent<TextMeshProUGUI>().DOKill();

        Destroy(obj);
    }
}
