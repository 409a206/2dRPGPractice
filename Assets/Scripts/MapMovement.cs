using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public AnimationCurve MovementCurve;
    bool startedTravelling = false;
    Vector3 StartLocation;
    Vector3 TargetLocation;
    float timer = 0;
    bool inputActive = true;
    bool inputReady = true;
    private BoxCollider2D myBoxCollider2D;
    int EncounterChance = 100;
    float EncounterDistance = 0;

    private void Awake() {
        myBoxCollider2D = this.GetComponent<BoxCollider2D>();
       myBoxCollider2D.enabled = false;

       var lastPosition = 
        GameState.GetLastScenePosition(Application.loadedLevelName);

        if(lastPosition != Vector3.zero) {
            transform.position = lastPosition;
        }
    }

    private void OnDestroy() {
        GameState.SetLastSceneposition(Application.loadedLevelName, transform.position);
    }

    private void Start() {
        MessagingManager.Instance.SubscribeUIEvent(UpdateInputAction);
    }

    private void UpdateInputAction(bool UIVisible)
    {
        inputReady = !UIVisible;
    }

    private void Update() {
        if(inputActive && Input.GetMouseButtonUp(0)) {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.mousePosition);
            startedTravelling = true;
            //전투 발생이 예정되면 플레이어가 전투 만남을 위해 이동해야 하는 거리를 설정한다
            var EncounterProbability = UnityEngine.Random.Range(1, 100);
            if(EncounterProbability < EncounterChance && !GameState.PlayerReturningHome) {
                EncounterDistance = (Vector3.Distance(StartLocation, TargetLocation)/100) * UnityEngine.Random.Range(10,100);
            } else {
                EncounterDistance = 0;
            }
        }
        else if (inputActive && Input.touchCount == 1) {
            StartLocation = transform.position.ToVector3_2D();
            timer = 0;
            TargetLocation = WorldExtensions.GetScreenPositionFor2D(Input.GetTouch(0).position);
            startedTravelling = true;
        }

        if(TargetLocation != Vector3.zero && TargetLocation != transform.position && TargetLocation != StartLocation) {
            transform.position = Vector3.Lerp(StartLocation, TargetLocation, MovementCurve.Evaluate(timer));
            timer += Time.deltaTime;
        }

        if(startedTravelling && Vector3.Distance(StartLocation, transform.position.ToVector3_2D()) > 0.5) {
            myBoxCollider2D.enabled = true;
            startedTravelling = false;
        }

        //EncounterDistance가 0보다 크면 전투는 무조건 발생한다
        //따라서 플레이어가 충분히 이동하면 멈추고 전투 씬으로 진입해야 한다
        if(EncounterDistance > 0) {
            if(Vector3.Distance(StartLocation, transform.position) > EncounterDistance) {
                TargetLocation = Vector3.zero;
                NavigationManager.NavigateTo("Battle");
            }
        }

        if(!inputReady && inputActive) {
            TargetLocation = this.transform.position;
            Debug.Log("Stopping Player");
        }

        inputActive = inputReady;
    }
}
