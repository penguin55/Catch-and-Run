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

        SetUpRoomMaster();
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnClick_Ready()
    {
        if (!PhotonNetwork.IsMasterClient) playerListing.UpdateStatus(PhotonNetwork.LocalPlayer, playerListing.GetPlayerInfo(PhotonNetwork.LocalPlayer).Ready);
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 4)
            {
                Debug.Log("Can't play, at least 4 players in room");
                return;
            }

            if (playerListing.GetReadyAll())
            {
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("GAME");
            }
            else
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

    private void SetUpRoomMaster()
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


    public override void OnLeftRoom()
    {
        playerListing.GetContent().DestroyChildrens();
        LeftRoom();
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
