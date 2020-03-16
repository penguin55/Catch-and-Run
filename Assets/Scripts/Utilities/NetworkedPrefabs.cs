using UnityEngine;

[System.Serializable]
public class NetworkedPrefabs
{
    public GameObject prefab;
    public string path;

    // Storing the prefabs and its path in Resources folder
    public NetworkedPrefabs(GameObject prefab, string path)
    {
        this.prefab = prefab;
        this.path = GetPathModified(path);
    }

    // Want to modify the path of prefabs path. The original path is still can't read by Photon Instantiate function
    // So I fix it by using this function. This function i've learnt from the "First Gear Games" youtube channel
    private string GetPathModified(string path)
    {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int additionalLength = "resources/".Length;
        int startIndex = path.ToLower().IndexOf("resources");

        if (startIndex == -1)
        {
            return string.Empty;
        } else
        {
            return path.Substring(startIndex + additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
        }
    }
}
