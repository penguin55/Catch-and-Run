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
        roomName.text = "Room : "+ PhotonNetwork.CurrentRoom.Name;
        roomPlayer.text = PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    // To update players count in the room
    public void UpdatePlayerRoom()
    {
        roomPlayer.text = PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    // To toggle button from Ready to cancel and otherwise
    public void UpdateReadyButtonPlayer(Player player) {
        readyText.text = ((bool)player.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]) ? "CANCEL": "READY";
    }
}
