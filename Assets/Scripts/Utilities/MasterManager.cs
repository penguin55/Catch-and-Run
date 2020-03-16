using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField] private GameSettings gameSettings;
    public static GameSettings GameSettings { get { return Instance.gameSettings; } }

    [SerializeField] private List<NetworkedPrefabs> networkPrefabs = new List<NetworkedPrefabs>();

    // To Instantiate object we want by network, I'm instantiate it by reference the object, not from its path like Photon do.
    // Just want make a function like Unity Instantiate
    public GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (NetworkedPrefabs prefab in networkPrefabs)
        {
            if (prefab.prefab == obj)
            {
                if (prefab.path != string.Empty)
                {
                    GameObject objectInstantiated = PhotonNetwork.Instantiate(prefab.path, position, rotation);
                    return objectInstantiated;
                } else
                {
                    Debug.LogError("Path is empty for : "+prefab.prefab.name);
                    return null;
                }
            } 
        }

        return null;
    }
#if UNITY_EDITOR
    // To get Resource from "Resources" folder and save it to my networkPrefabs list
    public void GetPrefabsResource()
    {
        if (!Application.isEditor) return;

        Instance.networkPrefabs.Clear();
        string path = "";
        GameObject[] results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                path = AssetDatabase.GetAssetPath(results[i]);
                Instance.networkPrefabs.Add(new NetworkedPrefabs(results[i], path));
            }
        }
    }
#endif
}
