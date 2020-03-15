using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cinemachine2D))] 
public class Cinemachine2DEditor : Editor {

    float sizeCamX, sizeCamY;
    bool foldouts;

    public override void OnInspectorGUI()
    {
        Cinemachine2D camera = (Cinemachine2D) target;
        if (camera.GetComponent<Camera>().orthographic)
        {
            SpeedCamera(camera);
        
            SizeCamera(camera);

            BoundsCamera(camera);
        }
        else
        {
            EditorGUILayout.HelpBox("Camera must be Ortographic for use this function", MessageType.Warning);
        }
        GUILayout.Space(40);
        GUILayout.Label("ARCA BANA Scripts", EditorStyles.miniBoldLabel);
    }

    private void SpeedCamera(Cinemachine2D camera)
    {
        camera.smoothTimeX = EditorGUILayout.Slider("Smoothness", camera.smoothTimeX, 0f, 1f);
        camera.smoothTimeY = camera.smoothTimeX;
        GUILayout.Space(5);
    }

    private void SizeCamera(Cinemachine2D camera)
    {
        Camera cam = camera.GetComponent<Camera>();

        cam.orthographicSize = EditorGUILayout.Slider("Camera Size", cam.orthographicSize, 1f, cam.pixelHeight);
        sizeCamY = camera.up = camera.down = cam.orthographicSize;
        sizeCamX = camera.left = camera.right = (cam.pixelWidth / (cam.pixelHeight / cam.orthographicSize));

        foldouts = EditorGUILayout.Foldout(foldouts, "Info");
        if (foldouts)
        {
            EditorGUILayout.LabelField("Screen Size : " + cam.pixelWidth + "x" + cam.pixelHeight, EditorStyles.miniBoldLabel);
            EditorGUILayout.LabelField("Camera Size : " + sizeCamX + "x" + sizeCamY, EditorStyles.miniBoldLabel);
            GUILayout.Space(10);
        }



    }

    private void BoundsCamera(Cinemachine2D camera)
    {
        camera.rightBound = (Transform) EditorGUILayout.ObjectField("Right Bound",camera.rightBound, typeof(Transform));
        camera.leftBound = (Transform) EditorGUILayout.ObjectField("Left Bound", camera.leftBound, typeof(Transform));
        camera.upBound = (Transform) EditorGUILayout.ObjectField("Up Bound", camera.upBound, typeof(Transform));
        camera.downBound = (Transform) EditorGUILayout.ObjectField("Down Bound", camera.downBound, typeof(Transform));
    }
}
