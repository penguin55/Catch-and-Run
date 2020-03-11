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
