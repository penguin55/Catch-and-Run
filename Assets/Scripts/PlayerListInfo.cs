using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListInfo : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Text status;

    public Player PlayerInfo { get; private set; }
    public bool Ready { get; private set; }

    public void SetRoomInfo(Player playerInfo)
    {
        PlayerInfo = playerInfo;
        playerName.text = playerInfo.NickName;
        if (playerInfo.IsMasterClient)
        {
            status.text = "Room Master";
        } else
        {
            status.text =  "Not Ready";
        }
        Ready = false;
    }

    public void GetReady(bool flag)
    {
        if (!PlayerInfo.IsMasterClient)
        {
            Ready = flag;
            status.text = flag ? "Ready" : "Not Ready";
        }
    }
}
