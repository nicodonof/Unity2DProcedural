﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
  BoxCollider2D coll;
	Animator animator;
	// Use this for initialization
	void Start () {
    rigid = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = rigid.velocity.x;
    float y = rigid.velocity.y;
		rigid.velocity = 
			new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") + x + Time.deltaTime, -7,7),
			y
			);
		if(Input.GetButtonDown("Jump") && rigid.velocity.y == 0){
			rigid.AddForce(new Vector2(0,400));
		}

		animator.SetFloat("Speed", rigid.velocity.x);
		print(rigid.velocity.y);
		animator.SetFloat("SpeedY", rigid.velocity.y);
		// animator.speed = rigid.velocity.x / 5;
	}
}
