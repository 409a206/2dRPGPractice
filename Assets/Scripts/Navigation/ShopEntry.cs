using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    bool canEnterShop;

    void DialogVisible(bool visibility) {
        canEnterShop = visibility;
        MessagingManager.Instance.BroadcastUIEvent(visibility);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DialogVisible(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        DialogVisible(false);
    }

    private void Update() {
        if(canEnterShop && Input.GetKeyDown(KeyCode.UpArrow)) {
            if(NavigationManager.CanNavigate(this.tag)) {
                NavigationManager.NavigateTo(this.tag);
            }
        }
    }

    private void OnGUI() {
        if(canEnterShop) {
            //레이아웃 시작
            GUI.BeginGroup(
                new Rect(
                    Screen.width / 2 - 150,
                    50,
                    300,
                    50));

            //메뉴 백그라운드 박스
            GUI.Box(new Rect(0,0,300,250), "");

            //대화상자 상세 내용 추가
            GUI.Label(
                new Rect(15,10,300,68),
                "Do you want to Enter the Shop? (Press Up)");
            
            //레이아웃 끝
            GUI.EndGroup();
        }
    }
}
