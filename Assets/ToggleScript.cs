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
    public int numToggle;
    public bool unlockable;
    public int numAchievement;

    public Transform spray;
    public float rotationDuration;
    public float angle;
    public Ease ease;

    public GameObject unlockTextPrefab;
    public string unlockText;

    private GameObject myPanel;

    [SerializeField]
    private bool unlocked = false;

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }
    }

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener((value) =>
        {
            MyListener(value);
        });//Do this in Start() for example

        if(!unlockable)
        {
            unlocked = true;
        }
        else
        {
            GetComponent<Toggle>().interactable = false;
            CheckUnlockable();
        }
    }

    public void MyListener(bool value)
    {
     if(value)
        {
            VariableGlobale.Si().SetColor(c, intensity, numPlayer);

            if(numPlayer == 0)
            {
                SelectPaint.Si().DeselectP2(numToggle);
            }
            else
            {
                SelectPaint.Si().DeselectP1(numToggle);
            }
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

    private void CheckUnlockable()
    {
        if(Achievement.Si().getUnlock(numAchievement))
        {
            unlocked = true;
            GetComponent<Toggle>().interactable = true;
        }
    }

    private void OnEnable()
    {
        if(!unlocked && unlockable)
        {
            CheckUnlockable();
        }
    }

    public void ShowText()
    {
        if(!m_Toggle.interactable && !unlocked)
        {
            myPanel = Instantiate(unlockTextPrefab, transform.position + new Vector3(10, 40, 0), Quaternion.identity);
            myPanel.GetComponentInChildren<Text>().text = unlockText;
            myPanel.transform.SetParent(transform);
        }
    }

    public void UnshowText()
    {
        if(myPanel)
        {
            Destroy(myPanel);
        }
    }
}
