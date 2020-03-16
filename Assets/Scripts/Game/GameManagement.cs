using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviourPunCallbacks
{
    public static GameManagement instance;
    public static CatchStatus catchStatus;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnListed;
    [SerializeField] private Cinemachine2D camera;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;

    private Vector3 position;
    private bool ready;
    private PlayerController playerObject;


    private void Start()
    {
        Initialize();

        if (PhotonNetwork.IsMasterClient)
        {
            UIManager.instance.SendCommand(CommandManager.UI.OPEN_EXIT_BUTTON);
            photonView.RPC("SetReady", RpcTarget.All, true);
            StartTheGame();
        }
       
        SetUp();
    }

    private void Initialize()
    {
        catchStatus = new CatchStatus();
        RandomPosition();
        instance = this;
        ready = false;
    }

    private void SetUp()
    {
        if (catchStatus.currentCatch == PhotonNetwork.LocalPlayer)
        {
            playerObject.SetPointerStatus("catcher");
        }
    }

    public void ActiveController()
    {
        jumpButton.gameObject.SetActive(true);
        joystick.gameObject.SetActive(true);
    }

    private void RandomPosition()
    {
        position = spawnListed[Random.Range(0, spawnListed.Length)].position;
    }

    private Player GetRandomPlayer()
    {
        List<int> tempID = new List<int>();

        foreach (int id in PhotonNetwork.CurrentRoom.Players.Keys)
        {
            tempID.Add(id);
        }

        int tempIndex = Random.Range(0, tempID.Count);

        return PhotonNetwork.CurrentRoom.Players[tempID[tempIndex]];
    }

    public void ChangeCatcher(Player newCatcher, Player currentCatch)
    {
        photonView.RPC("SetCatch", RpcTarget.All, newCatcher, currentCatch);
    }

    public void StartTheGame()
    {
        if (ready)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        playerObject = (MasterManager.Instance.NetworkInstantiate(player, position, Quaternion.identity)).GetComponent<PlayerController>();
        camera.SetPlayer(playerObject.gameObject);
        playerObject.SetController(joystick, jumpButton);

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetCatch", RpcTarget.All, GetRandomPlayer(), null);
        }
    }

    public void Gameover()
    {
        TimeManager.freeze = true;
        UIManager.instance.SendCommand(CommandManager.UI.OPEN_GAMEOVER_PANEL);
        UIManager.instance.SendCommandUIText(CommandManager.UI.SET_LAST_CATCHER, catchStatus.currentCatch.NickName);
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MAIN_MENU");
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        LeaveRoom();
    }

    [PunRPC]
    public void SetCatch(Player currentCatch, Player lastCatch)
    {
        catchStatus.lastCatch = lastCatch;
        catchStatus.currentCatch = currentCatch;

        if (catchStatus.currentCatch == PhotonNetwork.LocalPlayer)
        {
            playerObject.SetPointerStatus("catcher");
        }
    }

    [PunRPC]
    public void SetReady(bool flag)
    {
        ready = flag;
        if (!PhotonNetwork.IsMasterClient) StartTheGame();
    }
}

[System.Serializable]
public class CatchStatus
{
    public Player lastCatch;
    public Player currentCatch;
}
