using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class RoomListInfo : MonoBehaviour
{
    [SerializeField] private Text nameRoom;
    [SerializeField] private Text status;
    [SerializeField] private Text playerInfo;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        nameRoom.text = roomInfo.Name;
        playerInfo.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
        status.text = roomInfo.MaxPlayers / roomInfo.PlayerCount == 1 ? "Room Full" : "Waiting...";
    }

    public void OnClick_Button()
    {
        RoomManager.instance.UpdateSelectedRoom(RoomInfo);
    }
}
