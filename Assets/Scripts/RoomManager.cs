using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [SerializeField] private Text roomName;
    [SerializeField] private PlayerListings playerListing;
    [SerializeField] private RoomListings roomListing;
    [SerializeField] private WaitingRoomUI waitingRoomUI;

    private ExitGames.Client.Photon.Hashtable customProps = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        instance = this;
        customProps.Add(CommandManager.PROPS.READY_PLAYER_STATUS, false);
        PhotonNetwork.SetPlayerCustomProperties(customProps);
    }

    public void UpdateSelectedRoom(RoomInfo info)
    {
        roomListing.SetCurrentRoom(info);
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;
        options.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
    }

    private void OpenRoom()
    {
        roomListing.UnsetCurrentRoom();

        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_CREATE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOMLIST_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_MENU_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_ROOM_PANEL);

        SetUpMasterRoom();
    }

    private void SetUpMasterRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_READY_BUTTON);
            MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_START_BUTTON);
        } else
        {
            MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_START_BUTTON);
            MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_READY_BUTTON);
        }
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_Ready()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = !((bool)customProps[CommandManager.PROPS.READY_PLAYER_STATUS]);

            PhotonNetwork.SetPlayerCustomProperties(customProps);
        }
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 4)
            {
                Debug.Log("Can't start, at least 4 players in room");
                return;
            }

            if (playerListing.GetReadyAll())
            {
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("GAME");
            } else
            {
                Debug.Log("All players must ready");
            }
        }
    }

    private void LeftRoom()
    {
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_MENU_PANEL);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey(CommandManager.PROPS.READY_PLAYER_STATUS))
        {
            playerListing.UpdateStatus(targetPlayer);
            if (targetPlayer == PhotonNetwork.LocalPlayer) waitingRoomUI.UpdateReadyButtonPlayer(targetPlayer);
        }
    }

    public override void OnLeftRoom()
    {
        playerListing.GetContent().DestroyChildrens();
        playerListing.RemovePlayers();
        LeftRoom();
    }

    public override void OnJoinedRoom()
    {
        customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = false;
        PhotonNetwork.SetPlayerCustomProperties(customProps);
        OpenRoom();
        playerListing.GetCurrentRoomPlayers();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("RoomCreated");
        OpenRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail to create room : "+message);
    }
}
