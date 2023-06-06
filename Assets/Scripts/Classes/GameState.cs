using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class GameState 
{
    public static Player CurrentPlayer = 
        ScriptableObject.CreateInstance<Player>();
    public static bool PlayerReturningHome;
    public static Dictionary<string, Vector3> LastScenePositions = 
        new Dictionary<string, Vector3>();

    static string saveFilePath = 
        Application.persistentDataPath + "/playerstate.dat";

    public static bool SaveAvailable {
        get {return File.Exists(saveFilePath);}
    }
        
    public static Vector3 GetLastScenePosition(string sceneName) {
        if(GameState.LastScenePositions.ContainsKey(sceneName)) {
            var lastPos = GameState.LastScenePositions[sceneName];
            return lastPos;
        } else {
            return Vector3.zero;
        }
    }

    public static void SetLastSceneposition(
        string sceneName, Vector3 position) {
            if(GameState.LastScenePositions.ContainsKey(sceneName)) {
                GameState.LastScenePositions[sceneName] = position;
            } else {
                GameState.LastScenePositions.Add(sceneName, position);
            }
    }

    public static void SaveState() {
        try {
            PlayerPrefs.SetString("CurrentLocation", Application.loadedLevelName);

            var playerSerializedState = 
                SerializationHelper.Serialise<PlayerSaveState>(CurrentPlayer.GetPlayerSaveState());

            #if UNITY_METRO

                UnityEngine.Windows.File.WriteAllBytes(saveFilePath, playerSerializedState);

            #else

                using (var file = File.Create(saveFilePath)) {
                    // var playerSerializedState = 
                    //     SerializationHelper.Serialise<PlayerSaveState>(CurrentPlayer.GetPlayerSaveState());
                    file.Write(playerSerializedState, 0, playerSerializedState.Length);
                }

            #endif
        } catch{
            Debug.LogError("Saving data failed");
        }
    }

    public static void LoadState(Action LoadComplete) {
        PlayerSaveState LoadedPlayer;
        try {
            if(SaveAvailable) {
                #if UNITY_METRO
                    var playerSerializedState = 
                        UnityEngine.Windows.File.ReadAllBytes(saveFilePath);
                    LoadedPlayer = 
                        SerializationHelper.Deserialise<PlayerSaveState>(playerSerializedState);
                #else
                    //파일 얻기
                    using (var stream = File.Open(saveFilePath, FileMode.Open)) {
                        LoadedPlayer =
                            SerializationHelper.Deserialise<PlayerSaveState>(stream);
                }
                #endif
                            CurrentPlayer = 
                                LoadedPlayer.LoadPlayerSaveState(CurrentPlayer);
            }
        } catch{
            Debug.LogError("Loading data failed, file is corrupt");
        }
        LoadComplete();
    }


}
