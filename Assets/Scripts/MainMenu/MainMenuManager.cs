using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject roomListPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private GameObject createRoomPanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject readyButton;

    private void Start()
    {
        instance = this;
    }

    public void SetCommand(string command)
    {
        switch (command.ToLower())
        {
            case "open_menu_panel":
                //ShowMenuPanel();
                break;
            case "hide_menu_panel":
                //HideMenuPanel();
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

    #region UI
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

    private void OnClick()
    {

    }
}
