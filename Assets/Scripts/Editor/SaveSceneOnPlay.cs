using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SaveSceneOnPlay
{
   //스태틱 생성자
   //유니티가 시작될 때 바로 초기화된다
   static SaveSceneOnPlay() {
       EditorApplication.playmodeStateChanged += SaveSceneIfPlaying;
   }

   static void SaveSceneIfPlaying() {
       if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying) {
           Debug.Log("Automatically saving scene (" + EditorApplication.currentScene + ") before entering play mode");

           AssetDatabase.SaveAssets();
           EditorApplication.SaveScene();
       }
   }

  
}
