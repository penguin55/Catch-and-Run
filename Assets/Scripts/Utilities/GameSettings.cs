using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    //The game settings is for settings of Photon we connects to. Sending to photon with this settings to prevent the other version of game 
    //can't be one server, if the version is different each other.
    [SerializeField] private string gameVersion = "0.0.1";
    public string GameVersion { get { return gameVersion; } }

    [SerializeField] private string nickName = "Penguin";
    private string generatedUnique;
    public string NickName
    {
        get
        {
            return nickName+"-"+generatedUnique;
        }
    }

    public void GenerateNickname()
    {
        generatedUnique = Random.Range(0, 9999).ToString();
    }
}
