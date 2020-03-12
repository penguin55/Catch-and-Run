using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private PlayerListInfo playerListingPrefabs;

    private List<PlayerListInfo> players = new List<PlayerListInfo>();

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

    private void AddPlayerToRoom(Player newPlayer)
    {
        PlayerListInfo player = Instantiate(playerListingPrefabs, content);
        if (player != null) player.SetRoomInfo(newPlayer);
        players.Add(player);
    }

    public Transform GetContent()
    {
        return content;
    }

    public void UpdateStatus(Player player)
    {
        int index = players.FindIndex(x => x.PlayerInfo == player);
        if (index != -1)
        {
            players[index].GetReady((bool) player.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = players.FindIndex(x => x.PlayerInfo == otherPlayer);
        Destroy(players[index].gameObject);
        players.RemoveAt(index);
    }

}
