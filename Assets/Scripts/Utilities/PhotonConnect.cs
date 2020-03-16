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

    // To connect to photon using a setting
    public void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            MasterManager.GameSettings.GenerateNickname();
            PhotonNetwork.LocalPlayer.NickName = MasterManager.GameSettings.NickName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();

            MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_CONNECTING_PANEL);

            MainMenuManager.instance.ConnectionLog("", true);
        }
    }

    // Automatically called when we are connected to Photon Server and want to give a message to player
    // that they are connected correctly
    public override void OnConnectedToMaster()
    {
        MainMenuManager.instance.SetCommand(CommandManager.UI.HIDE_CONNECTING_PANEL);
        MainMenuManager.instance.SetCommand(CommandManager.UI.OPEN_MENU_PANEL);
        MainMenuManager.instance.ConnectionLog("Connected to Server", true);

        if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
    }

    // Automatically called when we are disconnected from Photon Server and want to give a message by UI text
    // that they are disconnected from server
    public override void OnDisconnected(DisconnectCause cause)
    {
        MainMenuManager.instance.ConnectionLog("Disconnected from Server", false);
    }
}
