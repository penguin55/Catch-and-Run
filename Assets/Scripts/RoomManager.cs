using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [SerializeField] private Text roomName;
    [SerializeField] private PlayerListings playerListing;
    [SerializeField] private RoomListings roomListing;

    private void Start()
    {
        instance = this;
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
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
    }

    private void OpenRoom()
    {
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_CREATE_ROOM_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_ROOMLIST_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_MENU_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_ROOM_PANEL);
    }

    public override void OnJoinedRoom()
    {
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
