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

    // This function is to update the room info we selected from room list. So, the info can appear in UI on the above of room list UI in room list panel.
    public void UpdateSelectedRoom(RoomInfo info)
    {
        roomListing.SetCurrentRoom(info);
    }

    // To create a new room
    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 8;
        options.BroadcastPropsChangeToAll = true;
        string roomTextName = roomName.text;

        if (roomTextName == string.Empty)
        {
            roomTextName = "Room-" + Random.Range(0,10000);
        }

        PhotonNetwork.JoinOrCreateRoom(roomTextName, options, TypedLobby.Default);
    }

    // To open a room we joined and display room panel
    private void OpenRoom()
    {
        roomListing.UnsetCurrentRoom();

        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_CREATE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOMLIST_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_MENU_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_ROOM_PANEL);

        SetUpMasterRoom();
    }

    // Setting up for master client and client to differentiate each other by give it start button for room master and ready button for client
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

    // Caed when we click the left button, this function to leave from the current room
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // To change in ready state or cancel ready when in room
    public void OnClick_Ready()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = !((bool)customProps[CommandManager.PROPS.READY_PLAYER_STATUS]);

            PhotonNetwork.SetPlayerCustomProperties(customProps);
        }
    }

    // To start teh game when player is equals or more than 4 and the players in room is all ready
    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 4)
            {
                MainMenuManager.instance.SetCommandUIText(CommandManager.UI.LOG_ROOM, "Can't start, at least 4 players in room!");
                return;
            }

            if (playerListing.GetReadyAll())
            {
                AudioManager.instance.SetVolumeBGM(0.2f);

                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("GAME");
            } else
            {
                MainMenuManager.instance.SetCommandUIText(CommandManager.UI.LOG_ROOM, "All players must ready!");
            }
        }
    }

    //To close the room panel and back to main menu
    private void LeftRoom()
    {
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_MENU_PANEL);
    }

    // It called when the player hit ready button, so other player can update the ready state of player who hit the ready button
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey(CommandManager.PROPS.READY_PLAYER_STATUS))
        {
            playerListing.UpdateStatus(targetPlayer);
            if (targetPlayer == PhotonNetwork.LocalPlayer) waitingRoomUI.UpdateReadyButtonPlayer(targetPlayer);
        }
    }

    // To clear the room player listing, so it will clear again when we joining the room again without causing the duplicate
    public override void OnLeftRoom()
    {
        playerListing.GetContent().DestroyChildrens();
        playerListing.RemovePlayers();
        LeftRoom();
    }

    // The function is called when we join the room
    public override void OnJoinedRoom()
    {
        customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = false;
        PhotonNetwork.SetPlayerCustomProperties(customProps);
        OpenRoom();
        playerListing.GetCurrentRoomPlayers();
    }

    // To open the room panel when the player is join the room
    public override void OnCreatedRoom()
    {
        Debug.Log("RoomCreated");
        OpenRoom();
    }

    // To give the player information about, what happened to they when they cant join the room
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail to create room : "+message);
    }
}
