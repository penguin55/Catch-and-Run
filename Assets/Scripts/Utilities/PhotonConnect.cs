using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    public string versionName = "0.1";

    private void Start()
    {
        ConnectToPhoton();    
    }

    public void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            MasterManager.GameSettings.GenerateNickname();
            PhotonNetwork.LocalPlayer.NickName = MasterManager.GameSettings.NickName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();

            Debug.Log("Connecting to Photon....");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        Debug.Log(MasterManager.GameSettings.NickName);

        if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server");
    }
}
