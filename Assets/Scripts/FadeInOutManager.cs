using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutManager : Singleton<FadeInOutManager>
{
    //싱글톤에서는 생성자 사용이 불가능하기 때문에 protected로 선언한다.
   protected FadeInOutManager(){}

    //페이딩(Fading) 도중 보여줄 텍스처
    private Material fadeMaterial;
    //페이딩 파라미터
    private float fadeOutTime, fadeInTime;
    private Color fadeColor;

    //앞으로 탐험할 레벨에 대한 플레이스 홀더(Place hloder)
    //(이름 또는 인덱스)
    private string navigateToLevelName = "";
    private int navigateToLevelIndex = 0;

    //페이딩 상태 제어를 위한 속성과
    // 코드 접근을 위한 퍼블릭(public) 속성
    private bool fading = false;
    public static bool Fading{
        get{return Instance.fading;}
    }

    private void Awake() {
        //재질(Material)이 제공되지 않을 땐 기본으로 빈 텍스처로 설정한다.
       fadeMaterial = new Material("Shader \"Plane/No zTest\" {" +
			"SubShader { Pass { " +
			"    Blend SrcAlpha OneMinusSrcAlpha " +
			"    ZWrite Off Cull Off Fog { Mode Off } " +
			"    BindChannels {" +
			"      Bind \"color\", color }" +
			"} } }");
    }

    private IEnumerator Fade() {
        float t = 0.0f;
        while (t < 1.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(
                t + Time.deltaTime / fadeOutTime);
            DrawingUtilities.DrawQuad(
                fadeMaterial,
                fadeColor,
                t
            );
        }
        if(navigateToLevelName != "") {
            Application.LoadLevel(navigateToLevelName);
        } else {
            Application.LoadLevel(navigateToLevelIndex);
        }
            while(t > 0.0f) {
                yield return new WaitForEndOfFrame();
                t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
                DrawingUtilities.DrawQuad(
                    fadeMaterial,
                    fadeColor,
                    t);
            }
        fading = false;
    }

    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor) {
        fading = true;
        Instance.fadeOutTime = aFadeOutTime;
        Instance.fadeInTime = aFadeInTime;
        Instance.fadeColor = aColor;
        StopAllCoroutines();
        StartCoroutine("Fade");
    }

    public static void FadeToLevel(
        string aLevelName,
        float aFadeOutTime,
        float aFadeInTime,
        Color aColor) {
            if(Fading) return;
            Instance.navigateToLevelName = aLevelName;
            Instance.StartFade(aFadeOutTime, aFadeInTime, aColor); 
        }

}

public static class DrawingUtilities {
    //전체 화면 텍스처를 그리기 위한 헬퍼 유틸리티
    public static void DrawQuad(
        Material aMaterial,
        Color aColor,
        float aAlpha) {
            aColor.a = aAlpha;
            aMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);
            GL.Color(aColor);
            GL.Vertex3(0,0,-1);
            GL.Vertex3(0,1,-1);
            GL.Vertex3(1,0,-1);
            GL.End();
            GL.PopMatrix();
        }

}