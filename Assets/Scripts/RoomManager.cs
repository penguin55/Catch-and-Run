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
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_CREATE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOMLIST_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_MENU_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_ROOM_PANEL);
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_Ready()
    {
        customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = !((bool)customProps[CommandManager.PROPS.READY_PLAYER_STATUS]);

        PhotonNetwork.SetPlayerCustomProperties(customProps);
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
        LeftRoom();
    }

    public override void OnJoinedRoom()
    {
        customProps[CommandManager.PROPS.READY_PLAYER_STATUS] = false;
        OpenRoom();
        playerListing.GetCurrentRoomPlayers();
        PhotonNetwork.SetPlayerCustomProperties(customProps);
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
