using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public string versionName = "0.1";

    public void ConnectToPhoton()
    {
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.LocalPlayer.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Connecting to Photon....");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log(MasterManager.GameSettings.NickName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server");
    }
}
