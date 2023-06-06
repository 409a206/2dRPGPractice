using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MainMenu : MonoBehaviour
{
    bool SaveAvailable;
    // Start is called before the first frame update
    void Start()
    {
        SaveAvailable = GameState.SaveAvailable;
    }

    private void OnGUI() {
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 200));
        if(GUILayout.Button("New Game")) {
            NavigationManager.NavigateTo("Home");
        }
        GUILayout.Space(50);
        if(SaveAvailable) {
            if(GUILayout.Button("Load Game")) {
                GameState.LoadState(() => {
                    var lastLocation = PlayerPrefs.GetString("CurrentLocation", "Home");
                    NavigationManager.NavigateTo(lastLocation);
                });
            }
        }
        GUILayout.EndArea();
    }
}
