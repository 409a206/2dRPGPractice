using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldExtensions
{   
    //this 키워드는 이 메소드가 확장 메소드임을 의미하며 this키워드의 티입을 갖고 직접 함수를 실행할 수 있다
    //예 : var clickPoint = Input.mousePosition.GetScreenPointFor2D();
    public static Vector3 ToVector3_2D(this Vector3 coordinate) {
        return new Vector3(coordinate.x, coordinate.y, 0);
    }

    public static Vector2 GetScreenPositionIn2D(this Vector3 screenCoordinate) {
        Vector3 wp = Camera.main.ScreenToWorldPoint(screenCoordinate);
        return new Vector2(wp.x, wp.y);
    }

    public static Vector3 GetScreenPositionFor2D(this Vector3 screenCoordinate) {
        Vector3 wp = Camera.main.ScreenToWorldPoint(screenCoordinate);
        return wp.ToVector3_2D();
    }
}
