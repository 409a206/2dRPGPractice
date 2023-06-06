using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//위임패턴(Delegate)
//설정 가능 메소드 패턴(configurable method pattern)
public class Delegates : MonoBehaviour
{
    public class Worker {
        List<string> WorkCompletedfor = new List<string>();
        public void DoSomething( 
            string ManagerName,
            Action myDelegate) {
                //어떤 매니저의 작업이 끝났는지 감시한다
                WorkCompletedfor.Add(ManagerName);
                myDelegate();
        }
    }

    public class Manager {
        private Worker myWorker = new Worker();
        public void PieceOfWork1() {
            //아주 오래 걸리는 작업
        }

        public void PieceOfWork2() {
            //좀 더 오래 걸리는 작업
        }

        public void DoWork() {
            //작업 1을 수행하게 한다
            myWorker.DoSomething("Manager1", PieceOfWork1);
            //작업 2를 수행하게 한다
            myWorker.DoSomething("Manager1", PieceOfWork2);
        }

        //C# 람다 표현식을 사용하면 함수 정의를 분리할 필요가 없다
        public void DoWork2() {
            Worker myWorker = new Worker();

            //작업 1을 수행하게 한다
            myWorker.DoSomething("Manager1", () => {
                //아주 오래 걸리는 작업
            });

            myWorker.DoSomething("Manager2", () => {
                //좀 더 오래 걸리는 작업
            });

        }
    }
    

    //위임 메소드 시그니처(signature) 정의
    delegate void RobotAction();
    //위임을 위한 프라이빗(private) 속성
    RobotAction myRobotAction;

    private void Start() {
        //위임을 위한 기본 메소드 설정
        myRobotAction = RobotWalk;
    }

    private void Update() {
        //업데이트 때 선택된 위임을 실행
        myRobotAction();
    }

    //로봇을 걷게 하는 퍼블릭(public) 메소드
    public void DoRobotWalk() {
        //위임 메소드를 걷기로 설정
        myRobotAction = RobotWalk;
    }

    void RobotWalk() {
        Debug.Log("Robot walking");
    }

    //로봇을 뛰게 하는 퍼블릭(public) 메소드
    public void DoRobotRun() {
        //위임 메소드를 뛰기로 설정
        myRobotAction = RobotRun;
    }

    void RobotRun() {
        Debug.Log("Robot Running");
    }
}


public class WorkerManager {
    void DoWork() {
        DoJob1();
        DoJob2();
        DoJob3();
    }

    private void DoJob3()
    {
        //파일 정리를 한다
    }

    private void DoJob2()
    {
        //사무실에서 커피를 탄다
    }

    private void DoJob1()
    {
        //업무 관련 논의를 한다
    }
}

//합성 위임(chained delegate) 예시
public class WorkerManager2 {
    
    //WorkerManager 위임
    delegate void MyDelegateHook();
    MyDelegateHook ActionsToDo;
    public string WorkerType = "Peon";

    //시작할 때 워커(worker)에게 작업을 할당한다
    //작업을 설정 가능한 형태로도 사용할 수 있다
    private void Start() {
        if(WorkerType == "Peon") {
            ActionsToDo += DoJob1;
            ActionsToDo += DoJob2;

        }
    //그 외 나머지는 골프를 즐긴다
        else {
            ActionsToDo += DoJob3;
        }
    }

    //Update에서 ActionsToDo에 지정된 일을 수행한다
    private void Update() {
        ActionsToDo();
    }

    private void DoJob3()
    {
        //골프를 즐긴다
    }


    private void DoJob2()
    {
       //사무실에서 커피를 탄다
    }

    private void DoJob1()
    {
        //파일 정리를 한다
    }
}

//이벤트 예시
public class Event {
    //위임 메소드 정의
    public delegate void ClickAction();
    //위임 메소드 시그니처를 사용한 이벤트 훅(hook)
    public static event ClickAction OnClicked;
    
    //이벤트 발생 시마다 로그 메시지를 전달한다
    public delegate void LogMessage(string message);
    public static event LogMessage Log;

    void OnLog(string message) {
        if(Log != null) {
            Log(message);
        }
    }

    
    private void Start() {
        //함수의 OnClicked에 이벤트가 발생했을 때 알림을 받을 함수를 지정하고
        //이벤트가 발생하면 실행한다
        OnClicked += Events_OnClicked;
    }

    private void Update() {
        //스페이스바가 눌리면 아이템이 클릭된 상태다
        if(Input.GetKeyDown(KeyCode.Space)) {
            //수신 대상이 있다면 이벤트 위임을 발생시킨다
            Clicked();
        }
    }

    private void Events_OnClicked()
    {
       Debug.Log("The button was clicked");
    }

    void Destroy() {
        //이벤트 수신을 받지 않게 한다
        OnClicked -= Events_OnClicked;
    }

    //안전한 이벤트 호출 메소드
    void Clicked() {
        //수신자가 있다면 이벤트 위임을 발생시킨다
        if(OnClicked != null) {
            OnClicked();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        //SendMessage, BroadCastMessage는 성능이 낮아서 안 쓰는것이 좋음

        //어떤물체인지 상관 없이 IHitYou 메소드 호출
        other.gameObject.SendMessage("IHitYou");  

        //이렇게 구현하면 충돌 대상에 IHitYou 메소드가 없을 시 에러 발생
        other.gameObject.SendMessage("IHitYou", SendMessageOptions.RequireReceiver);  

        //BroadcastMessage는 선택된 gameObject와 그의 모든 자식에게까지 전달됨
        other.gameObject.BroadcastMessage("IHitYou");
    }

   
}
