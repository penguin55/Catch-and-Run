#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (GUI.changed)
        {
            EditorUtility.SetDirty(masterManagerTarget);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
#endif