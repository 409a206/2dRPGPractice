using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingletonManager : MonoBehaviour
{   
    //정적 싱글톤 속성
    public static MySingletonManager Instance {
        get; private set;
    }
    //퍼블릭 속성의 매니저
    public string MyTestProperty = "Hello World";

    private void Awake() {

        //충돌하는 또 다른 인스턴스가 있는지 확인
        if(Instance != null && Instance != this) {
            //동일하지 않다면 다른 인스턴스를 소멸시킨다
            Destroy(gameObject);
        }

        //현재 싱글톤 인스턴스 저장
        Instance = this;

        //씬 간에 인스턴스가 소멸되지 않게 처리
        DontDestroyOnLoad(gameObject);
    }

    //매니저를 위한 퍼블릭 메소드
    public void DoSomethingAwesome() {

    }
}
