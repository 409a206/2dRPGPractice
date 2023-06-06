using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPrompt : MonoBehaviour
{
    bool showDialog;

    private void OnCollisionEnter2D(Collision2D other) {
        //이동할 수 있을때만 플레이어의 이동을 허가한다
        if(NavigationManager.CanNavigate(this.tag)) {
            DialogVisible(true);        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        DialogVisible(false);
    }

    private void OnGUI() {

        if(showDialog){
            //레이아웃 시작
            GUI.BeginGroup(new Rect(Screen.width/2 - 150, 50, 300, 250));

            //메뉴 배경 박스
            GUI.Box(new Rect(0,0,300,250), "");

            //정보 텍스트
            GUI.Label(new Rect(15,10,300,68), " Do you want to travel to " + NavigationManager.GetRouteInfo(this.tag) + "?");

            //플레이어가 이 지역을 벗어나야 함
            if(GUI.Button(new Rect(55, 100, 180, 40), "Travel")) {
                DialogVisible(false);
                NavigationManager.NavigateTo(this.tag);
                
            }

            if(GUI.Button(new Rect(55, 150, 180, 30), "Stay")) {
                DialogVisible(false);
            }

            //레이아웃 끝
            GUI.EndGroup();

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //허용됐을 때만 플레이어의 탐험을 허용
        if(NavigationManager.CanNavigate(this.tag)) {
            DialogVisible(true);
        }
    }

   

    void DialogVisible(bool visibility) {
        showDialog = visibility;
        MessagingManager.Instance.BroadcastUIEvent(visibility);
    }

    
}
