#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MasterManager))]
public class MasterManagerEditor : Editor
{
    private MasterManager masterManagerTarget;

    public override void OnInspectorGUI()
    {
        masterManagerTarget = (MasterManager) target;

        base.DrawDefaultInspector();

        if (GUILayout.Button("Get Resources"))
        {
            masterManagerTarget.GetPrefabsResource();
        }
    }
}
#endif