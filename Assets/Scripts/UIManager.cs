using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;

public class UIManager : MySingleton<UIManager>
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject ComboText;
    [SerializeField] private TextMeshProUGUI winner;

    [SerializeField] private InGamePlayerUI player1UI;
    [SerializeField] private InGamePlayerUI player2UI;

    private PlayerInputAction playerAction;
    private InputActionMap actionMap;
    private bool isPaused;

    private void Awake() {
        playerAction = new PlayerInputAction();
        actionMap = playerAction.asset.FindActionMap("Player1");

        actionMap["Pause"].performed += ctx => EscapeButton();
        isPaused = false;
    }

    public void StartGameSolo() {
        VariableGlobale.Si().NumberPlayer = 0;
        VariableGlobale.Si().MaxAmountPlayer = 1;
        VariableGlobale.Si().CurrentHP = VariableGlobale.Si().MaxHP;

        AudioManager.Si().StopMusic("Music");
        AudioManager.Si().PlayMusic("MusicPlay", AudioManager.Si().gameObject);
        SceneManager.LoadScene(1);
    }

    public void StartGameCoop()
    {
        VariableGlobale.Si().NumberPlayer = 0;
        VariableGlobale.Si().MaxAmountPlayer = 2;
        VariableGlobale.Si().CurrentHP = VariableGlobale.Si().MaxHP;

        AudioManager.Si().StopMusic("Music");
        AudioManager.Si().PlayMusic("MusicPlay", AudioManager.Si().gameObject);
        SceneManager.LoadScene(1);
    }

    public void Quit() {
        Application.Quit();
    }

    private void EscapeButton() {
        if (isPaused) ResumeGame();
        if (!isPaused) PauseGame();
    }

    public void PauseGame() {
        isPaused = true;
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        if(VariableGlobale.Si().MaxAmountPlayer == 1)
        {
            StartGameSolo();
        }
        else
        {
            StartGameCoop();
        }
    }

    public void GoToMenu() {
        AudioManager.Si().StopMusic("MusicPlay");
        AudioManager.Si().PlayMusic("Music", AudioManager.Si().gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ShowEndMenu() {
        InGameUI.SetActive(false);
        EndMenu.SetActive(true);
        Time.timeScale = 0f;
        
        WinnerText winnerText = EndMenu.GetComponentInChildren<WinnerText>();
        winnerText.SetWinnerText(player1UI.GetScore(), player2UI.GetScore());
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }

    public void Combo()
    {
        StartCoroutine(StartCombo());
    }

    IEnumerator StartCombo()
    {
        Transform duo = ComboText.transform.GetChild(0);
        Transform combo = ComboText.transform.GetChild(1);

        duo.gameObject.SetActive(true);

        duo.DOScale(1.0f, 0.3f).From(3.0f);
        duo.DOMove(duo.position + new Vector3(-1, -1, 0) * 100, 0.3f);
        
        duo.GetComponent<Text>().DOColor(new Color(1f, 0f, 1f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.GetComponent<Text>().DOColor(new Color(1f, 0f, 1f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(0.1f);

        combo.gameObject.SetActive(true);

        combo.DOScale(1.0f, 0.3f).From(3.0f);
        combo.DOMove(combo.position + new Vector3(-1, -1, 0) * 100, 0.3f);

        yield return new WaitForSeconds(0.3f);

        duo.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        duo.DOMove(duo.position + new Vector3(0, 1, 0) * 30, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.DOMove(combo.position + new Vector3(0, -1, 0) * 30, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        
    }

}
