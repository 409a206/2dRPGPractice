using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //카메라가 따라오기 전까지 플레이어가 x축으로 이동할 수 있는 거리
    public float xMargin = 1.5f;

    //카메라가 따라오기 전까지 플레이어가 y축으로 이동할 수 있는 거리
    public float yMargin = 1.5f;

    //카메라가 얼마나 부드럽게 x축 이동으로 목표물을 잡아내는지를 결정
    public float xSmooth = 1.5f;

    //카메라가 얼마나 부드럽게 y축 이동으로 목표물을 잡아내는지를 결정
    public float ySmooth = 1.5f;

    //카메라가 가질 수 있는 최대 x와 y좌표
    private Vector2 maxXAndY;

     //카메라가 가질 수 있는 최소 x와 y좌표
    private Vector2 minXAndY;

    //플레이어 트랜스폼에 대한 참조
    public Transform player;

    private Camera camera;

    private void Awake() {

        camera = this.GetComponent<Camera>();
        //배경 텍스처에 대한 경계를 얻는다 ? 월드 크기

        var backgroundBounds = 
            GameObject.Find("background").GetComponent<Renderer>().bounds;

        //월드 좌표계에서 카메라가 볼 수 있는 경계를 얻음
        var camTopLeft = camera.ViewportToWorldPoint(new Vector3(0,0,0));

        var camBottomRight = camera.ViewportToWorldPoint(new Vector3(1,1,0));

        //Debug.Log(camBottomRight);

        //자동으로 min과 max값 설정
        minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
        maxXAndY.x = backgroundBounds.max.x - camBottomRight.x;


        player = GameObject.Find("Player").transform;

        if(player == null) {
            Debug.LogError("Player object not found");
        }
    }

    bool CheckXMargin() {
        //카메라와 플레이어 사이의 x축 거리가 x 마진보다 크면 참을 반환
        return Mathf.Abs(this.transform.position.x - player.position.x) > xMargin;
    }

       bool CheckYMargin() {
        //카메라와 플레이어 사이의 y축 거리가 y 마진보다 크면 참을 반환
        return Mathf.Abs(this.transform.position.y - player.position.y) > yMargin;
    }

    private void FixedUpdate() {
        //기본적으로 카메라 목표물의 x와 y좌표는 현재 카메라의 x와 y좌표다
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        //플레이어가 x마진을 넘어 이동했다면
        if(CheckXMargin()) {
            //목표 x좌표는 카메라의 현재 x 위치와 플레이어의 현재 x위치에 대한
            //선형 보간법으로 구한다

            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
        }
        
        //플레이어가 y마진을 넘어 이동했다면
        if(CheckYMargin()) {
            //목표 y좌표는 카메라의 현재 y 위치와 플레이어의 현재 x위치에 대한
            //선형 보간법으로 구한다

            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);
        }

        //목표물 x와 y좌표는 최댓값보다 크거나 최솟값보다 작지 않아야 한다
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        //카메라의 위치를 목표물의 위치로 설정하고 z컴포넌트는 동일해야 한다
        transform.position =
            new Vector3(targetX, targetY, transform.position.z);
    }
}
