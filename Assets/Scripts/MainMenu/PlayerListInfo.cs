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
        if (playerInfo.IsMasterClient)
        {
            status.text = "Room Master";
            status.color = Color.yellow;
        }
        else
        {
            GetReady((bool)playerInfo.CustomProperties[CommandManager.PROPS.READY_PLAYER_STATUS]);
        }
    }

    public void GetReady(bool flag)
    {
        if (!PlayerInfo.IsMasterClient)
        {
            status.text = flag ? "Ready" : "Not Ready";
            status.color = flag ? Color.green : Color.red;
        }
    }
}
