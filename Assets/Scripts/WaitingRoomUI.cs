using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text roomName;
    [SerializeField] private Text roomPlayer;
    [SerializeField] private Text readyText;

    public void Initialize()
    {
        RoomInfo info = PhotonNetwork.CurrentRoom;
        roomName.text = "Room : "+info.Name;
        roomPlayer.text = (info.PlayerCount + 1) + "/" + info.MaxPlayers;
    }

    public void UpdateReadyButtonPlayer(Player player) {
        readyText.text = ((bool)player.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]) ? "CANCEL": "READY";
    }
}
