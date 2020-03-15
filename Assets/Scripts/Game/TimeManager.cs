using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviourPun
{
    public static bool freeze;

    [SerializeField] private float time;
    [SerializeField] private int timeToStartGame;

    private int timeCountingToStart;
    private float firstNetworkTime;
    private float timeCounting;
    private bool timesout;

    private void Start()
    {
        UIManager.instance.SendCommand(CommandManager.UI.HIDE_GAMEOVER_PANEL);
        timeCountingToStart = timeToStartGame;
        freeze = true;
        timesout = false;
        timeCounting = time;
        StartCoroutine(delayStart());
    }

    private void Update()
    {
        if (freeze) return;
        if (!timesout) CountTime();
    }

    private void CountTime()
    {
        timeCounting = time - (((float)PhotonNetwork.Time) - firstNetworkTime);

        if (timeCounting < 0)
        {
            timeCounting = 0;
            timesout = true;
            freeze = true;
            GameManagement.instance.Gameover();
        }

        UIManager.instance.SendCommandUIText(CommandManager.UI.UPDATE_TIME_UI, ((int) timeCounting).ToString());
    }

    IEnumerator delayStart()
    {
        UIManager.instance.SendCommand(CommandManager.UI.OPEN_START_TIMER_TEXT);
        UIManager.instance.SendCommandUIText(CommandManager.UI.UPDATE_START_TIMER, timeCountingToStart.ToString());

        while (timeCountingToStart != 0)
        {
            yield return new WaitForSeconds(1);
            timeCountingToStart--;
            if (timeCountingToStart <= 0)
            {
                timeCountingToStart = 0;
            }
            UIManager.instance.SendCommandUIText(CommandManager.UI.UPDATE_START_TIMER , timeCountingToStart.ToString());
        }

        yield return new WaitForSeconds(0.5f);

        UIManager.instance.SendCommandUIText(CommandManager.UI.UPDATE_START_TIMER, "START!");
        firstNetworkTime = (float)PhotonNetwork.Time;

        yield return new WaitUntil(CheckNetworkTime);
        UIManager.instance.SendCommand(CommandManager.UI.HIDE_START_TIMER_TEXT);
        
        freeze = false;
        UIManager.instance.SendCommand(CommandManager.UI.OPEN_TIME_TEXT);
        UIManager.instance.SendCommandUIText(CommandManager.UI.UPDATE_TIME_UI, ((int)timeCounting).ToString());

    }

    private bool CheckNetworkTime()
    {
        return firstNetworkTime != 0;
    }

}
