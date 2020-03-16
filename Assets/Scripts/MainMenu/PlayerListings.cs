using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private PlayerListInfo playerListingPrefabs;
    [SerializeField] private WaitingRoomUI waitingRoomUI;

    private List<PlayerListInfo> players = new List<PlayerListInfo>();

    // Getting the player who joins the room and displaying it
    public void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;

        content.DestroyChildrens();
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerToRoom(playerInfo.Value);
        }
    }

    // To add the player to the room of they clicked
    private void AddPlayerToRoom(Player newPlayer)
    {
        PlayerListInfo player = Instantiate(playerListingPrefabs, content);
        if (player != null) player.SetRoomInfo(newPlayer);
        players.Add(player);
    }

    // To check the all player is ready or not
    public bool GetReadyAll()
    {
        foreach (PlayerListInfo info in players)
        {
            if (!info.PlayerInfo.IsMasterClient)
            {
                if (!((bool)info.PlayerInfo.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]))
                    return false;
            }
        }

        return true;
    }

    // To get the content of player listing to spawn player info to list display
    public Transform GetContent()
    {
        return content;
    }

    // To remove player list when we leave the room
    public void RemovePlayers()
    {
        players.Clear();
    }

    // To update other player ready state locally by getting to network from each player
    public void UpdateStatus(Player player)
    {
        int index = players.FindIndex(x => x.PlayerInfo == player);
        if (index != -1)
        {
            players[index].GetReady((bool) player.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]);
        }
    }

    // To automatically kick all client who is not room master when the first room master is leave from room
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.InRoom)
        {
            RoomManager.instance.OnClick_LeaveRoom();
        }
    }

    // To update player list in waiting room, when new player joined the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToRoom(newPlayer);
        waitingRoomUI.UpdatePlayerRoom();
    }

    // To update player list in waiting room, when new player left from the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        waitingRoomUI.UpdatePlayerRoom();
        int index = players.FindIndex(x => x.PlayerInfo == otherPlayer);
        if (index != -1)
        {
            Destroy(players[index].gameObject);
            players.RemoveAt(index);
        }
    }

}
