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

    public PlayerListInfo GetPlayerInfo(Player player)
    {
        int index = players.FindIndex(x => x.PlayerInfo == player);
        if (index != -1)
        {
            return players[index];
        }
        else return null;
    }

    public bool GetReadyAll()
    {
        foreach (PlayerListInfo info in players)
        {
            if (!info.Ready && !info.PlayerInfo.IsMasterClient) return false;
        }

        return true;
    }

    public void UpdateStatus(Player player, bool flag)
    {
        base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All ,player , flag);
        waitingRoomUI.UpdateReadyButtonPlayer(GetPlayerInfo(player));
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool flag)
    {
        int index = players.FindIndex(x => x.PlayerInfo == player);
        if (index != -1)
        {
            players[index].GetReady(!flag);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        RoomManager.instance.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerToRoom(newPlayer);
        waitingRoomUI.UpdateRoomPlayer();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = players.FindIndex(x => x.PlayerInfo == otherPlayer);
        Destroy(players[index].gameObject);
        players.RemoveAt(index);
    }

}
