using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private RoomListInfo roomListingPrefabs;
    [SerializeField] private Text roomName;
    [SerializeField] private Text roomStatus;
    [SerializeField] private Text roomPlayerCount;
    [SerializeField] private Button joinButton;

    private List<RoomListInfo> rooms = new List<RoomListInfo>();
    private List<RoomInfo> roomsInfo = new List<RoomInfo>(); 

    private RoomInfo currentRoom = null;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                if (currentRoom == info) UnsetCurrentRoom();
                int index = rooms.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(rooms[index].gameObject);
                    rooms.RemoveAt(index);
                    roomsInfo.RemoveAt(index);
                }
            }
            else
            {
                UpdatingList(info);
            }
        }
    }

    private void UpdatingList(RoomInfo info)
    {
        if (roomsInfo.Contains(info))
        {
            rooms[roomsInfo.IndexOf(info)].SetRoomInfo(info);
        }
        else
        {
            RoomListInfo room = Instantiate(roomListingPrefabs, content);
            if (room != null) room.SetRoomInfo(info);
            rooms.Add(room);
            roomsInfo.Add(info);
        }
    }

    public void UnsetCurrentRoom()
    {
        roomName.text = "";
        roomStatus.text = "";
        roomPlayerCount.text = "";
        joinButton.gameObject.SetActive(false);
        currentRoom = null;
    }

    public void SetCurrentRoom(RoomInfo info)
    {
        currentRoom = info;
        roomName.text = info.Name;
        roomStatus.text = info.MaxPlayers / info.PlayerCount == 1 ? "Room Full" : "Waiting...";
        roomPlayerCount.text = info.PlayerCount + "/" + info.MaxPlayers;
        joinButton.gameObject.SetActive(true);
    }

    public void OnClick_Join()
    {
        if (currentRoom != null) PhotonNetwork.JoinRoom(currentRoom.Name);
    }

    public Transform GetContent()
    {
        return content;
    }
}
