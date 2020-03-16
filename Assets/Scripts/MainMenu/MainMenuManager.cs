using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField] private Text connectionLog;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject roomListPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject createRoomPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject readyButton;
    [SerializeField] private Text logMessage;
    [SerializeField] private GameObject connectingPanel;

    private Coroutine lastLogCoroutine;

    private void Start()
    {
        instance = this;
        AudioManager.instance.SetVolumeBGM(0.5f);
    }

    // Get the command from other class who call it, and doing a task according to command about changing UI Text
    public void SetCommandUIText(string command, string message)
    {
        switch (command.ToLower())
        {
            case "log_room":
                LogRoom(message);
                break;
        }
    }

    // Get the command from other class who call it, and doing a task according to command about hide or unhide the panel
    public void SetCommand(string command)
    {
        switch (command.ToLower())
        {
            case "open_connecting_panel":
                ShowConnectingPanel();
                break;
            case "hide_connecting_panel":
                HideConnectingPanel();
                break;
            case "open_menu_panel":
                ShowMenuPanel();
                break;
            case "hide_menu_panel":
                HideMenuPanel();
                break;
            case "open_tutorial_panel":
                //ShowTutorialPanel();
                break;
            case "hide_tutorial_panel":
                //HideTutorialPanel();
                break;
            case "open_room_panel" :
                ShowRoomPanel();
                break;
            case "hide_room_panel":
                HideRoomPanel();
                break;
            case "open_roomlist_panel":
                ShowRoomListPanel();
                break;
            case "hide_roomlist_panel":
                HideRoomListPanel();
                break;
            case "open_createroom_panel":
                ShowCreateRoomPanel();
                break;
            case "hide_createroom_panel":
                HideCreateRoomPanel();
                break;
            case "open_start_button":
                ShowStartButton();
                break;
            case "hide_start_button":
                HideStartButton();
                break;
            case "open_ready_button":
                ShowReadyButton();
                break;
            case "hide_ready_button":
                HideReadyButton();
                break;
        }
    }

    // To render about room panel
    #region RoomRender
    private void ShowRoomPanel()
    {
        roomPanel.SetActive(true);
        RenderRoom();
    }

    private void HideRoomPanel()
    {
        roomPanel.SetActive(false);
    }

    private void RenderRoom()
    {
        roomPanel.GetComponent<WaitingRoomUI>().Initialize();
    }

    private void ShowStartButton()
    {
        startButton.SetActive(true);
    }

    private void HideStartButton()
    {
        startButton.SetActive(false);
    }

    private void ShowReadyButton()
    {
        readyButton.SetActive(true);
    }

    private void HideReadyButton()
    {
        readyButton.SetActive(false);
    }
    #endregion

    // To render about room list panel
    #region RoomListRender
    private void ShowRoomListPanel()
    {
        roomListPanel.SetActive(true);
    }

    private void HideRoomListPanel()
    {
        roomListPanel.SetActive(false);
    }
    #endregion

    // To render about other panel in canvas
    #region UI

    private void ShowConnectingPanel()
    {
        connectingPanel.SetActive(true);
    }

    private void HideConnectingPanel()
    {
        connectingPanel.SetActive(false);
    }

    private void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    private void HideMenuPanel()
    {
        menuPanel.SetActive(false);
    }

    private void ShowTutorialPanel()
    {
        tutorialPanel.SetActive(true);
    }

    private void HideTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }

    private void ShowCreateRoomPanel()
    {
        createRoomPanel.SetActive(true);
    }

    private void HideCreateRoomPanel()
    {
        createRoomPanel.SetActive(false);
    }
    #endregion

    public void ExitGames()
    {
        Application.Quit();
    }

    // Trigerring the sfx each button is clicked
    public void OnClick()
    {
        AudioManager.instance.PlaySFX("button");
    }

    // To displaying the log message when the room is under 4 player and room master is hit the start
    // or when the a player is not ready but the room master hit the start button
    private void LogRoom(string message)
    {
        logMessage.text = message;
        if (lastLogCoroutine != null) StopCoroutine(lastLogCoroutine);
        lastLogCoroutine = StartCoroutine(delayAppear());
    }

    // To displaying the current status of connectivity of Photon
    public void ConnectionLog(string message, bool flag)
    {
        connectionLog.text = message;
        connectionLog.color = flag ? new Color(0, 84f / 255f, 0, 1) : new Color(1, 0, 0, 1);
    }

    // Give the log message time to dissapear
    IEnumerator delayAppear()
    {
        logMessage.enabled = true;
        yield return new WaitForSeconds(2);
        logMessage.enabled = false;
    }
}
