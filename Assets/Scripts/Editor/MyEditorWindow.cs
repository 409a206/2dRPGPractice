using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    string windowName = "My Editor Window";
    bool groupEnabled;
    bool DisplayToggle = true;
    float OffSet = 1.23f;

    [MenuItem("Window/My Window")]
    public static void ShowWindow() {
        //EditorWindow.GetWindow(typeof(MyEditorWindow));
        EditorWindow.GetWindowWithRect(typeof(MyEditorWindow), new Rect(0,0,400,150));
    }

    private void OnGUI() {
        //커스텀 Editor Window GUI 코드
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        windowName = EditorGUILayout.TextField("Window Name", windowName);

        groupEnabled = 
            EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        
        DisplayToggle = 
            EditorGUILayout.Toggle("Display Toggle", DisplayToggle);

            OffSet = EditorGUILayout.Slider("Offset Slider", OffSet, -3, 3);
        
        EditorGUILayout.EndToggleGroup();
    }
}
