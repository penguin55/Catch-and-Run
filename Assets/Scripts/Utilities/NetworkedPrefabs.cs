using UnityEngine;

[System.Serializable]
public class NetworkedPrefabs
{
    public GameObject prefab;
    public string path;

    public NetworkedPrefabs(GameObject prefab, string path)
    {
        this.prefab = prefab;
        this.path = GetPathModified(path);
    }

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
