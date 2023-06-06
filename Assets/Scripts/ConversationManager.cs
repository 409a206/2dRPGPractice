using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ConversationManager : Singleton<ConversationManager>
{
    //싱글톤에선 생성자를 사용할 수 없음
    protected ConversationManager(){}


    //대화가 진행 중인지
    bool talking = false;
    //출력 중인 라인
    ConversationEntry currentConversationLine;
    //폰트의 폭
    int fontSpacing = 7;
    //필요한 대화 창의 폭
    int conversationTextWidth;
    //필요한 대화 창의 높이
    int dialogHeight = 70;

    //캐릭터 이미지에 필요한 오프셋 공간
    public int displayTextureOffset = 70;
    //캐릭터 이미지 출력을 위한 스케일 처리된 이미지 사각 영역
    Rect scaledTextureRect;

    public void StartConversation(Conversation conversation) {
           //대화 출력 시작
        if(!talking) {
            StartCoroutine(DisplayConversation(conversation));
        }
    }

    IEnumerator DisplayConversation(Conversation conversation) {
        talking = true;
        foreach (var conversationLine in conversation.ConversationLines)
        {
            currentConversationLine = conversationLine;

             conversationTextWidth = 
                currentConversationLine.conversationText.Length * fontSpacing;

            currentConversationLine.SpeakingCharacterName = conversationLine.SpeakingCharacterName;
             currentConversationLine.conversationText = conversationLine.conversationText;

            scaledTextureRect = new Rect(
                currentConversationLine.DisplayPic.textureRect.x/
                currentConversationLine.DisplayPic.texture.width,
                
                currentConversationLine.DisplayPic.textureRect.y /
                currentConversationLine.DisplayPic.texture.height,

                currentConversationLine.DisplayPic.textureRect.width/
                currentConversationLine.DisplayPic.texture.width,

                currentConversationLine.DisplayPic.textureRect.height/
                currentConversationLine.DisplayPic.texture.height
                );


            // conversationTextWidth = 
            //     conversationLine.conversationText.Length * fontSpacing;


                Debug.Log("scaledTextureRect : " + scaledTextureRect);

            yield return new WaitForSeconds(3);
        }
        talking = false;
        yield return null;
    }

    private void OnGUI() {
        if(talking) {
            //레이아웃 시작
            GUI.BeginGroup(new Rect(Screen.width / 2 - conversationTextWidth / 2, 50, conversationTextWidth + displayTextureOffset + 10, dialogHeight));

            //배경 상자
            GUI.Box(new Rect(0, 0, conversationTextWidth + displayTextureOffset + 10, dialogHeight), "");

            //캐릭터 이름
            GUI.Label(new Rect(displayTextureOffset, 10, conversationTextWidth + 30, 20), currentConversationLine.SpeakingCharacterName);

            //대화 텍스트
            GUI.Label(new Rect(displayTextureOffset, 30, conversationTextWidth + 30, 20), currentConversationLine.conversationText);

            //캐릭터 이미지
            GUI.DrawTextureWithTexCoords(new Rect(10, 10, 50, 50), currentConversationLine.DisplayPic.texture, scaledTextureRect);

            //레이아웃 종료
            GUI.EndGroup();
        }
    }

 

}
