using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PositionManager : MonoBehaviour
{   
    //새로운 애셋을 만들기 위해 편집기에서 메뉴 옵션을 정의한다
    [MenuItem("Assets/Create/PositionManager")]

    public static void CreateAsset() {
        //스크립트 가능 오브젝트의 인스턴스를 새로 만든다
        ScriptingObjects positionManager = 
            ScriptableObject.CreateInstance<ScriptingObjects>();

        //새 오브젝트를 위한 애셋 파일을 만들고 저장한다
        AssetDatabase.CreateAsset(positionManager, "Assets/newPositionManager.asset");
        AssetDatabase.SaveAssets();

        //인스펙터에서 새 오브젝트를 선택한다
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = positionManager;
    }

    public static PositionManager ReadPositionsFromAsset(string Name) {
        string path = "/";
        object o = Resources.Load(path + Name);
        PositionManager retrievedPositions = (PositionManager)o;
        return retrievedPositions;
    }

}
