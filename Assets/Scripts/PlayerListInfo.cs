using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListInfo : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Text status;

    public Player PlayerInfo { get; private set; }

    public void SetRoomInfo(Player playerInfo)
    {
        PlayerInfo = playerInfo;
        playerName.text = playerInfo.NickName;
        status.text = "Not Ready";
    }

    public void GetReady(bool flag)
    {
        status.text = flag ? "Ready" : "Not Ready";
    }
}
