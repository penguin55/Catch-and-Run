using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The same function with MainMenuManager class
public class UIManager : MonoBehaviour
{
    
    public static UIManager instance;

    [SerializeField] private GameObject buttonExit;
    [SerializeField] private Text time;
    [SerializeField] private Text startTimer;
    [SerializeField] private GameObject exitConfPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text gameOverMessage;

    private void Start()
    {
        instance = this;
    }

    public void SendCommandUIText(string command, string value)
    {
        switch (command.ToLower())
        {
            case "update_time_ui":
                UpdateTime(value);
                break;
            case "update_start_timer":
                UpdateStartTimer(value);
                break;
            case "set_last_catcher":
                SetLastCatcher(value);
                break;
        }
    }

    public void SendCommand(string command)
    {
        switch (command.ToLower())
        {
            case "hide_time_text":
                ActivateTimeText(false);
                break;
            case "open_time_text":
                ActivateTimeText(true);
                break;
            case "hide_start_timer_text":
                ActivateStartTimerText(false);
                break;
            case "open_start_timer_text":
                ActivateStartTimerText(true);
                break;
            case "hide_exit_button":
                ActivateExitButton(false);
                break;
            case "open_exit_button":
                ActivateExitButton(true);
                break;
            case "hide_exit_confirmation_panel":
                ActivateExitConfirmationPanel(false);
                break;
            case "open_exit_confirmation_panel":
                ActivateExitConfirmationPanel(true);
                break;
            case "hide_gameover_panel":
                ActivateGameoverPanel(false);
                break;
            case "open_gameover_panel":
                ActivateGameoverPanel(true);
                break;
        }
    }

    public void ExitGameMaster()
    {
        GameManagement.instance.LeaveRoom();
    }

    public void GoToHome()
    {
        GameManagement.instance.LeaveRoom();
    }

    private void ActivateTimeText(bool flag)
    {
        time.gameObject.SetActive(flag);
    }

    private void ActivateStartTimerText(bool flag)
    {
        startTimer.gameObject.SetActive(flag);
    }

    private void ActivateExitButton(bool flag)
    {
        buttonExit.SetActive(flag);
    }

    private void ActivateExitConfirmationPanel(bool flag)
    {
        exitConfPanel.SetActive(flag);
    }

    private void ActivateGameoverPanel(bool flag)
    {
        gameOverPanel.SetActive(flag);
    }

    private void UpdateTime(string time)
    {
        this.time.text = time + " s";
    }

    private void UpdateStartTimer(string time)
    {
        this.startTimer.text = time;
    }

    private void SetLastCatcher(string message)
    {
        gameOverMessage.text = message + " is the last catcher.";
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySFX("button");
    }
}
