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
    [SerializeField] private GameObject Canvas;
    [SerializeField] private TextMeshProUGUI winner;

    [SerializeField] private InGamePlayerUI player1UI;
    [SerializeField] private InGamePlayerUI player2UI;

    [SerializeField] private GameObject EndUIPlayer1;
    [SerializeField] private GameObject EndUIPlayer2;

    private PlayerInputAction playerAction;
    private InputActionMap actionMap;
    private bool isPaused;
    private Vector3 duoBasePos;
    private Vector3 comboBasePos;

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
        VariableGlobale.Si().InGameTime = 0f;

        AudioManager.Si().StopMusic("Music");
        AudioManager.Si().PlayMusic("MusicPlay", AudioManager.Si().gameObject);
        SceneManager.LoadScene(1);
    }

    public void StartGameCoop()
    {
        VariableGlobale.Si().NumberPlayer = 0;
        VariableGlobale.Si().MaxAmountPlayer = 2;
        VariableGlobale.Si().CurrentHP = VariableGlobale.Si().MaxHP;
        VariableGlobale.Si().InGameTime = 0f;

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
        EndUIPlayer2.SetActive(false);
        EndUIPlayer1.SetActive(false);

        if (VariableGlobale.Si().MaxAmountPlayer == 1)
        {
            StartGameSolo();
        }
        else
        {
            StartGameCoop();
        }
    }

    public void GoToMenu() {
        EndUIPlayer2.SetActive(false);
        EndUIPlayer1.SetActive(false);
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

        if(VariableGlobale.Si().MaxAmountPlayer == 2)
        {
            EndUIPlayer2.SetActive(true);
            EndUIPlayer2.GetComponent<SetResult>().SetEnd();


        }
        else
        {
            EndUIPlayer1.SetActive(true);
            EndUIPlayer1.GetComponent<SetResult>().SetEnd();
        }

        winnerText.SetWinnerText(player1UI.GetScore(), player2UI.GetScore());

        if(Achievement.Si().getUnlock(1) == false)
        {
            if ((int)VariableGlobale.Si().InGameTime > 100)
            {
                Achievement.Si().setUnlock(1, true);
            }
        }

        if (Achievement.Si().getUnlock(0) == false)
        {
            if ((int)VariableGlobale.Si().GlobalScore > 2000)
            {
                Achievement.Si().setUnlock(0, true);
            }
        }



        winnerText.SetTime((int)VariableGlobale.Si().InGameTime);

        Achievement.Si().SaveData();
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }

    public void Combo()
    {
        duoBasePos = ComboText.transform.GetChild(0).position;
        comboBasePos = ComboText.transform.GetChild(1).position;

        StartCoroutine(StartCombo());
    }

    IEnumerator StartCombo()
    {

        Transform duo = ComboText.transform.GetChild(0);
        Transform combo = ComboText.transform.GetChild(1);

        duo.DOKill();
        combo.DOKill();

        duo.gameObject.SetActive(true);

        duo.DOScale(1.0f, 0.3f).From(3.0f);
        duo.DOMove(duo.position + new Vector3(0, -1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 20f), 0.3f);
        
        duo.GetComponent<TextMeshProUGUI>().DOColor(new Color(1f, 0f, 1f), 0.5f).From(new Color(0f,1f, 52f/255)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.GetComponent<TextMeshProUGUI>().DOColor(new Color(1f, 0f, 1f), 0.5f).From(new Color(0f,1f, 52f/255)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        yield return new WaitForSeconds(0.1f);

        combo.gameObject.SetActive(true);

        combo.DOScale(1.0f, 0.3f).From(3.0f);
        combo.DOMove(combo.position + new Vector3(0, -1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 20f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        duo.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        duo.DOMove(duo.position + new Vector3(0, 1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 50f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        combo.DOMove(combo.position + new Vector3(0, -1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 50f), 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        
    }

    IEnumerator EndCombo()
    {

        Transform duo = ComboText.transform.GetChild(0);
        Transform combo = ComboText.transform.GetChild(1);

        TagManager.Si().EndAnim = true;

        duo.DOKill();
        combo.DOKill();

        duo.DOScale(0f, 0.3f).From(1.0f);
        duo.DOMove(duo.position + new Vector3(0, 1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 20f), 0.3f);

        yield return new WaitForSeconds(0.1f);

        combo.DOScale(0f, 0.3f).From(1.0f);
        combo.DOMove(combo.position + new Vector3(0, 1, 0) * (Canvas.GetComponent<RectTransform>().rect.height / 20f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        TagManager.Si().EndAnim = false;

        duo.DOKill();
        combo.DOKill();

        duo.GetComponent<TextMeshProUGUI>().DOKill();
        combo.GetComponent<TextMeshProUGUI>().DOKill();

        duo.transform.position = duoBasePos;
        combo.transform.position = comboBasePos;

        duo.gameObject.SetActive(false);
        combo.gameObject.SetActive(false);

    }

    public void StopCombo()
    {
        Debug.Log("StopCombo");

        StartCoroutine(EndCombo());

    }

}
