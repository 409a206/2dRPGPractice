using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private GameObject playerSprite;
    private Rigidbody2D playerRigidBody2D;
    //private GameObject playerSprite;

    private bool facingRight = true;

    public float speed = 4.0f;

    private void Awake() {
        playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
        playerSprite = transform.Find("PlayerSprite").gameObject;
        //playerSprite = transform.Find("PlayerSprite").gameObject;
        anim = (Animator)playerSprite.GetComponent(typeof(Animator));
    }

    private void Update() {
        float movePlayerVector = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(movePlayerVector));

        playerRigidBody2D.velocity = new Vector2(movePlayerVector * speed, playerRigidBody2D.velocity.y);

        if(movePlayerVector > 0 && !facingRight) {
            Flip();
        } else if (movePlayerVector < 0 && facingRight) {
            Flip();
        }
    }

    //플레이어가 바라보는 방향 교체
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = playerSprite.transform.localScale;
        theScale.x *= -1;
        playerSprite.transform.localScale = theScale;
    }
}
