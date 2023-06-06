using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyMenu
{
   //MenuItem1이라는 메뉴 아이템을 메뉴 바에 있는
   //MenuName이라는 Menu옵션에 추가한다.
   [MenuItem("MenuName/MenuItem1")]
   static void EnableMyAwesomeFeature() {
       Debug.Log("I am a leaf on the wind. Watch how I soar");
   }

   [MenuItem("MenuName/MenuItem1", true)]
   static bool CheckifaGameObjectisselected() {
       //선택된 트랜스폼이 없으면 false를 반환
       return Selection.activeTransform != null;
   }

   [MenuItem("MenuName/MenuItem2 %g")]
   static void EnableMyOtherAwesomeFeature() {
       Debug.Log("Find my Key and win the prize -g");
   }

   [MenuItem("CONTEXT/Transform/Move to Center")]
   static void MoveToCenter(MenuCommand command) {
       Transform transform = (Transform)command.context;
       transform.position = Vector3.zero;
       Debug.Log("Moved object to " + transform.position + " from a Context Menu.");
   }

   private void OnEnable() {
       //이벤트/델리게이트 등록, 주로 OnEnable 함수에 등록함 
       EditorApplication.hierarchyWindowChanged += HierarchyWindowChanged;
   }
   //이벤트 발생에 대응하는 콜백 함수
   void HierarchyWindowChanged() {
       //새 아이템을 위한 계층 구조 탐색
       //새 아이템을 발견하면 에디터 창에 추가
   }

   private void OnDestroy() {
       //영역을 벗어나거나 필요하지 않게 되면 델리게이트 등록을 해제한다.
       EditorApplication.hierarchyWindowChanged -= HierarchyWindowChanged;
   }
}
