﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour {
    public LayerMask enemyMask;
    public float enemySpeed;
    public int damageToGive;
    float myWidth;

    Animator enemyAnimator;
    Rigidbody2D myBody;
    Transform myTransform;


    void Start () {
        myTransform = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyAnimator.SetBool("isCharging", true); //set enemy to constantly charge

    }

    void FixedUpdate () {
        //check if there is ground ahead of the enemy
        //draws a line that shows where the tip of the enemy is
        Vector2 lineCastPos = myTransform.position - myTransform.right * .65f * myWidth;
        
       // Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isOnGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        if (!isOnGrounded) //if the position ahead is not ground, change direction of enemy
        {
            Vector3 rotate = myTransform.eulerAngles;
            rotate.y += 180; //rotate 180 degrees
            myTransform.eulerAngles = rotate; //change the angle of the transform to angle of rotate
        }
        //move forward until it hits the edge
        Vector2 myVel = myBody.velocity;
        myVel.x = myTransform.right.x * - enemySpeed;
        myBody.velocity = myVel;
	}
}
