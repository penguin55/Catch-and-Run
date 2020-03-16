using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;

    // It's Instance SingletonScriptable, I'm not sure about how it's work. I know that this script is for single instance for scriptable object
    // I get this script from "First Gear Games" youtube channel about using of Photon Unity Networking 2
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is 0 for typr" + typeof(T).ToString()+".");
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is greather than 1 for typr" + typeof(T).ToString() + ".");
                    return null;
                }

                _instance = results[0];
            }
            return _instance;
        }
    }
}
