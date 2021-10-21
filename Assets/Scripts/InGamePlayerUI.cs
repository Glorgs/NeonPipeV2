using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePlayerUI : MonoBehaviour
{
    [SerializeField] private Slider LifeBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private RawImage[] hearts;

    public int blinkFrame = 3;

    private int score = 0;
    private CanvasRenderer scoreRenderer;
    private float newBarValue;
    private bool isUpdatingBar = false;
    public float speedBar = 0.001f;

    private void Start() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        //scoreRenderer = scoreText.gameObject.GetComponent<CanvasRenderer>();
    }

    public void UpdateScoreText(int currentScore) {
        score = currentScore;
        scoreText.SetText(currentScore.ToString());
    }

    public int GetScore() {
        return score;
    }

    public void UpdateLife(int currentLife) {
        Debug.Log("UpdateLife");
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentLife) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    public void UpdateLifeBar(int amount)
    {
        if (!isUpdatingBar)
        {
            StartCoroutine(UpdateBar(amount));
        }
        else
        {
            VariableGlobale.Si().Heal(amount);
            newBarValue = ((float)VariableGlobale.Si().CurrentHP / VariableGlobale.Si().MaxHP);
        }

    }

    IEnumerator UpdateBar(int amount)
    {
        isUpdatingBar = true;
        float value = LifeBar.value;
        Debug.Log(amount);

        VariableGlobale.Si().Heal(amount);
        newBarValue = ((float)VariableGlobale.Si().CurrentHP / VariableGlobale.Si().MaxHP);

        while (LifeBar.value != newBarValue)
        {
            Debug.Log("Min : " + Mathf.Sign(newBarValue - value));
            LifeBar.value += Mathf.Min(speedBar, Mathf.Abs(newBarValue - LifeBar.value)) * Mathf.Sign(newBarValue - value);
            yield return null;
        }

        isUpdatingBar = false;

    }
}
