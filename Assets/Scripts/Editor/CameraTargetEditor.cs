using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraLookAt))]
public class CameraTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraLookAt targetScript = (CameraLookAt) target;

        targetScript.cameraTarget =
            EditorGUILayout.Vector3Field ("Look At Point", targetScript.cameraTarget);
            if(GUI.changed)
                EditorUtility.SetDirty(target);
    }

    private void OnSceneGUI() {
        CameraLookAt targetScript = (CameraLookAt) target;

        targetScript.cameraTarget = Handles.PositionHandle(
            targetScript.cameraTarget, Quaternion.identity);

        Handles.SphereHandleCap(0, targetScript.cameraTarget, Quaternion.identity, 2, EventType.MouseDrag);
        
        if(GUI.changed)
            EditorUtility.SetDirty(target);
    }
}
