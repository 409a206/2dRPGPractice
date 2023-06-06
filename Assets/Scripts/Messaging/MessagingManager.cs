using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MessagingManager : Singleton<MessagingManager>
{
    //정적 싱글톤 속성
    // public static MessagingManager Instance {
    //     get; private set;
    // }
    protected MessagingManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    //true면 GUI가 보이는 상태, false면 GUI가 보이지 않는 상태
    private List<Action<bool>> uiEventSubscribers = new List<Action<bool>>();
    //퍼블릭 속성
    private List<Action> subscribers = new List<Action>();
    public ScriptingObjects MyWayPoints;
    private List<Action<InventoryItem>> inventorySubscribers = new 
        List<Action<InventoryItem>>();

    private void Awake() {
        Debug.Log("Messaging Manager Started");

        //일단 충돌하는 다른 인스턴스가 있는지 검사
        if(Instance != null && Instance != this) {
            //동일하지 않으면 인스턴스를 제거한다
            Destroy(gameObject);
        }

        //현재 싱글톤을 저장
        // Instance = this;

        //씬 전환 간에 싱글톤이 제거되지 않게 한다(옵션)
        DontDestroyOnLoad(gameObject);
    }

    //수신자 등록 메소드
    public void Subscribe(Action subscriber) {
        Debug.Log("Subscriber registered");
        subscribers.Add(subscriber);
    }

    //수신자 해지 메소드
    public void UnSubscribe(Action subscriber) {
        Debug.Log("Subscriber registered");
        subscribers.Remove(subscriber);
    }

    //모든 수신자 해지 메소드
    public void ClearAllSubscribers() {
        subscribers.Clear();
    }

    public void Broadcast() {
        Debug.Log("Broadcast requested, No of Subscribers = " + subscribers.Count);
        foreach(var subscriber in subscribers) {
            subscriber();
        }
    }

    //UI 매니저를 위한 수신 메소드
    public void SubscribeUIEvent(Action<bool> subscriber) {
        uiEventSubscribers.Add(subscriber);
    }

    //UI 매니저를 위한 브로드캐스트 메소드
    public void BroadcastUIEvent(bool UIVisible) {
        foreach (var subscriber in uiEventSubscribers.ToArray())
        {
            subscriber(UIVisible);
        }
    }

    //UI 매니저를 위한 수신 해제 메소드
    public void UnSubscribeUIEvent(Action<bool> subscriber) {
        uiEventSubscribers.Remove(subscriber);
    }

    //매니저를 위한 수신자 정리 메소드
    public void ClearAllEventSubscribers() {
        uiEventSubscribers.Clear();
    }

    //Inventory 매니저를 위한 구독 메소드
    public void SubscribeInventoryEvent
    (Action<InventoryItem> subscriber) {
        if(inventorySubscribers != null) {
            inventorySubscribers.Add(subscriber);
        }
    }

    //Inventory 매니저를 위한 브로드캐스트 메소드
    public void BroadcastInventoryEvent(InventoryItem itemInUse) {
        foreach (var subscriber in inventorySubscribers)
        {
            subscriber(itemInUse);
        }
    }

    //Inventory 매니저를 위한 구독 해지 메소드
    public void UnSubscribeInventoryEvent(Action<InventoryItem> subscriber) {
        if(inventorySubscribers != null) {
            inventorySubscribers.Remove(subscriber);
        }
    } 

    //Inventory 매니저를 위한 전체 구독 해지 메소드
    public void ClearAllInventoryEventSubscribers() {
        if(inventorySubscribers != null) {
            inventorySubscribers.Clear();
        }
    }
}
